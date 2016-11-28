using System.Windows;
using Client.ViewModels;

namespace Client.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DownloadOpenFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel model = (MainWindowViewModel)DataContext;
            model.DownloadOpenFile();
        }

        private void RefreshListButton_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel model = (MainWindowViewModel)DataContext;
            model.RefreshList();
        }

        private void ClearDownloadsButton_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel model = (MainWindowViewModel)DataContext;
            model.ClearDownloads();
        }

    }
}