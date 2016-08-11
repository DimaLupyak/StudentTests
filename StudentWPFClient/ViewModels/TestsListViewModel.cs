using StudentWpfClient.ServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StudentWpfClient.ViewModels
{
    class TestsListViewModel : INotifyPropertyChanged
    {
        private StudentTestServiceClient service = new StudentTestServiceClient();

        public TestsListViewModel(int studentId)
        {
            Tests = service.GetStudentTests(studentId);
        }


        private IEnumerable<TestViewModel> tests;
        public IEnumerable<TestViewModel> Tests
        {
            get { return this.tests; }
            set
            {
                this.tests = value;
                this.OnPropertyChanged("Tests");
            }
        }
        
        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
