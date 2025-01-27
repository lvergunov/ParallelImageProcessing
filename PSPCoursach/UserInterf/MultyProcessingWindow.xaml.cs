using Microsoft.Win32;
using PSPCoursach.Filtration;
using PSPCoursach.Networking.Host;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Imaging;

namespace UserInterf
{
    /// <summary>
    /// Логика взаимодействия для MultyProcessingWindow.xaml
    /// </summary>
    public partial class MultyProcessingWindow : Window
    {
        public MultyProcessingWindow()
        {
            InitializeComponent();
        }

        private void LoadImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif|All Files|*.*"
            };

            if (fileDialog.ShowDialog() == true)
            {
                try
                {
                    _imageToProcess = new BitmapImage(new Uri(fileDialog.FileName));
                    LoadedImage.Visibility = Visibility.Visible;
                    LoadedImage.Source = _imageToProcess;
                    ButtonToLoadImg.Visibility = Visibility.Collapsed;
                    _isImageLoaded = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}");
                }
            }
        }

        private void CompletePocessings(object sender, RoutedEventArgs e)
        {
            if (!_isImageLoaded)
            {
                MessageBox.Show("Ошибка: изображение не загружено!");
                return;
            }

            WriteableBitmap writeableBitmap = new WriteableBitmap(_imageToProcess);
            int colorFormat = writeableBitmap.Format.BitsPerPixel / 8;
            int width = writeableBitmap.PixelWidth;
            int height = writeableBitmap.PixelHeight;
            int stride = width * colorFormat;
            byte[] pixels = new byte[height * stride];
            writeableBitmap.CopyPixels(pixels, stride, 0);
            PSPCoursach.Filtration.Image<byte> checkedImage = PSPCoursach.Filtration.Image<byte>.GetImage(pixels, width, height, colorFormat);
            Stopwatch stopwatch = new Stopwatch();
            ServerComponent networkHandler = NetworkingWindow.Network as ServerComponent;
            ParallelBlur parallelBlur = new ParallelBlur(networkHandler, MainWindow.TransferData.kernelSize);
            stopwatch.Start();
            MainWindow.TransferData.AddImage(parallelBlur.ProcessFullImage(checkedImage));
            stopwatch.Stop();
            MainWindow.TransferData.SetProcessingTime(stopwatch.ElapsedMilliseconds);
            ProcessedImageWindow processedImageWindow = new ProcessedImageWindow();
            processedImageWindow.Show();
        }

        private void BackToMainMenu(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool _isImageLoaded = false;
        private BitmapImage _imageToProcess;
    }
}
