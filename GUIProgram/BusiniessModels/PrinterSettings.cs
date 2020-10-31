using AlgorithmLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIProgram.BusiniessModels
{
    public partial class PrinterSettings
    {
        public bool IsUseDot { get; set; }
        public string DisplayCount { get; set; }
        public int Count { get; private set; } = 1;
        public PrinterSettings()
        {
            DisplayCount = Count.ToString();
        }
        public NumberPrinter GeneratePrinter(LoggingDelegate loggingDelegate = null)
        {
            Count =  int.Parse(DisplayCount);
            return new NumberPrinter(IsUseDot, Count, loggingDelegate);
        }
    }
}
