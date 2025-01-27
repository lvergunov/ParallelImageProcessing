using System.Numerics;

namespace PSPCoursach.Filtration
{
    public abstract class AbstractBlur
    {
        public AbstractBlur(int kernelSize)
        {
            this.kernelSize = kernelSize;
        }
        public abstract Image<byte> ProcessFullImage(Image<byte> image);
        public abstract byte[,,] ProcessImagePart(byte[,,] imageBitmap, int height, int width, int colorDepth);

        public abstract byte[,,] ProcessImagePart(byte[] imageBitmap, int height, int width, int colorDepth);
        public abstract byte[,] ProcessOneRow(byte[,,] image, int rowNumber, int localHeight, int width, int depth);

        protected int kernelSize;
    }
}
