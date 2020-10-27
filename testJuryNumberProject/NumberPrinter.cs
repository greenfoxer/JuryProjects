using System;
using System.Collections.Generic;
using System.Text;

namespace testJuryNumberProject
{
    public class NumberPrinter
    {
        bool IsDotNeed { get; set; }
        int DisplayAfterDot { get; set; }
        public NumberPrinter(bool isNeedDot, int countAfterDot)
        {
            IsDotNeed = isNeedDot;
            DisplayAfterDot = countAfterDot;
        }

        public string Print(double valueToPrint)
        {
            valueToPrint = (double)Math.Round(valueToPrint, DisplayAfterDot);
            string result = string.Empty;
            string pattern = $"F{DisplayAfterDot}";

            result = valueToPrint.ToString(pattern, System.Globalization.CultureInfo.InvariantCulture);
            result = result.TrimEnd('0');
            if (!IsDotNeed && (valueToPrint % 1) == 0)
            {
                result = result.Split('.')[0];
            }
            return result;
        }
    }
}
