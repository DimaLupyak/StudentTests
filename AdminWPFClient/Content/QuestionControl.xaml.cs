using AdminWPFClient.ServiceReference;
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
    /// Interaction logic for QuestionControl.xaml
    /// </summary>
    public partial class QuestionControl : UserControl
    {
        public QuestionControl()
        {
            InitializeComponent();
        }
        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            AnswerViewModel answer = e.Row.DataContext as AnswerViewModel;
            if (answer.Text != "" && answer.Text != null && answer.Image != null)
            {
                //if (answer.Id == 0)
                //    viewModel.CreateStudent(answer);
                //else viewModel.UpdateStudent(answer);
            }
        }

    }
}
