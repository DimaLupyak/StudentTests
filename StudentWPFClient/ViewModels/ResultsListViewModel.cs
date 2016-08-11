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
    class ResultsListViewModel : INotifyPropertyChanged
    {
        private StudentTestServiceClient service = new StudentTestServiceClient();

        public ResultsListViewModel(int studentId)
        {
            Results = service.GetResults().Where(x => x.StudentId == studentId);
        }


        private IEnumerable<ResultViewModel> results;
        public IEnumerable<ResultViewModel> Results
        {
            get { return this.results; }
            set
            {
                this.results = value;
                this.OnPropertyChanged("Results");
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
