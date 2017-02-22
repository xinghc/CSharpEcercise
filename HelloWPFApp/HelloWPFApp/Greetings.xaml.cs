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

namespace HelloWPFApp
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

        private void RadioButton1_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton2_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (RadioButton1.IsChecked == true)
            {
                MessageBox.Show("Hello");
            }
            else if (RadioButton2.IsChecked == true)
            {
                RadioButton2.IsChecked = true;
                MessageBox.Show("Goodbye");
            }
            else
            {
                MessageBox.Show("You should choose one of the option above!");
            }
        }
    }
}
