using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Model;
using Cells;

namespace ViewModel
{
    public class ConverterViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public  TemperatureScaleViewModel Kelvin { get; }
        public TemperatureScaleViewModel Celsius { get; }
        public TemperatureScaleViewModel Fahrenheit { get; }

        private double temperatureInKelvin;

        public ConverterViewModel()
        {
            this.Kelvin = new TemperatureScaleViewModel(this, new KelvinTemperatureScale());
            this.Celsius = new TemperatureScaleViewModel(this, new CelsiusTemperatureScale());
            this.Fahrenheit = new TemperatureScaleViewModel(this, new FahrenheitTemperatureScale());
        }
        public Cell<double> TemperatureInKelvin
        {
            get
            {
                return this.temperatureInKelvin;
            }
            set
            {
                this.temperatureInKelvin = value.Value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TemperatureInKelvin)));
            }
        }

        public IEnumerable<TemperatureScaleViewModel> Scales
        {
            get
            {
                yield return Celsius;
                yield return Fahrenheit;
                yield return Kelvin;
            }
        }
    }

    public class TemperatureScaleViewModel : INotifyPropertyChanged
    {
        private readonly ConverterViewModel parent;
        private readonly ITemperatureScale temperatureScale;

        public TemperatureScaleViewModel(ConverterViewModel parent, ITemperatureScale temperatureScale)
        {   
            this.parent = parent;
            this.temperatureScale = temperatureScale;

            this.parent.PropertyChanged += (sender, args) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Temperature)));
        }

        public string Name => temperatureScale.Name;

        public double Temperature {
            get
            {
                return temperatureScale.ConvertFromKelvin(parent.TemperatureInKelvin);
            }
            set
            {
                parent.TemperatureInKelvin = temperatureScale.ConvertToKelvin(value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
