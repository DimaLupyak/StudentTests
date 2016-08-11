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

namespace StudentWpfClient.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class LogonPage : UserControl
    {

        private LogonPageViewModel viewModel;
        public LogonPage()
        {
            InitializeComponent();
            this.DataContext = null;
            this.Loaded += (s, e) => { new Thread(InitializeData).Start(); };

        }
        void InitializeData()
        {
            viewModel = new LogonPageViewModel();
            this.Dispatcher.Invoke((Action)(() =>
            {
                this.DataContext = this.viewModel;
            }));
        }
    }
}
