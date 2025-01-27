using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PSPCoursach.Filtration;

namespace UserInterf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenSingleProcessingWindow(object sender, RoutedEventArgs e)
        {
            int kernelSize = Int32.Parse(KernelInput.Text);
            ImageTypes imageType = (ImageTypes)ImageTypeField.SelectedIndex;
            TransferData = new WindowTransferData
            {
                blurWay = new LocalBlur(kernelSize),
                imageType = imageType
            };
            SingleProcessingWindow singleProcessingWindow = new SingleProcessingWindow();
            singleProcessingWindow.Show();
            this.Close();
        }

        private void OpenParallelProcessingWindow(object sender, RoutedEventArgs e)
        {
            int kernelSizeField = Int32.Parse(KernelInput.Text);
            ImageTypes imageType = (ImageTypes)ImageTypeField.SelectedIndex;
            TransferData = new WindowTransferData { 
                kernelSize = kernelSizeField
            };
            NetworkingWindow networkingWindow = new NetworkingWindow();
            networkingWindow.Show();
            this.Close();
        }

        public static WindowTransferData TransferData { get; private set; }

        private void NumericInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^\d+$");
        }
    }
}