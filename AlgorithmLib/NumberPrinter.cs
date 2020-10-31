using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmLib
{
    public class NumberPrinter
    {
        bool IsDotNeed { get; set; }
        int DisplayAfterDot { get; set; }
        public NumberPrinter(bool isNeedDot, int countAfterDot, LoggingDelegate loggingDelegate = null)
        {
            IsDotNeed = isNeedDot;
            DisplayAfterDot = countAfterDot;
            Logger += loggingDelegate;
        }
        public LoggingDelegate Logger;
        void Log(string message, LoggerState state = LoggerState.Info)
        {
            if (Logger != null)
                Logger("NumberPrinter", new AlgorithmLibEventArgs(state, message));
        }
        public string Print(double valueToPrint)
        {
            string result = string.Empty;
            try
            {
                valueToPrint = (double)Math.Round(valueToPrint, DisplayAfterDot);
                string pattern = $"F{DisplayAfterDot}";

                result = valueToPrint.ToString(pattern, System.Globalization.CultureInfo.InvariantCulture);
                result = result.TrimEnd('0');
                if (!IsDotNeed && (valueToPrint % 1) == 0)
                {
                    result = result.Split('.')[0];
                }
            }
            catch (Exception e)
            {
                Log(e.Message, LoggerState.Error);
                return valueToPrint.ToString();
            }
            return result;
        }
    }
}
