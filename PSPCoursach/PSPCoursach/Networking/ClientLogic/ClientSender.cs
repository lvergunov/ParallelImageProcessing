using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PSPCoursach.Networking.ClientLogic
{
    public class ClientSender
    {
        private ClientComponent _clientComponent;

        public ClientSender(ClientComponent clientComponent)
        {
            _clientComponent = clientComponent;
        }

        private void SendTCP(BytePackage package)
        {
            package.WriteLength();
            _clientComponent.Tcp.SendBytes(package);
        }

        public void AnswerOnWelcome()
        {
            using (BytePackage newPackage = new BytePackage((int)ClientPackets.welcomeReceived))
            {
                newPackage.Write(_clientComponent.Tcp.GlobalId);
                SendTCP(newPackage);
            }
        }

        public void SendProcessedImagePart(int globalRowNumber, byte[] stretchedImage) {
            using (BytePackage newPackage = new BytePackage((int)ClientPackets.sendProcessedImagePart)) {
                newPackage.Write(globalRowNumber);
                newPackage.Write(stretchedImage.Length);
                for (int i = 0; i < stretchedImage.Length; i++) { 
                    newPackage.Write(stretchedImage[i]);
                }

                SendTCP(newPackage);
            }
        }

        public void SendDisconnect(int id)
        {
            using (BytePackage newPackage = new BytePackage((int)ClientPackets.disconnect))
            {
                newPackage.Write(id);
                SendTCP(newPackage);
            }
        }

    }
}
