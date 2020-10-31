using AlgorithmLib;
using GUIProgram.BusiniessModels;
using GUIProgram.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUIProgram.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        IDialogService DialogService;
        private CoordinateModel _x, _y, _z;
        public CoordinateModel XCoordinate { get { return _x; } set { SetProperty(ref _x, value); } }
        public CoordinateModel YCoordinate { get { return _y; } set { SetProperty(ref _y, value); } }
        public CoordinateModel ZCoordinate { get { return _z; } set { SetProperty(ref _z, value); } }
        string _source, _log;
        public string SourceFileName { get { return _source; } set { SetProperty(ref _source, value); } }
        public string LogInfo { get { return _log; } set { SetProperty(ref _log, value); } }
        PrinterSettings _printer;
        public PrinterSettings PrinterSettings { get { return _printer; } set { SetProperty(ref _printer, value); } }
        int _step;
        public int Step { get { return _step; } set { SetProperty(ref _step, value); } }
        public bool IsModelValid
        {
            get 
            {
                if (string.IsNullOrEmpty(XCoordinate.Error) 
                    && string.IsNullOrEmpty(YCoordinate.Error) 
                    && string.IsNullOrEmpty(ZCoordinate.Error) 
                    && string.IsNullOrEmpty(PrinterSettings.Error) 
                    && isFileSelected)
                    return true;
                return false;
            }
        }
        public MainViewModel()
        {
            DialogService = new DefaultDialogService();
            XCoordinate = new CoordinateModel('X');
            YCoordinate = new CoordinateModel('Y');
            ZCoordinate = new CoordinateModel('Z');
            CoordinateModel.SetLogger(Log);
            SourceFileName = "Файл не выбран";
            PrinterSettings = new PrinterSettings();
            isFileSelected = false;
        }
        bool isFileSelected;
        void Log(string message)
        {
            LogInfo += message + Environment.NewLine;
        }
        void Log(string sender, AlgorithmLibEventArgs e)
        {
            Log("\t"+e.message);
        }
        private void PickSourceFile()
        {
            if (DialogService.OpenFileDialog())
            {
                LogInfo = string.Empty;
                Step = 0;
                SourceFileName = DialogService.FilePath;
                isFileSelected = true;
            }
        }
        private void DoWork()
        {
            LogInfo = string.Empty;
            Step = 0;
            Log("Начало обработки файла.");
            var printer = PrinterSettings.GeneratePrinter(Log);
            string data = File.ReadAllText(SourceFileName);
            if (XCoordinate.IsSelected)
            {
                Log("Обработка координаты X");
                Thread.Sleep(1500);
                data = XCoordinate.Process(data, printer);
            }
            Step = 1;
            if (YCoordinate.IsSelected)
            {
                Log("Обработка координаты Y");
                Thread.Sleep(1500);
                data = YCoordinate.Process(data, printer);
            }
            Step = 2;
            if (ZCoordinate.IsSelected)
            {
                Log("Обработка координаты Z");
                Thread.Sleep(1500);
                data = ZCoordinate.Process(data, printer);
            }
            Step = 3;
            if (DialogService.SaveFileDialog())
            {
                Log($"Запись результата в {DialogService.FilePath}");
                File.WriteAllText(DialogService.FilePath, data);
            }
            else
                Log($"Файл не выбран, запись не произведена!");
            Log($"Работа завершена");
        }
        private void DoArrayWork()
        {
            LogInfo = string.Empty;
            Step = 0;
            Log("Начало обработки файла.");
            var printer = PrinterSettings.GeneratePrinter(Log);
            string data = File.ReadAllText(SourceFileName);
            if (XCoordinate.IsSelected)
            {
                Log("Обработка координаты X");
                Thread.Sleep(1500);
                data = XCoordinate.ProcessWithArray(data, printer);
            }
            Step = 1;
            if (YCoordinate.IsSelected)
            {
                Log("Обработка координаты Y");
                Thread.Sleep(1500);
                data = YCoordinate.ProcessWithArray(data, printer);
            }
            Step = 2;
            if (ZCoordinate.IsSelected)
            {
                Log("Обработка координаты Z");
                Thread.Sleep(1500);
                data = ZCoordinate.ProcessWithArray(data, printer);
            }
            Step = 3;
            if (XCoordinate.IsSelected)
                data = XCoordinate.InitArrayProcessLogic(data,printer);
            if (YCoordinate.IsSelected)
                data = YCoordinate.InitArrayProcessLogic(data, printer);
            if (ZCoordinate.IsSelected)
                data = ZCoordinate.InitArrayProcessLogic(data, printer);
            if (DialogService.SaveFileDialog())
            {
                Log($"Запись результата в {DialogService.FilePath}");
                File.WriteAllText(DialogService.FilePath, data);
            }
            else
                Log($"Файл не выбран, запись не произведена!");
            Log($"Работа завершена");
        }
        #region BackgroundWorker
        private readonly BackgroundWorker worker = new BackgroundWorker();
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            DoWork();
        }
        private void worker_DoArrayWork(object sender, DoWorkEventArgs e)
        {
            DoArrayWork();
        }
        #endregion
        #region Commands
        private ICommand _command;
        public ICommand TestCommand
        {
            get
            {
                this._command = new RelayCommand(
                    (object param) =>
                    {
                        worker.DoWork += worker_DoWork;
                        worker.RunWorkerAsync();
                    }, (object param) => { return IsModelValid;  }
                    );
                return this._command;
            }
            set
            {
                this._command = value;
            }
        }
        public ICommand ArrayCommand
        {
            get
            {
                this._command = new RelayCommand(
                    (object param) =>
                    {
                        worker.DoWork += worker_DoArrayWork;
                        worker.RunWorkerAsync();
                    }, (object param) => { return IsModelValid; }
                    );
                return this._command;
            }
            set
            {
                this._command = value;
            }
        }
        public ICommand PickFileCommand
        {
            get
            {
                this._command = new RelayCommand(
                    (object param) =>
                    {
                        PickSourceFile();
                    }, (object param) => { return true; }
                    );
                return this._command;
            }
            set
            {
                this._command = value;
            }
        }
        #endregion
    }
}
