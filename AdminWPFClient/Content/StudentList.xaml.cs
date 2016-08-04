using AdminClient.ViewModels;
using AdminWPFClient.ServiceReference;
using FirstFloor.ModernUI.Windows.Controls;
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
    /// Interaction logic for StudentList.xaml
    /// </summary>
    public partial class StudentList : UserControl
    {
        private StudentsListViewModel viewModel;

        public StudentList()
        {
            InitializeComponent();
            this.Loaded += (s, e) => { new Thread(InitializeData).Start(); };
            this.IsVisibleChanged += (s, e) =>
            {
                if ((bool)e.NewValue && viewModel!=null)
                    viewModel.RefreshGroups();
            };

        }
        void InitializeData()
        {
            viewModel = new StudentsListViewModel();
            this.Dispatcher.Invoke((Action)(() =>
            {
                this.DataContext = this.viewModel;
            }));
        }
        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            StudentViewModel student = e.Row.DataContext as StudentViewModel;
            if (student.Name != "" && student.Name != null && student.GroupID != 0)
            {
                if (student.Id == 0)
                    viewModel.CreateStudent(student);
                else viewModel.UpdateStudent(student);
            } 
        }
    }
}
