using System.Windows;
using Digitamentos.ViewModels;

namespace Digitamentos
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void ToggleSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsSplitView.IsPaneOpen = !SettingsSplitView.IsPaneOpen;
        }
    }
}