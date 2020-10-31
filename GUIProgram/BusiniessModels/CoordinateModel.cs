using AlgorithmLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIProgram.BusiniessModels
{
    public partial class CoordinateModel
    {
        [Description("Координата: ")]
        public char Litera { get; set; }
        [Description("Минимальное значение: ")]
        public string DisplayMinimumValue { get; set; }
        public double MinimumValue { get; private set; }
        [Description("Дельта: ")]
        public string DisplayDelta { get; set; }
        public double Delta { get; private set; }
        public bool IsSelected { get; set; }
        public CoordinateModel(char coordinate)
        {
            Litera = coordinate;
            DisplayMinimumValue = MinimumValue.ToString();
            DisplayDelta = Delta.ToString();
        }
        public string Process(string data, NumberPrinter printer, LoggingDelegate loggingDelegate = null)
        {
            Delta = double.Parse(DisplayDelta, NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);
            MinimumValue = double.Parse(DisplayMinimumValue, NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);
            var parser = new ValueFinder(Litera, Delta, data, printer, MinimumValue, loggingDelegate);
            return parser.Process();
        }
    }
}
