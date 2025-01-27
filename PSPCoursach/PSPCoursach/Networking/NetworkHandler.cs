using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PSPCoursach.Networking
{
    public abstract class NetworkHandler
    {
        public abstract void Disconnect(bool withUniform);
        public static NetworkHandler Instance { get; protected set; }
        public Threading ThreadManager { get; protected set; }
        public int PortNumber { get; }
        public NetworkHandler(Threading threadManager, int portNumber) {
            ThreadManager = threadManager;
            Instance = this;
            PortNumber = portNumber;
        }
    }
}
