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
    public partial class GroupList : UserControl
    {
        private GroupListViewModel viewModel;

        public GroupList()
        {
            InitializeComponent();
            this.Loaded += (s, e) => { new Thread(InitializeData).Start(); };

        }
        void InitializeData()
        {
            viewModel = new GroupListViewModel();
            this.Dispatcher.Invoke((Action)(() =>
            {
                this.DataContext = this.viewModel;
            }));
        }
        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            GroupViewModel group = e.Row.DataContext as GroupViewModel;
            if (group.Name != "" && group.Name != null)
            {
                if (group.Id == 0)
                    viewModel.CreateGroup(group);
                else viewModel.UpdateGroup(group);
            } 
        }
    }
}
