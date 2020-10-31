using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmLib
{
    public delegate void LoggingDelegate(string sender, AlgorithmLibEventArgs e);
    public enum LoggerState
    { 
        Info,
        Error
    }
    public class AlgorithmLibEventArgs
    {
        public string message;
        public LoggerState state;
        public AlgorithmLibEventArgs( LoggerState _state, string _mesage)
        {
            message = _mesage;
            state = _state;
        }
    }
}
