using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSPCoursach.Networking.ClientLogic
{
    public class ClientHandler
    {

        public void OnWelcome(BytePackage package)
        {
            _clientComponent.Tcp.GlobalId = package.ReadInt();
            _clientComponent.AnswerOnWelcome();
        }

        public void OnDisconnect(BytePackage package) {
            _clientComponent.DisconnectHost();
        }

        public void OnReceiveImageSizes(BytePackage package) {
            _kernelSize = package.ReadInt();
            int imgWidth = package.ReadInt();
            int imgColorFormat = package.ReadInt();
            _imgWidth = imgWidth;
            _imgColorFormat = imgColorFormat;
            _clientComponent.GetFiltrationInitializationData(_imgWidth, imgColorFormat, _kernelSize);
        }

        public void OnReceiveImagePart(BytePackage package) {
            int localHeight = package.ReadInt();
            int localRowNumber = package.ReadInt();
            int globalRowNumber = package.ReadInt();
            int byteLength = localHeight * _imgWidth * _imgColorFormat;
            byte[] receivedRow = new byte[byteLength];
            for (int i = 0; i < byteLength; i++)
            {
                receivedRow[i] = package.ReadByte();
            }
            _clientComponent.ReceiveImagePartToProcess(localHeight, localRowNumber, globalRowNumber, receivedRow);
        }

        private ClientComponent _clientComponent;

        public ClientHandler(ClientComponent clientComponent)
        {
            _clientComponent = clientComponent;
        }

        private int _imgWidth = 0;
        private int _imgColorFormat = 0;
        private int _kernelSize = 0;
    }
}
