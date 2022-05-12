using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Data;
using System;
using System.Globalization;
using System.ComponentModel;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private double temperatureInKelvin;
        public double TemperatureInKelvin {
            get
            {
                return this.temperatureInKelvin;
            }
            set
            {
                this.temperatureInKelvin = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TemperatureInKelvin)));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class TemperatureConverter : IValueConverter
    {
        public ITemperatureScale TemperatureScale { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.TemperatureScale.ConvertFromKelvin((double)value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (string)value;
            var temp = 0.0;
            if (val.Length > 0 && val != "-")temp = double.Parse(val);
            return this.TemperatureScale.ConvertToKelvin(temp);
        }
    }
}
