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

using System.Threading;
using Newtonsoft.Json;

namespace Client1
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Listener l;
        Producer p;

        public MainWindow()
        {
            InitializeComponent();
            l = new Listener(this);
            p = new Producer();
            Thread t = new Thread(new ThreadStart(l.listen));
            t.Start();
        }
        
        public void updateText(string txt)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                this.uppercased.Text = txt;
            });
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            p.send(JsonConvert.SerializeObject(this.uppercasemi.Text));
        }
    }
}
