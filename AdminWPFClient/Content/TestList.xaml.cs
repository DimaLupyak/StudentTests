using AdminClient.ViewModels;
using AdminWPFClient.ServiceReference;
using AdminWPFClient.ViewModels;
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
    public partial class TestList : UserControl
    {
        private TestListViewModel viewModel = new TestListViewModel();
        public TestList()
        {
            InitializeComponent();
            this.Loaded += (s, e) => { new Thread(InitializeData).Start(); };
            
        }
        void InitializeData()
        {
            viewModel = new TestListViewModel();
            this.Dispatcher.Invoke((Action)(() =>
            {
                this.DataContext = this.viewModel;
            }));
        }
        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            TestViewModel test = e.Row.DataContext as TestViewModel;
            if (test.Name != "" && test.Name != null)
            {
                if (test.Id == 0)
                    viewModel.CreateTest(test);
                else viewModel.UpdateTest(test);
            }
        }

    }
}
