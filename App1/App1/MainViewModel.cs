using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Sockets.Plugin;
using Sockets.Plugin.Abstractions;

namespace App1
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ISomeService _someService;
        public ObservableCollection<UDPMessage> Messages { get; set; }
        private readonly  UdpSocketReceiver receiver;

        int listenPort = 1900;

        public MainViewModel(ISomeService someService)
        {
            receiver = new UdpSocketReceiver();
          
            receiver.MessageReceived += OnMessageReceived;


            Messages = new ObservableCollection<UDPMessage>();
            
            Task.Run(()=>receiver.StartListeningAsync(listenPort));

        }

        private void OnMessageReceived(object sender, UdpSocketMessageReceivedEventArgs e)
        {
            var from = String.Format("{0}:{1}", e.RemoteAddress, e.RemotePort);
            var data = Encoding.UTF8.GetString(e.ByteData, 0, e.ByteData.Length);
            Messages.Add(new UDPMessage(){Value = data});
            Debug.WriteLine("{0} - {1}", @from, data);
            RaisePropertyChanged(() => Messages);
        }

        public async Task Init()
        {
          
        }
    }

    public  class UDPMessage
    {
        public string Value { get; set; }
    }
}
