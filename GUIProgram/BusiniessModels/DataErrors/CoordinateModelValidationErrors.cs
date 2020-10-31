using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIProgram.BusiniessModels
{
    public partial class CoordinateModel : IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                Error = String.Empty;
                switch (columnName)
                {
                    case nameof(DisplayDelta):
                        {
                            if (!IsStringDigitAndDot(DisplayDelta))
                                Error = "Значение не является числом с точкой";
                            break;
                        }
                    case nameof(DisplayMinimumValue):
                        {
                            if (!IsStringDigitAndDot(DisplayMinimumValue))
                                Error = "Значение не является числом с точкой";
                            break;
                        }
                }
                return Error;
            }
        }
        private bool IsStringDigitAndDot(string data)
        {
            return double.TryParse(data, NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double value);
        }
        public string Error { get; private set; }
    }
}
