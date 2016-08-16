using AdminClient.ViewModels;
using AdminWPFClient.ServiceReference;
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
    /// Interaction logic for StudentsResults.xaml
    /// </summary>
    public partial class StudentsResults : UserControl
    {
        private StudentsResultsViewModel viewModel;
        public StudentsResults()
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

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var viewModel = this.DataContext as StudentsResultsViewModel;

            if(e.NewValue == null)
            {
                viewModel.SelectedStydent = null;
                viewModel.SelectedGroup = null;
            }            
            var group = e.NewValue as GroupViewModel;
            if (group != null)
            {
                viewModel.SelectedStydent = null;
                viewModel.SelectedGroup = group;
            }
            else
            {
                var student = e.NewValue as StudentViewModel;
                if (student != null)
                {
                    viewModel.SelectedGroup = null;
                    viewModel.SelectedStydent = student;
                }
            }
        }

        private void DataGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    DataGridRow dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;
                    if (dgr.IsMouseOver)
                    {
                        (dgr as DataGridRow).IsSelected = false;
                    }
                }
            }
        }
    }
}
