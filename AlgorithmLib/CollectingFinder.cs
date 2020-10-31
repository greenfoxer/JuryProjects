using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AlgorithmLib
{
    public class CollectingFinder : IAlgorithm
    {
        private char Cords { get; set; }
        private double Delta { get; set; }
        private double MinValue { get; set; }
        private string Data { get; set; }
        NumberPrinter Printer { get; set; }
        private bool IsUncheckedCoordinate { get; set; } = false;
        public CollectingFinder(char cordsToFind, double delta, NumberPrinter printer, double minValue, LoggingDelegate loggingDelegate = null)
        {
            Cords = cordsToFind;
            Delta = delta;
            Printer = printer;
            MinValue = minValue;
            Logger += loggingDelegate;
            if (Cords == 'Z')
                IsUncheckedCoordinate = true;
        }
        public LoggingDelegate Logger;
        void Log(string message, LoggerState state = LoggerState.Info)
        {
            if (Logger != null)
                Logger("ValueFinder", new AlgorithmLibEventArgs(state, message));
        }
        Dictionary<string, double> StubValueDictionary;
        int Counter;
        public string Process(string dataToProcess)
        {
            Data = dataToProcess;
            StubValueDictionary = new Dictionary<string, double>();
            Counter = 0;
            StringBuilder result = new StringBuilder();

            bool stateCollectNumber = false;
            StringBuilder collectedValue = new StringBuilder();
            try
            {
                foreach (var letter in Data)
                {
                    if (stateCollectNumber)
                    {
                        if (!char.IsWhiteSpace(letter) && !Environment.NewLine.Contains(letter) && !char.IsLetter(letter))
                            collectedValue.Append(letter);
                        else
                        {
                            stateCollectNumber = false;
                            string stub = $"{{{Cords}{Counter++}}}";
                            double value = double.Parse(collectedValue.ToString(), NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);
                            StubValueDictionary.Add(stub, value);
                            result.Append(stub);
                            result.Append(letter);
                            Log($"{stub} -> {value}");
                            collectedValue.Clear();
                        }
                    }
                    else
                        result.Append(letter);

                    if (letter == Cords)
                        stateCollectNumber = true;
                }
            }
            catch (Exception e)
            {
                Log($"{e.Message}", LoggerState.Error);
                return Data;
            }

            return result.ToString();
        }
        public void DoTransform()
        {
            var output = new Dictionary<string, double>();
            foreach (string key in StubValueDictionary.Keys)
            {
                output.Add(key,GetSummaryValue(key));
            }
            StubValueDictionary = output;
        }
        private double GetSummaryValue(string key)
        {
            double value = StubValueDictionary[key];
            if (value >= MinValue || IsUncheckedCoordinate)
                value += Delta;
            return value;
        }
        public Dictionary<string, double> GetCoordinateArray()
        {
            return StubValueDictionary;
        }
    }
}
