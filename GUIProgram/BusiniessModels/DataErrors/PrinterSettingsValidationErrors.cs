using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIProgram.BusiniessModels
{
    public partial class PrinterSettings : IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                Error = String.Empty;
                switch (columnName)
                {
                    case nameof(DisplayCount):
                        {
                            if (!IsStringAllowedInt(DisplayCount))
                                Error = "Значение не является допустимым числом";
                            break;
                        }
                }
                return Error;
            }
        }
        private bool IsStringAllowedInt(string data)
        {
            return data == "1" || data == "2" || data == "3";
        }
        public string Error { get; private set; }
    }
}
