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

namespace View
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

        private void ConvertCelsius(object sender, RoutedEventArgs e)
        {
            var cel = double.Parse(celsiusBox.Text);
            var far = (cel * 1.8) + 32;
            var kel = cel + 273.15;

            fahrenheitBox.Text = "" + far;
            kelvinBox.Text = "" + kel;
        }

        private void ConvertFahrenheit(object sender, RoutedEventArgs e)
        {
            var far = double.Parse(fahrenheitBox.Text);
            var cel = (far - 32) * 0.5556;
            var kel = cel + 273.15;

            celsiusBox.Text = "" + cel;
            kelvinBox.Text = "" + kel;
        }

        private void ConvertKelvin(object sender, RoutedEventArgs e)
        {
            var kel = double.Parse(kelvinBox.Text);
            var cel = kel - 273.15;
            var far = (cel * 1.8) + 32;

            celsiusBox.Text = "" + cel;
            fahrenheitBox.Text = "" + far;
        }
    }
}
