using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace testJuryNumberProject
{
    public class ValueFinder
    {
        private char Cords { get; set; }
        private double Delta { get; set; }
        private string Data { get; set; }
        NumberPrinter Printer { get; set; }
        public ValueFinder(char cordsToFind, double delta, string dataToProcess, NumberPrinter printer)
        {
            Cords = cordsToFind;
            Delta = delta;
            Data = dataToProcess;
            Printer = printer;
        }

        public string Process()
        {
            StringBuilder result = new StringBuilder();

            bool stateCollectNumber = false;
            StringBuilder collectedValue = new StringBuilder();

            foreach (var letter in Data)
            {
                if (stateCollectNumber)
                {
                    if (!char.IsWhiteSpace(letter))
                        collectedValue.Append(letter);
                    else
                    {
                        stateCollectNumber = false;
                        var currentValue = Printer.Print(GetSummaryValue(collectedValue.ToString()));
                        result.Append(currentValue);
                        result.Append(letter);
                    }
                }
                else
                    result.Append(letter);

                if (letter == Cords)
                    stateCollectNumber = true;
            }

            return result.ToString();
        }

        private double GetSummaryValue(string v)
        {
            double value = double.Parse(v, NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);
            value += Delta;
            return value;
        }
    }
}
