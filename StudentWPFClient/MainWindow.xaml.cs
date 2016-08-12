using FirstFloor.ModernUI.Windows.Controls;
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
            this.Loaded += (s, e) => { new Thread(InitializeData).Start(); };
        }
        void InitializeData()
        {
            viewModel = new MainWindowViewModel();
            this.Dispatcher.Invoke((Action)(() =>
            {
                this.DataContext = new MainWindowViewModel();
            }));
        }
    }
}
