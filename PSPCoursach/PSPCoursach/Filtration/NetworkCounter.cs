using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PSPCoursach.Filtration
{
    public class NetworkCounter
    {
        public ServerBlur ServerBlur { get => _blurToCount; }
        public NetworkCounter(int id, ServerBlur localBlur)
        {
            _blurToCount = localBlur;
            ID = id;
        }

        public void SendDataToProcess(byte[,,] imagePart, int rowNumberLocal, int rowNumberGlobal, int height, int width, int colorDepth)
        {
            _blurToCount.SolveImagePart(imagePart, width, height, colorDepth, rowNumberLocal, rowNumberGlobal);
        }

        

        public int ID { get; }
        private ServerBlur _blurToCount;
    }

}
