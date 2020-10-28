using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace testJuryNumberProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //// ГОТОВАЯ ПРОГРАММА
            /*
            if (args.Length != 5)
            {
                Console.WriteLine("Неверное количество параметров. Нужно 6: 1.dot{0,1} 2.количество знаков{1,2,3} 3.delta X {число с точкой} 4.delta Y {число с точкой} 5. delta Z {число с точкой} 6. Путь к файлу");
                return;
            }
            bool isNeedDot = true;
            if (args[0] == "1")
                isNeedDot = true;
            else if (args[0] == "0")
                isNeedDot = false;
            else
            {
                Console.WriteLine("Неверное значение параметра IsNeedDot");
            }

            int countAfterDor = int.Parse(args[1]);

            var printer = new NumberPrinter(isNeedDot, countAfterDor);

            double deltaX = double.Parse(args[2], NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);
            double deltaY = double.Parse(args[3], NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);
            double deltaZ = double.Parse(args[4], NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);

            string Path = args[5]; // открыть и прочитать файл в FileContent
            string FileContent = "G0 X500.2365 Y300.12354 Z-50.25648 F2500";

            string result = FileContent;

            result = StrategyForCoordinateStart('X', result, deltaX, printer);
            result = StrategyForCoordinateStart('Y', result, deltaY, printer);
            result = StrategyForCoordinateStart('Z', result, deltaZ, printer);

            Console.WriteLine(result);*/
            ///ТЕСТОВАЯ ПРОГРАММА
            string FileContent = @"X500.2365
G0 X500.2365 Y300.12354 Z-50.25648 F2500 G0 X500.2365 Y300.12354 Z-50.25648
G0 X500.2365 Y300.12354 Z-50.25648 F2500";
            var printer = new NumberPrinter(true, 2);
            string result = FileContent;
            result = StrategyForCoordinateStart('X', result, -100, printer, 500);
            result = StrategyForCoordinateStart('Y', result, 100, printer, 400);
            result = StrategyForCoordinateStart('Z', result, 0, printer, 0);
            Console.WriteLine(result);
        }
        static string StrategyForCoordinateStart(char cords, string data, double delta, NumberPrinter printer, double minValue)
        {
            var parser = new ValueFinder(cords, delta, data, printer, minValue);
            return parser.Process();
        }
    }
}
