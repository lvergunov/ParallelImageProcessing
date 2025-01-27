using PSPCoursach.Filtration;
using System.Numerics;
using System.Windows.Controls;

namespace UserInterf
{
    public class WindowTransferData
    {
        public int kernelSize;
        public AbstractBlur blurWay;
        public Image<float> imageToTransferInFloat;
        public Image<int> imageToTransferInInteger;
        public Image<double> imageToTransferInDouble;
        public Image<byte> imageToTransferInByte;
        public ImageTypes imageType;
        public float processingTime;

        public void AddImage<T>(Image<T> image) where T : INumber<T>, IConvertible {
            switch (imageType) {
                case ImageTypes.Byte:
                    imageToTransferInByte = image as Image<byte>;
                    break;
                case ImageTypes.Float:
                    imageToTransferInFloat = image as Image<float>;
                    break;
                case ImageTypes.Int:
                    imageToTransferInInteger = image as Image<Int32>;
                    break;
                case ImageTypes.Double:
                    imageToTransferInDouble = image as Image<Double>;
                    break;
            }
        }

        public void SetProcessingTime(float procTime) { 
            processingTime = procTime;
        }
    }

    public enum ImageTypes { 
        Byte, Float, Double, Int
    }
}
