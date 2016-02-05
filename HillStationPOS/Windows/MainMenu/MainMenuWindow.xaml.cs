using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
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

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            this.SaveSettings(false);
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            this.LoadSettings(false);
            var buttons = Shared.GetChildren<Button>(this);
            foreach (var button in buttons)
            {
                button.IsEnabled = true;
            }
            TxtStatus.Visibility = Visibility.Collapsed;
        }
    }
}