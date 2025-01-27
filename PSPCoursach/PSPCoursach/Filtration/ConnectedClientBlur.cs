using PSPCoursach.Networking.Host;
using System.Numerics;

namespace PSPCoursach.Filtration
{
    public class ConnectedClientBlur : ServerBlur
    {
        public ConnectedClientBlur(int id, ServerComponent network, int kernelSize) : base(id, network, kernelSize)
        {
        }

        public void SendImageDataInit(int imgWidth, int colorDepth) { 
            _isImageSizesInitialized = true;
            server.SendImageDataInitialization(Id, kernelSize, imgWidth, colorDepth);
        }

        public override void SolveImagePart(byte[,,] imagePart, int width, int localHeight, int colorDepth, int localRowNumber, int globalRowNumber)
        {
            byte[] stretched = Image<byte>.GetStretched(imagePart, width, localHeight, colorDepth);
            SolveImagePart(stretched, localHeight, width, colorDepth, localRowNumber, globalRowNumber);
        }

        private void SolveImagePart(byte[] imagePart, int height, int width, int colorDepth, int localRowNumber, int globalRowNumber)
        {
            if (!_isImageSizesInitialized) {
                _isImageSizesInitialized=true;
                SendImageDataInit(width, colorDepth);
            }

            server.SendImagePart(Id, new ImageSendService(localRowNumber, globalRowNumber, height, imagePart));
        }

        private bool _isImageSizesInitialized = false;
    }
}
