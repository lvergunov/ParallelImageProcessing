using PSPCoursach.Filtration;
using PSPCoursach.Networking.Host;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static PSPCoursach.Networking.Host.ConnectedClient;

namespace PSPCoursach.Networking.ClientLogic
{
    public delegate void ConnectedToServerHandler(int receivedId);
    public delegate void ClientErrorHandler(string message);
    public class ClientComponent : NetworkHandler
    {
        public event ConnectedToServerHandler onHostDisconnected;
        public string IP { get; }
     
        public ClientComponent(Threading threadManager, int portNumber, string ipAddress) : 
            base(threadManager, portNumber)
        {
            IP = ipAddress;
            _handler = new ClientHandler(this);
            _sender = new ClientSender(this);
            _tcp = new TCP(threadManager, _handler, _sender);
        }
        public TCP Tcp
        {
            get
            {
                return _tcp;
            }
        }

        public void GetFiltrationInitializationData(int width, int colorDepth, int kernelSize) { 
            _imageWidth = width;
            _imageColorDepth = colorDepth;
            _kernelSize = kernelSize;
            _imageDataInitialized = true;
            _clientFiltration = new ClientSideFiltration(this, new LocalBlur(kernelSize), width, colorDepth);
        }

        public void ReceiveImagePartToProcess(int localHeight, int rowNumberLocal, int rowNumberGlobal, byte[] receivedImg) {
            _clientFiltration.SolveRow(receivedImg, localHeight, rowNumberLocal, rowNumberGlobal);
        }

        public void SendProcessed(byte[] line, int globalRowNumber) {
            _sender.SendProcessedImagePart(globalRowNumber, line);
        }

        public override void Disconnect(bool withInform)
        {
            if (withInform)
                _sender.SendDisconnect(_tcp.GlobalId);
        }

        public void ConnectToServer() { 
            _isConnected = true;
            InitializeClientData();
            _tcp.Connect(IP, PortNumber);
        }

        private int _imageWidth = 0;
        private int _imageColorDepth = 0;
        private int _kernelSize = 0;
        private bool _imageDataInitialized = false;
        private ClientHandler _handler;
        private ClientSender _sender;
        private delegate void PacketHandler(BytePackage _packet);
        private static Dictionary<int, PacketHandler> _packetHandlers;
        private TCP _tcp;
        private bool _isConnected;
        private ClientSideFiltration _clientFiltration;
        public void DisconnectHost()
        {
            onHostDisconnected(0);
            _isConnected = false;
            _tcp.Connection.Close();
            _tcp.Disconnect();
        }
        private void InitializeClientData() {
            _packetHandlers = new Dictionary<int, PacketHandler> {
                { (int)ServerPackets.welcome, _handler.OnWelcome },
                { (int)ServerPackets.disconnectHost, _handler.OnDisconnect },
                { (int)ServerPackets.disconnectClient, _handler.OnDisconnect },
                { (int)ServerPackets.sendImageSizes, _handler.OnReceiveImageSizes },
                { (int)ServerPackets.sendImagePart, _handler.OnReceiveImagePart }
            };
        }
        public void AnswerOnWelcome() {
            _sender.AnswerOnWelcome();
        }

        public class TCP {

            public event ConnectedToServerHandler onConnectToServer;
            public event ClientErrorHandler onClientError;

            public int GlobalId
            {
                get
                {
                    return _globalId;
                }
                set
                {
                    if (!_isIdSetup)
                    {
                        onConnectToServer(value);
                        _isIdSetup = true;
                        _globalId = value;
                    }
                }
            }
            public Threading ThreadManager { get; private set; }
            public TCP(Threading threadManager, ClientHandler clientHandler,
                ClientSender sender)
            {
                ThreadManager = threadManager;
                _clientHandlerInner = clientHandler;
                _clientSender = sender;
            }
            private string _ip;
            private int _port;
            private int _globalId;
            private int _bufferSize = 16 * 32;
            private bool _isIdSetup = false;
            private BytePackage _receivedPackage;
            private ClientHandler _clientHandlerInner;
            private ClientSender _clientSender;
            public TcpClient Connection
            {
                get
                {
                    return _connection;
                }
            }

            public void Connect(string ip, int port) {
                try
                {
                    _threadUpdater = new ThreadUpdater(ThreadManager);
                    Thread _newThreadForListener = new Thread(_threadUpdater.UpdateThread);
                    _newThreadForListener.Start();
                    _ip = ip;
                    _port = port;
                    _connection = new TcpClient();
                    _connection.ReceiveBufferSize = _bufferSize;
                    _connection.SendBufferSize = _bufferSize;
                    _receiveBuffer = new byte[_bufferSize];
                    _connection.BeginConnect(_ip, _port,
                        ConnectCallback, _connection);
                }
                catch {
                    onClientError("Ошибка при подключении!");
                }
            }

            public void SendBytes(BytePackage package)
            {
                try
                {
                    _stream.BeginWrite(package.ToArray(), 0, package.Length, null, null);
                }
                catch
                {
                    onClientError("Ошибка отправки сообщения!");
                    Disconnect();
                }
            }
            private void ConnectCallback(IAsyncResult result)
            {
                _connection.EndConnect(result);

                if (!_connection.Connected)
                {
                    return;
                }
                try
                {
                    _receivedPackage = new BytePackage();
                    _stream = _connection.GetStream();
                    _stream.BeginRead(_receiveBuffer, 0, _bufferSize,
                    ReceiveCallback, null);
                }
                catch (Exception ex)
                {
                    onClientError("Ошибка при подключении!");
                    Disconnect();
                }
            }


            private void ReceiveCallback(IAsyncResult _result)
            {
                try
                {
                    int _byteLength = _stream.EndRead(_result);
                    if (_byteLength <= 0)
                    {
                        Connection.Close();
                        return;
                    }
                    byte[] data = new byte[_byteLength];
                    Array.Copy(_receiveBuffer, data, _byteLength);

                    _receivedPackage.Reset(HandleData(data));
                    _stream.BeginRead(_receiveBuffer, 0, _bufferSize, ReceiveCallback, null);
                }
                catch (Exception ex)
                {
                    onClientError("Ошибка получения сообщения!");
                    Disconnect();
                }
            }

            private bool HandleData(byte[] data)
            {
                int _packetLength = 0;

                _receivedPackage.SetBytes(data);

                if (_receivedPackage.UnreadLength >= 4)
                {
                    _packetLength = _receivedPackage.ReadInt();
                    if (_packetLength <= 0)
                    {
                        return true;
                    }
                }

                while (_packetLength > 0 && _packetLength <= _receivedPackage.UnreadLength)
                {
                    byte[] _packetBytes = _receivedPackage.ReadBytes(_packetLength);
                    ThreadManager.ExecuteOnMainThread(() =>
                    {
                        using (BytePackage _packet = new BytePackage(_packetBytes))
                        {
                            int _packetId = _packet.ReadInt();
                            _packetHandlers[_packetId](_packet);
                        }
                    });

                    _packetLength = 0;
                    if (_receivedPackage.UnreadLength >= 4)
                    {
                        _packetLength = _receivedPackage.ReadInt();
                        if (_packetLength <= 0)
                        {
                            return true;
                        }
                    }
                }

                if (_packetLength <= 1)
                {
                    return true;
                }

                return false;
            }

            public void Disconnect()
            {
                _newThreadForListener = null;
                _stream = null;
                _receiveBuffer = null;
                _connection = null;
            }
            private NetworkStream _stream;
            private Thread _newThreadForListener;
            private byte[] _receiveBuffer;
            private TcpClient _connection;
            private ThreadUpdater _threadUpdater;
        }
    }
}
