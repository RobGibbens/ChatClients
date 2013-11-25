using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
using ChatClient.Shared;
using Microsoft.AspNet.SignalR.Client;

namespace ChatClient.Store
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly Client _client;

        IHubProxy chat;
        public SynchronizationContext Context { get; set; }
        private ObservableCollection<string> _items  = new ObservableCollection<string>();
        

        public ObservableCollection<string> Items
        {
            get { return _items; }
        }

        public MainPage()
        {

            this.InitializeComponent();
            _client = new Client("WinStore");
            _client.Connect();
            //Task.WaitAll( _client.Connect());
            _client.OnMessageReceived += (sender, message) => DoStuff2(message);

            _items.Add("test");

            this.DataContext = Items;

            //MessagesList.ItemsSource = Items;
        }

        private void DoStuff2(string message)
        {
            _items.Add(message);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await _client.Send(MessageText.Text);

            MessageText.Text = "";
        }


    }
}
