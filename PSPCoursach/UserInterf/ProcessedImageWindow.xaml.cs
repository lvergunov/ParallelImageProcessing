﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UserInterf
{
    /// <summary>
    /// Логика взаимодействия для ProcessedImageWindow.xaml
    /// </summary>
    public partial class ProcessedImageWindow : Window
    {
        public ProcessedImageWindow()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                byte[] linedImage = MainWindow.TransferData.imageToTransferInByte.StretchedTensor;
                BitmapImage bitmap = IntArrayToBitmapImage(linedImage, MainWindow.TransferData.imageToTransferInByte.Width,
                    MainWindow.TransferData.imageToTransferInByte.Height);
                ProcessedImageField.Source = bitmap;
                OutputTextBlock.Text = $"Линейно выполнено за {MainWindow.TransferData.processingTime} миллисекунд.";
            }
            catch {
                OutputTextBlock.Text = "Ошибка формата изображения!";
            }
        }

        private void CloseThisWindow(object sender, RoutedEventArgs e)
        { 
            this.Close();
        }

        private BitmapImage IntArrayToBitmapImage(byte[] pixelData, int width, int height)
        {
            WriteableBitmap writeableBitmap = new WriteableBitmap(width, height, 96, 96, System.Windows.Media.PixelFormats.Bgra32, null);

            writeableBitmap.WritePixels(
                new Int32Rect(0, 0, width, height), 
                pixelData,                          
                width * 4,                          
                0                                   
            );

            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream stream = new MemoryStream())
            {
                BitmapEncoder encoder = new PngBitmapEncoder(); 
                encoder.Frames.Add(BitmapFrame.Create(writeableBitmap));
                encoder.Save(stream);

                stream.Seek(0, SeekOrigin.Begin);
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }

            return bitmapImage;
        }

        private void SaveImage_Click(object sender, RoutedEventArgs e)
        {
            if (ProcessedImageField.Source is BitmapImage bitmap)
            {

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp",
                    Title = "Save Image",
                    FileName = "image.png"
                };
                if (saveFileDialog.ShowDialog() == true)
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    switch (System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower())
                    {
                        case ".jpg":
                            encoder = new JpegBitmapEncoder();
                            break;
                        case ".bmp":
                            encoder = new BmpBitmapEncoder();
                            break;
                    }
                    encoder.Frames.Add(BitmapFrame.Create(bitmap));

                    using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        encoder.Save(fileStream);
                    }
                }
            }
            else {
                MessageBox.Show("Ошибка формата изображения!");
            }
        }
    }
}
