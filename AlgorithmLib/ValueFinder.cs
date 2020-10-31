using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AlgorithmLib
{
    public class ValueFinder
    {
        private char Cords { get; set; }
        private double Delta { get; set; }
        private double MinValue { get; set; }
        private string Data { get; set; }
        NumberPrinter Printer { get; set; }
        private bool IsUncheckedCoordinate { get; set; } = false;
        public ValueFinder(char cordsToFind, double delta, string dataToProcess, NumberPrinter printer, double minValue, LoggingDelegate loggingDelegate = null)
        {
            Cords = cordsToFind;
            Delta = delta;
            Data = dataToProcess;
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
        public string Process()
        {
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
                            var currentValue = Printer.Print(GetSummaryValue(collectedValue.ToString()));
                            result.Append(currentValue);
                            result.Append(letter);
                            Log( $"{Cords}{collectedValue.ToString()} -> {Cords}{currentValue}");
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
                Log( $"{e.Message}", LoggerState.Error);
                return Data;
            }

            return result.ToString();
        }

        private double GetSummaryValue(string v)
        {
            double value = double.Parse(v, NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);
            if (value >= MinValue || IsUncheckedCoordinate)
                value += Delta;
            return value;
        }
    }
}
