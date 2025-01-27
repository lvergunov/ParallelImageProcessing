using PSPCoursach.Networking.ClientLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSPCoursach.Filtration
{
    public class ClientSideFiltration
    {

        public ClientSideFiltration(ClientComponent clientComponent, AbstractBlur blurControl, int imgWidth, int imgColorDepth)
        {
            _clientComponent = clientComponent;
            _blurControl = blurControl;
            _imgWidth = imgWidth;
            _imgColorDepth = imgColorDepth; 
        }

        public void SolveRow(byte[] receivedLine, int localHeight, int localRow, int globalRow) {
            byte[,,] tensored = Image<byte>.GetTensoredLine(receivedLine, _imgWidth, localHeight, _imgColorDepth);
            byte[,] processed = _blurControl.ProcessOneRow(tensored, localRow, localHeight, _imgWidth, _imgColorDepth);
            byte[] lined = Image<byte>.GetStretched(processed, _imgWidth, _imgColorDepth);
            _clientComponent.SendProcessed(lined, globalRow);
        }

        private int _imgWidth = 0;
        private int _imgColorDepth = 0;
        private ClientComponent _clientComponent;
        private AbstractBlur _blurControl;
    }
}
