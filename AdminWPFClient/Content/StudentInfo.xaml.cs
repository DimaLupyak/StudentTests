using AdminClient.ViewModels;
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

namespace AdminWPFClient.Content
{
    /// <summary>
    /// Interaction logic for StudentsInfo.xaml
    /// </summary>
    public partial class StudentsInfo : UserControl
    {
        private StudentsResultsViewModel viewModel;
        public StudentsInfo()
        {
            InitializeComponent();
            this.Loaded += (s, e) => { new Thread(InitializeData).Start(); };
            
        }
        void InitializeData()
        {
            viewModel = new StudentsResultsViewModel();
            this.Dispatcher.Invoke((Action)(() =>
            {
                this.DataContext = this.viewModel;
            }));            
        }
    }
}
