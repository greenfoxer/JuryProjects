using GUIProgram.Infrastructure;
using GUIProgram.ViewModels;
using GUIProgram.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GUIProgram
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainView main = new MainView();
            main.DataContext = new MainViewModel();
            main.Show();
        }
    }
}
