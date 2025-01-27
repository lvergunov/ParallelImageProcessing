using System.Numerics;

namespace PSPCoursach.Filtration
{
    public class Image<T> where T : INumber<T>, IConvertible
    {
        public int Height { get => _height; }
        public int Width { get => _width; }
        public int ColorFormat { get => _colorFormat; }
        public T[,,] Bitmap { get; }
        public T[] StretchedTensor
        {
            get
            {
                T[] imageInline = new T[_height * _width * _colorFormat];
                int bigIndex = 0;
                for (int h = 0; h < _height; h++)
                {
                    for (int w = 0; w < _width; w++)
                    {
                        for (int d = 0; d < _colorFormat; d++)
                        {
                            imageInline[bigIndex] = Bitmap[h, w, d];
                            bigIndex++;
                        }
                    }
                }
                return imageInline;
            }
        }
        public Image(T[,,] bitmapTensor, int width, int height, int colorFormat)
        {
            Bitmap = bitmapTensor;
            _width = width;
            _height = height;
            _colorFormat = colorFormat;
        }

        public static Image<T> GetImage(T[] lined, int width, int height, int colorFormat)
        {

            return new Image<T>(GetTensoredLine(lined, width, height, colorFormat), width, height, colorFormat);
        }

        public T[] GetStretchedPart(int rowNumber, int kernelSize)
        {
            int offset = kernelSize / 2;
            T[] stretched = new T[_width * kernelSize * _colorFormat];
            int rowUp = rowNumber - offset;
            if (rowUp < 0)
                rowUp = 0;
            int rowDown = rowNumber + offset;
            if (rowDown > _height - 1)
                rowDown = _height - 1;

            int stretchedIndex = 0;
            for (int row = rowUp; row <= rowDown; row++)
            {
                for (int col = 0; col < _width; col++)
                {
                    for (int dep = 0; dep < _colorFormat; dep++)
                    {
                        stretched[stretchedIndex] = Bitmap[row, col, dep];
                        stretchedIndex++;
                    }
                }
            }
            return stretched;
        }

        public T[,,] GetStripe(int rowStart, int rowFinish)
        {
            T[,,] stripe = new T[rowFinish - rowStart + 1, Width, ColorFormat];
            for (int row = rowStart; row <= rowFinish; row++)
            {
                for (int column = 0; column < Width; column++)
                {
                    for (int color = 0; color < ColorFormat; color++)
                    {
                        stripe[row - rowStart, column, color] = Bitmap[row, column, color];
                    }
                }
            }
            return stripe;
        }

        public static T[,,] GetTensoredLine(T[] lined, int width, int height, int colorFormat)
        {
            int bigIndex = 0;
            T[,,] bitmapTensor = new T[height, width, colorFormat];
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    for (int d = 0; d < colorFormat; d++)
                    {
                        bitmapTensor[h, w, d] = lined[bigIndex];
                        bigIndex++;
                    }
                }
            }
            return bitmapTensor;
        }

        public static T[,] GetTwoDimRow(T[] lined, int width, int colorFormat)
        {
            int bigIndex = 0;
            T[,] twoDimensional = new T[width, colorFormat];
            for (int w = 0; w < width; w++)
            {
                for (int d = 0; d < colorFormat; d++)
                {
                    twoDimensional[w, d] = lined[bigIndex];
                    bigIndex++;
                }
            }
            return twoDimensional;
        }

        public static T[] GetStretched(T[,,] tensor, int width, int height, int colorFormat)
        {
            T[] line = new T[height * width * colorFormat];
            int glIndex = 0;
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    for (int dep = 0; dep < colorFormat; dep++)
                    {
                        line[glIndex] = tensor[row, col, dep];
                        glIndex++;
                    }
                }
            }

            return line;
        }

        public static T[] GetStretched(T[,] array, int width, int colorFormat)
        {
            T[] line = new T[width * colorFormat];
            int glIndex = 0;
            for (int col = 0; col < width; col++)
            {
                for (int dep = 0; dep < colorFormat; dep++)
                {
                    line[glIndex] = array[col, dep];
                    glIndex++;
                }
            }

            return line;
        }

        private int _width;
        private int _height;
        private int _colorFormat;
    }
}
