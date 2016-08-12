using StudentWpfClient.ViewModels;
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

namespace StudentWpfClient.Pages
{
    /// <summary>
    /// Interaction logic for TestingPage.xaml
    /// </summary>
    public partial class TestingPage : UserControl
    {
        public TestingPage()
        {
            InitializeComponent();
        }

        private void TestingPageKeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                switch (e.Key)
                {
                    case Key.Left:
                        if ((this.DataContext as TestingViewModel).PreviousQuestionCommand.CanExecute(null))
                            (this.DataContext as TestingViewModel).PreviousQuestionCommand.Execute(null);
                        break;
                    case Key.Right:
                        if ((this.DataContext as TestingViewModel).NextQuestionCommand.CanExecute(null))
                            (this.DataContext as TestingViewModel).NextQuestionCommand.Execute(null);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
