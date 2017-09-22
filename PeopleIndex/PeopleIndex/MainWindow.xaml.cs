using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PeopleIndex
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Person> people;
        private Producer p;
        private Listener l;

        public MainWindow()
        {
            InitializeComponent();
            people = new ObservableCollection<Person>();
            this.peopleContainer.ItemsSource = people;
            p = new Producer();
            l = new Listener(this);
            new Thread(new ThreadStart(l.listen)).Start();
        }

        public void updateIndex(Person p)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                this.people.Add(p);
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(this.Name.Text) && !string.IsNullOrEmpty(this.Surname.Text) && !string.IsNullOrEmpty(this.Address.Text))
                p.send(new Person { Name = this.Name.Text, Surname = this.Surname.Text, Address = this.Address.Text });
            else MessageBox.Show("You must fill all fields", "It's not a person");
        }
    }
}
