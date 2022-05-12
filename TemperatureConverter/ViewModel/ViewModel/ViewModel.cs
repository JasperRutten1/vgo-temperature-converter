using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Model;
using Cells;
using System.Windows.Input;

namespace ViewModel
{
    public class ConverterViewModel
    {
        public Cell<double> TemperatureInKelvin { get; }
        public  TemperatureScaleViewModel Kelvin { get; }
        public TemperatureScaleViewModel Celsius { get; }
        public TemperatureScaleViewModel Fahrenheit { get; }

        public ConverterViewModel()
        {
            this.TemperatureInKelvin = new Cell<double>();
            this.Kelvin = new TemperatureScaleViewModel(this, new KelvinTemperatureScale());
            this.Celsius = new TemperatureScaleViewModel(this, new CelsiusTemperatureScale());
            this.Fahrenheit = new TemperatureScaleViewModel(this, new FahrenheitTemperatureScale());
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

    public class TemperatureScaleViewModel
    {
        private readonly ConverterViewModel parent;
        private readonly ITemperatureScale temperatureScale;

        public string Name => temperatureScale.Name;

        public Cell<double> Temperature { get; }

        public ICommand Add { get; }
        public ICommand Remove { get; }

        public TemperatureScaleViewModel(ConverterViewModel parent, ITemperatureScale temperatureScale)
        {   
            this.parent = parent;
            this.temperatureScale = temperatureScale;
            this.Temperature = this.parent.TemperatureInKelvin.Derive(
                kelvin => temperatureScale.ConvertFromKelvin(kelvin),
                t => temperatureScale.ConvertToKelvin(t)
            );
            this.Add = new AddCommand(this.Temperature, 1);
            this.Remove = new AddCommand(this.Temperature, -1);
        }
    }

    public class AddCommand : ICommand
    {
        private readonly Cell<double> cell;

        private int delta;

        public event EventHandler CanExecuteChanged;

        public AddCommand(Cell<double> cell, int delta)
        {
            this.cell = cell;
            this.delta = delta;
        }

        public bool CanExecute(object parameter)
        {
            var newValue = cell.Value + delta;
            return newValue >= 0 && newValue <= 1000;
        }

        public void Execute(object parameter)
        {
            cell.Value = Math.Round(cell.Value + delta);
        }
    }
}
