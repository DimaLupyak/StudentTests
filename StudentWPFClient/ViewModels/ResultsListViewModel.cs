using StudentWpfClient.ServiceReference;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace StudentWpfClient.ViewModels
{
    class ResultsListViewModel : INotifyPropertyChanged
    {
        private StudentTestServiceClient service = new StudentTestServiceClient();

        public event PropertyChangedEventHandler PropertyChanged;

        public ResultsListViewModel(int studentId)
        {
            Results = service.GetResults()
                .Where(x => x.StudentId == studentId)
                .OrderBy(x => x.ResultDate)
                .Reverse();
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
        
    }
}
