using PSPCoursach.Filtration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSPCoursach.Networking.Host
{
    public class ServerSender
    {
        public ServerSender(ServerComponent serverComponent)
        {
            _serverComponent = serverComponent;
        }

        public void SendTCP(ConnectedClient client, BytePackage bytePackage)
        {
            bytePackage.WriteLength();
            client.Tcp.SendBytes(bytePackage);
        }

        public void WelcomeClient(ConnectedClient connectedClient)
        {
            using (BytePackage _package = new BytePackage((int)ServerPackets.welcome))
            {
                _package.Write(connectedClient.Id);
                SendTCP(connectedClient, _package);
            }
        }

        public void DisconnectClient(Dictionary<int, ConnectedClient> clients, int id)
        {
            using (BytePackage package = new BytePackage((int)ServerPackets.disconnectClient))
            {
                package.Write(id);
                BroadcastMessage(clients, package);
            }
        }

        public void DisconnectHost(Dictionary<int, ConnectedClient> clients)
        {
            using (BytePackage package = new BytePackage((int)ServerPackets.disconnectHost))
            {
                package.Write(0);
                BroadcastMessage(clients, package);
            }
        }

        public void SendImageSizes(ConnectedClient client, int kernelSize, int width, int colorDepth)
        {
            using (BytePackage package = new BytePackage((int)ServerPackets.sendImageSizes))
            {
                package.Write(kernelSize);
                package.Write(width);
                package.Write(colorDepth);

                SendTCP(client, package);
            }
        }

        public void SendImageRow(ConnectedClient connectedClient, ImageSendService imageSend)
        {
            using (BytePackage package = new BytePackage((int)ServerPackets.sendImagePart))
            {
                package.Write(imageSend.localHeight);
                package.Write(imageSend.localRowNumber);
                package.Write(imageSend.globalRowNumber);
                foreach (byte pixelPart in imageSend.partOfImage)
                {
                    package.Write(pixelPart);
                }
                SendTCP(connectedClient, package);
            }
        }

        public void BroadcastMessage(Dictionary<int, ConnectedClient> clients, BytePackage package)
        {
            package.WriteLength();
            foreach (var cl in clients.Values)
            {
                if (cl.Tcp.Active)
                {
                    cl.Tcp.SendBytes(package);
                }
            }
        }

        public void BroadcastMessage(Dictionary<int, ConnectedClient> clients, int exceptId, BytePackage package)
        {
            package.WriteLength();
            foreach (var cl in clients.Values)
            {
                if (cl.Tcp.Active && cl.Id != exceptId)
                    cl.Tcp.SendBytes(package);
            }
        }



        private ServerComponent _serverComponent;
    }
}
