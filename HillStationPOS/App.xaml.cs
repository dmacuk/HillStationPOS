using GalaSoft.MvvmLight.Threading;
using System.Windows;
using System.Windows.Threading;
using Utils.Preference;

namespace HillStationPOS
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            PreferenceManager.SavePreferences();
            base.OnExit(e);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            PreferenceManager.GetInstance("Orders.prefs");
            base.OnStartup(e);
        }

        private void UnHandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Oops");
        }
    }
}