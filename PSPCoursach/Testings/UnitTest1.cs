using System;
using PSPCoursach.Filtration;

namespace Testings
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestTensorstretching()
        {
            Random random = new Random();
            int height = 4;
            int width = 9;
            int depth = 3;
            float[,,] tensor = new float[height, width, depth];
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    for (int d = 0; d < depth; d++) {
                        tensor[h, w, d] = (float)random.NextDouble();
                    }
                }
            }
            Image<float> image = new Image<float>(tensor, width, height, depth);
            float[] stretchedTensor = image.StretchedTensor;
            int indexForStretched = 0;
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    for (int d = 0; d < depth; d++)
                    {
                        Assert.AreEqual(tensor[h,w,d], stretchedTensor[indexForStretched]);
                        indexForStretched++;
                    }
                }
            }
        }

        [TestMethod]
        public void TestUnstreatching() {
            int height = 4;
            int width = 9;
            int depth = 3;
            Random random = new Random();
            int[] lineFormat = new int[height * width * depth];
            for (int i = 0; i < height * width * depth; i++) {
                lineFormat[i] = random.Next(0, 100);
            }
            Image<int> image = Image<int>.GetImage(lineFormat, width, height, depth);
            int indexForStretched = 0;
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    for (int d = 0; d < depth; d++)
                    {
                        Assert.AreEqual(image.Bitmap[h, w, d], lineFormat[indexForStretched]);
                        indexForStretched++;
                    }
                }
            }
        }

        [TestMethod]
        public void StretchAndUnstretch() {
            Random random = new Random();
            int height = 4;
            int width = 9;
            int depth = 3;
            float[,,] tensor = new float[height, width, depth];
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    for (int d = 0; d < depth; d++)
                    {
                        tensor[h, w, d] = (float)random.NextDouble();
                    }
                }
            }
            Image<float> image = new Image<float>(tensor, width, height, depth);
            float[] stretchedTensor = image.StretchedTensor;
            Image<float> newImage = Image<float>.GetImage(stretchedTensor, width, height, depth);
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    for (int d = 0; d < depth; d++)
                    {
                        Assert.AreEqual(tensor[h,w,d], newImage.Bitmap[h,w,d]);
                    }
                }
            }
        }
    }
}