using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using uPLibrary.Networking.M2Mqtt;
using Windows.UI.Popups;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Diagnostics;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using static uPLibrary.Networking.M2Mqtt.Fx;
using Windows.UI.Core;
using System.Data.SQLite;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MQTTSubApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<MqttItem> mqttItem = new ObservableCollection<MqttItem>();
        public MainPage()
        {
            InitializeComponent();
            ItemList.DataContext = mqttItem;
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            string output_text = "";
            MqttClient client = new MqttClient("gpssensor.ddns.net");
            byte code = client.Connect(Guid.NewGuid().ToString());

            if (client.Subscribe(new string[] { "LASS/Test/+" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }) > 0)
            {
                output_text = "Subscribed successfully";
                client.MqttMsgSubscribed += client_MqttMsgSubscribed;
                client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            }
            else
            {
                output_text = "Subscribed failed";
            }
            var messageDialog = new MessageDialog(output_text);
            await messageDialog.ShowAsync();
        }
        void UpdateMqttList(string new_item)
        {
            mqttItem.Add(new MqttItem(new_item));
            ItemList.SelectedIndex = ItemList.Items.Count - 1;
            ItemList.ScrollIntoView(ItemList.SelectedItem);
        }
        void client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            Debug.WriteLine("Subscribed for id = " + e.MessageId);
        }

        async void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string message = Encoding.UTF8.GetString(e.Message);
            Debug.WriteLine("Received = " + message + " on topic " + e.Topic);
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                UpdateMqttList(message);
            });
        }
        public class MqttItem {
            public MqttItem() { }
            public MqttItem(string input_string)
            {
                InputString = input_string;
            }
            public string InputString { get; set; }
            public override string ToString()
            {
                return DateTime.Now.ToString("h:mm:ss tt") + ": " + InputString;
            }
        }
    }
}
