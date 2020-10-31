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
        static LoggingDelegate LoggingDelegate;
        public static void SetLogger(LoggingDelegate loggingDelegate = null)
        {
            LoggingDelegate = loggingDelegate;
        }
        IAlgorithm _parser;
        public string Process(string data, NumberPrinter printer)
        {
            Delta = double.Parse(DisplayDelta, NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);
            MinimumValue = double.Parse(DisplayMinimumValue, NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);
            _parser = new ValueFinder(Litera, Delta, printer, MinimumValue, LoggingDelegate);
            return _parser.Process(data);
        }
        Dictionary<string, double> StubValueDictionary;
        public string ProcessWithArray(string data, NumberPrinter printer)
        {
            Delta = double.Parse(DisplayDelta, NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);
            MinimumValue = double.Parse(DisplayMinimumValue, NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);
            _parser = new CollectingFinder(Litera, Delta, printer, MinimumValue, LoggingDelegate);
            return _parser.Process(data);
        }
        public string InitArrayProcessLogic(string data, NumberPrinter printer)
        {
            var _currentParser = (_parser as CollectingFinder);
            _currentParser.DoTransform();
            StubValueDictionary = _currentParser.GetCoordinateArray();
            foreach (string key in StubValueDictionary.Keys)
            {
                data = data.Replace(key, printer.Print(StubValueDictionary[key]) );
            }
            return data;
        }
    }
}
