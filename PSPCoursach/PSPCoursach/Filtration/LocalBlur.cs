
using System.Numerics;
using System.Windows.Media.Media3D;

namespace PSPCoursach.Filtration
{
    public class LocalBlur : AbstractBlur
    {
        public LocalBlur(int kernelSize) : base(kernelSize)
        {
        }

        public override Image<byte> ProcessFullImage(Image<byte> image) 
        {
            return new Image<byte>(ProcessImagePart(image.Bitmap, image.Height, image.Width, image.ColorFormat),
                image.Width, image.Height, image.ColorFormat);
        }

        public override byte[,,] ProcessImagePart(byte[,,] image, int height, int width, int depth)
        {
            int offset = kernelSize / 2;
            byte[,,] newBitmap = new byte[height, width, depth];
            for (int h = 0; h < height; h++) {
                for (int w = 0; w < width; w++) {
                    double[] colorSums = PrepareColorSums<double>(depth);
                    int count = 0;
                    for (int ky = -offset; ky <= offset; ky++) {
                        for (int kx = -offset; kx <= offset; kx++) {
                            int nx = w + kx;
                            int ny = h + ky;
                            if (nx >= 0 && ny >= 0 && nx < width && ny < height) {

                                for (int c = 0; c < depth - 1; c++) {
                                    colorSums[c] += Convert.ToDouble(image[ny, nx, c]);
                                }

                                count++;
                            }
                        }
                    }

                    for (int c = 0; c < depth - 1; c++) {
                        newBitmap[h, w, c] = (byte)(colorSums[c] / count);
                    }
                    newBitmap[h, w, depth - 1] = image[h, w, depth - 1];
                }
            }

            return newBitmap;
        }

        public override byte[,,] ProcessImagePart(byte[] imageBitmap, int height, int width, int colorDepth)
        {
            byte[,,] tensoredImage = Image<byte>.GetTensoredLine(imageBitmap, width, height, colorDepth);
            return ProcessImagePart(tensoredImage, height, width, colorDepth);
        }

        public override byte[,] ProcessOneRow(byte[,,] image, int rowNumber, int localHeight, int width, int depth)
        {
            int offset = kernelSize / 2;
            byte[,] processedRow = new byte[width, depth];
            for (int w = 0; w < width; w++)
            {
                double[] colorSums = PrepareColorSums<double>(depth);
                int count = 0;
                for (int ky = -offset; ky <= offset; ky++)
                {
                    for (int kx = -offset; kx <= offset; kx++)
                    {
                        int nx = w + kx;
                        int ny = rowNumber + ky;
                        if (nx >= 0 && ny >= 0 && nx < width && ny < localHeight)
                        {

                            for (int c = 0; c < depth - 1; c++)
                            {
                                colorSums[c] += Convert.ToDouble(image[ny, nx, c]);
                            }

                            count++;
                        }
                    }
                }

                for (int c = 0; c < depth - 1; c++)
                {
                    processedRow[w, c] = (byte)(colorSums[c] / count);
                }
                processedRow[w, depth - 1] = image[rowNumber, w, depth - 1];
            }
            return processedRow;
        }

        private T[] PrepareColorSums<T>(int colorFormat) where T : INumber<T>{
            T[] colorSums = new T[colorFormat];
            for (int i = 0; i < colorFormat; i++) {
                colorSums[i] = T.CreateChecked(0);
            }
            return colorSums;
        }

        private T ConvertDoubleToGeneric<T>(double value) where T : INumber<T>, IConvertible { 
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
