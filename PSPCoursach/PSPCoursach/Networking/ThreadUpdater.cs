using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSPCoursach.Networking
{
    public class ThreadUpdater
    {
        public ThreadUpdater(Threading threading) { 
            _mainThread = threading;
        }

        public void UpdateThread() {
            while (true)
            {
                _mainThread.UpdateMain();
                Thread.Sleep(10);
            }
        }
        private Threading _mainThread;
    }
}
