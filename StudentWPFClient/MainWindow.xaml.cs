using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using StudentWpfClient.Properties;
using StudentWpfClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentWpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        private MainWindowViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow = this;
            this.Closed += ModernWindow_Closed;
            this.Loaded += ModernWindow_Loaded;
        }
        void InitializeData()
        {
            viewModel = new MainWindowViewModel();
            this.Dispatcher.Invoke((Action)(() =>
            {
                this.DataContext = new MainWindowViewModel();
            }));
        }

        private void ModernWindow_Closed(object sender, EventArgs e)
        {
            Settings.Default.ChosenFontSize = AppearanceManager.Current.FontSize;
            Settings.Default.ChosenAccent = AppearanceManager.Current.AccentColor;
            Settings.Default.Save();
        }

        private void ModernWindow_Loaded(object sender, RoutedEventArgs e)
        {
            new Thread(InitializeData).Start();
            AppearanceManager.Current.FontSize = Settings.Default.ChosenFontSize;
            AppearanceManager.Current.AccentColor = Settings.Default.ChosenAccent;
        }
    }
}
