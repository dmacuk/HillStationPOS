using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using HillStationPOS.Services;
using HillStationPOS.ViewModel;
using Utils.Window;
using Utils.Window.Utils;

namespace HillStationPOS.Windows.MainMenu
{
    /// <summary>
    ///     Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        public MainMenuWindow()
        {
            InitializeComponent();
        }

        private void ExitProgram(object sender, RoutedEventArgs e)
        {
            ViewModelLocator.Cleanup();
            Close();
        }

        private async void WindowLoaded(object sender, RoutedEventArgs e)
        {
            this.LoadSettings(false);
            var loaded = await DataService.Instance.Initialise();
            var buttons = Shared.GetChildren<Button>(this);
            foreach (var button in buttons)
            {
                button.IsEnabled = loaded;
            }
            TxtStatus.Visibility = Visibility.Collapsed;
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            this.SaveSettings(false);
        }
    }
}