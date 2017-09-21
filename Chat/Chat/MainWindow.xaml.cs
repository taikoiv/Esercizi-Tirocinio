using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Threading;

//IMPORT ACTIVEMQ LIBRARY

using Apache.NMS;

//IMPORT JSON LIBRARY
using Newtonsoft.Json;


namespace Chat
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<ChatMessage> ChatList { get; set; }
        private Publisher p;
        private Listener l;

        public MainWindow()
        {
            InitializeComponent();
            l = new Listener(this);
            p = new Publisher();
            ChatList = new ObservableCollection<ChatMessage>();
            this.chatContentView.ItemsSource = ChatList;
            Thread t1 = new Thread(new ThreadStart(l.listen));
            t1.Start();
        }

        private void sendBtn_Click(object sender, RoutedEventArgs e)
        {
            string chtContent = contentTextBox.Text;
            if (!string.IsNullOrEmpty(chtContent))
            {
                ChatMessage chat = new ChatMessage() { Text = chtContent,  Nick = "Umberto" };
                ChatList.Add(chat);
                string jsonString = JsonConvert.SerializeObject(chat); //CREATE A JSON STRING FOR THIS OBJECT
                p.publish(jsonString);
                contentTextBox.Text = string.Empty;
            }
        }

    public void updateCollection(ChatMessage chat)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                ChatList.Add(chat);
            });
        }
    }
}
