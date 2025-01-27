using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSPCoursach.Networking.Host
{
    public class ServerHandler
    {
        public ServerHandler(ServerComponent serverComponent) { 
            _serverComponent = serverComponent;
        }

        public void ReceiveOnWelcome(int fromClientID, BytePackage bytePackage) {
            int clientID = bytePackage.ReadInt();
            if (fromClientID != clientID)
                throw new Exception("Ошибка в идентификации пользователя!");
        }

        public void ReceiveProcessedRow(int fromClientID, BytePackage bytePackage) { 
            int globalRowNum = bytePackage.ReadInt();
            int size = bytePackage.ReadInt();
            byte[] line = new byte[size];
            for (int i = 0; i < size; i++) 
            { 
                line[i] = bytePackage.ReadByte();
            }
            _serverComponent.HandleRowReceivement(fromClientID, globalRowNum, line);
        }

        public void ReceiveOnDisconnectUser(int fromClientID, BytePackage bytePackage) { 
            int clientID = bytePackage.ReadInt();
            _serverComponent.DisconnectOne(clientID);
        }
        private ServerComponent _serverComponent;
    }
}
