using AdminClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private StudentsPageViewModel viewModel = new StudentsPageViewModel();
        public StudentsInfo()
        {
            InitializeComponent();
            this.Loaded += (s, e) => { this.DataContext = this.viewModel; };
            
        }
    }
}
