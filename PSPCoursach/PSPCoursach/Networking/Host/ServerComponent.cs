using PSPCoursach.Filtration;
using System.Net;
using System.Net.Sockets;

namespace PSPCoursach.Networking.Host
{
    public delegate void ServerPackageHandler(int fromClientId, BytePackage package);
    public delegate void UserConnectionHandler(int fromClientId, string ipAddress);
    public delegate void ServerErrorMessageHandler(string message);
    public delegate void ClientDisconnectHandler(int clientId);
    public delegate void RowReceivementHandler(int clientID, int rowGlobalIndex, byte[] line);
    public class ServerComponent : NetworkHandler
    {
        public event ServerErrorMessageHandler onServerError;
        public event UserConnectionHandler onConnectClient;
        public event ClientDisconnectHandler onDisconnectClient;
        public event RowReceivementHandler onRowReceivement;
        public ServerSender Sender { get { return _serverSender; } }
        public List<ConnectedClient> ActiveNodes
        {
            get
            {
                return _connectedClients.Values.Where(connected => connected.Tcp.Active).ToList();
            }
        }

        public void SendImageDataInitialization(int clientID, int kernelSize, int width, int colorDepth)
        {
            _serverSender.SendImageSizes(_connectedClients[clientID], kernelSize, width, colorDepth);
        }

        public void SendImagePart(int id, ImageSendService imageSendService)
        {
            _serverSender.SendImageRow(_connectedClients[id], imageSendService);
        }

        public void HandleRowReceivement(int userId, int globalRowNumber, byte[] line)
        {
            onRowReceivement(userId, globalRowNumber, line);
        }

        public override void Disconnect(bool withUniform)
        {
            try
            {
                _newThreadForListener = null;
                foreach (var cl in _connectedClients.Values)
                {
                    if (cl.Tcp.Client != null && cl.Tcp.Client.Connected)
                    {
                        _serverSender.DisconnectHost(_connectedClients);
                        cl.Disconnect();
                    }
                }
            }
            catch
            {
                onServerError("Ошибка при разрыве соединения с пользователями!");
            }
            finally
            {
                _listener.Stop();
            }
        }

        public void DisconnectOne(int id)
        {
            try
            {
                onDisconnectClient(id);
                _serverSender.DisconnectClient(_connectedClients, id);
                _connectedClients[id].Disconnect();
            }
            catch
            {
                onServerError("Ошибка при отключении пользователя!");
            }
        }
        public static ServerComponent ServerInstance { get; private set; }

        public ServerComponent(Threading threading, int maxUserAmount, int portNumber) : base(threading, portNumber)
        {
            _maxUserAmount = maxUserAmount;
            ServerInstance = this;
            _listener = new TcpListener(IPAddress.Any, portNumber);
            _serverSender = new ServerSender(this);
            _serverHandler = new ServerHandler(this);

            for (int i = 1; i < _maxUserAmount; i++)
            {
                _connectedClients.Add(i, new ConnectedClient(i, this, ThreadManager));
            }

            ActionsForPackage = new Dictionary<int, ServerPackageHandler>()
            {
                { (int)ClientPackets.welcomeReceived, _serverHandler.ReceiveOnWelcome},
                { (int)ClientPackets.sendProcessedImagePart, _serverHandler.ReceiveProcessedRow },
                { (int)ClientPackets.disconnect, _serverHandler.ReceiveOnDisconnectUser}
            };
        }

        public void StartConnectionListener()
        {
            _threadUpdater = new ThreadUpdater(ThreadManager);
            _newThreadForListener = new Thread(_threadUpdater.UpdateThread);
            _newThreadForListener.Start();
            _listener.Start();
            _listener.BeginAcceptTcpClient(new AsyncCallback(WorkWithNewClient), null);
        }

        private void WorkWithNewClient(IAsyncResult _result)
        {
            try
            {
                TcpClient tcpClient = _listener.EndAcceptTcpClient(_result);
                _listener.BeginAcceptTcpClient(new AsyncCallback(WorkWithNewClient), null);
                for (int i = 1; i <= _maxUserAmount; i++)
                {
                    if (_connectedClients[i].Tcp.Client == null)
                    {
                        _connectedClients[i].Tcp.Connect(tcpClient);
                        _serverSender.WelcomeClient(_connectedClients[i]);
                        onConnectClient(i, _connectedClients[i].IP);
                        return;
                    }
                }
            }
            catch { }
        }
        Thread _newThreadForListener;
        public Dictionary<int, ServerPackageHandler> ActionsForPackage { get; private set; }
        private TcpListener _listener;
        private ServerSender _serverSender;
        private ServerHandler _serverHandler;
        private int _maxUserAmount = 0;
        private ThreadUpdater _threadUpdater;
        private int _kernelSize;
        private Dictionary<int, ConnectedClient> _connectedClients = new Dictionary<int, ConnectedClient>();
    }
}
