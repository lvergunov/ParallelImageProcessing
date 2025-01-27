using PSPCoursach.Networking;
using PSPCoursach.Networking.ClientLogic;
using PSPCoursach.Networking.Host;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UserInterf
{
    /// <summary>
    /// Логика взаимодействия для NetworkingWindow.xaml
    /// </summary>
    public partial class NetworkingWindow : Window
    {
        public static NetworkHandler Network { get; private set; }
        public NetworkingWindow()
        {
            InitializeComponent();
            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(_isNeworkActive){
                Network.Disconnect(true);
            }
        }

            private void OpenConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            ConnectingButton.Visibility = Visibility.Collapsed;
            ConnectedDevices.Visibility = Visibility.Visible;
            int portNumber = -1;
            try
            {
                portNumber = Int32.Parse(PortNumberInput.Text);
                if (portNumber < 1 || portNumber > MAX_PORT_NUMBER) {
                    throw new Exception();
                }
            }
            catch {
                MessageBox.Show("Неверный формат адреса порта!");
                return;
            }

            try
            {
                Threading threading = new Threading();
                Network = new ServerComponent(threading, _maxUsers, portNumber);
                (Network as ServerComponent).StartConnectionListener();
                (Network as ServerComponent).onServerError += HandleNetworkErrorMessage;
                (Network as ServerComponent).onConnectClient += HandleClientConnection;
                (Network as ServerComponent).onDisconnectClient += HandleOneClientDisconnection;
                _isNeworkActive = true;
            }
            catch {
                MessageBox.Show("Ошибка при инициализации сети");
                return;
            }
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            ConnectingButton.Visibility = Visibility.Collapsed;
            IPInputCanvas.Visibility = Visibility.Visible;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void NumericInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^\d+$");
        }


        private void ConnectToHostButton_Click(object sender, RoutedEventArgs e)
        {
            string ipText = IPInput.Text;
            if (!Regex.IsMatch(ipText, @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$")) {
                MessageBox.Show("Неверный формат IP-адреса!");
                return;
            }
            IPInputCanvas.Visibility = Visibility.Collapsed;
            ConnectedAsClientInfo.Visibility = Visibility.Visible;

            int portNumber = -1;
            try
            {
                portNumber = Int32.Parse(PortNumberInput.Text);
                if (portNumber < 1 || portNumber > MAX_PORT_NUMBER)
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат адреса порта!");
                return;
            }

            try
            {
                Threading threading = new Threading();
                Network = new ClientComponent(threading, portNumber, ipText);
                (Network as ClientComponent).Tcp.onConnectToServer += HandleConnectionToServer;
                (Network as ClientComponent).ConnectToServer();
                (Network as ClientComponent).Tcp.onClientError += HandleNetworkErrorMessage;
                (Network as ClientComponent).onHostDisconnected += HandleHostDisconnection;
                _isNeworkActive = true;
            }
            catch {
                MessageBox.Show("Ошибка при поключении!");
            }
        }

        private void HandleNetworkErrorMessage(string message) { 
            MessageBox.Show(message);
        }

        private void HandleClientConnection(int id, string ipAddress) {
            Application.Current.Dispatcher.Invoke(() =>
            {
                TextBlock newClientTextBlock = new TextBlock()
                {
                    Text = $"Пользователь {id}. Адрес: {ipAddress}.",
                    FontSize = 45,
                    TextAlignment = TextAlignment.Center
                };
                DynamicPanel.Children.Add(newClientTextBlock);
                _idWithInfoBlock.Add(id, newClientTextBlock);
            });
        }

        private void HandleConnectionToServer(int id) {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ConnectedInfoField.Text = $"Устройство успешно подключено к вычислительной сети. Номер {id}";
            });
        }

        private void HandleOneClientDisconnection(int clientId) {
            Application.Current.Dispatcher.Invoke(()=>
            {
                DynamicPanel.Children.Remove(_idWithInfoBlock[clientId]);
                _idWithInfoBlock.Remove(clientId);
            });
        }

        private void HandleHostDisconnection(int hostId) {
            Application.Current?.Dispatcher.Invoke(() => { 
                _isNeworkActive = false;
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            });
        }

        private Dictionary<int, TextBlock> _idWithInfoBlock = new Dictionary<int, TextBlock>();
        private bool _isNeworkActive = false;
        private int _maxUsers = 4;
        private const int MAX_PORT_NUMBER = 65535;

        private void Load_Image_Click(object sender, RoutedEventArgs e)
        {
            MultyProcessingWindow multyProcessingWindow = new MultyProcessingWindow();
            multyProcessingWindow.Show();
        }
    }
}
