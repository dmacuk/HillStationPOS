using System.ComponentModel;
using System.Windows;
using HillStationPOS.ViewModel;
using Utils.Window;

namespace HillStationPOS
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private MainViewModel _model;

        /// <summary>
        ///     Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            _model = (MainViewModel) DataContext;
            _model.OrderItemAdded += OrderItemAdded;

            this.LoadSettings(false);
        }

        private void OrderItemAdded(object sender, OrderItemAddedEventArgs e)
        {
            OrderGrid.SelectedItem = e.OrderItemAdded;
            OrderGrid.ScrollIntoView(e.OrderItemAdded);
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            this.SaveSettings(false);
        }

        private void StartEdit(object sender, RoutedEventArgs e)
        {
            OrderGrid.BeginEdit();
        }
    }
}