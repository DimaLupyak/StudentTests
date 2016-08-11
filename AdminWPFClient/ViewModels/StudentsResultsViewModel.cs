using AdminWPFClient.ServiceReference;
using AdminWPFClient.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdminClient.ViewModels
{
    public class StudentsResultsViewModel : INotifyPropertyChanged
    {        
        private StudentTestServiceClient service = new StudentTestServiceClient();

        public StudentsResultsViewModel()
        {
            Students = service.GetStudents();
            Tests = service.GetTests();
            Results = service.GetResults().Select(x=> new ResultControlViewModel(x));
        }
        private StudentViewModel selectedStydent;
        public StudentViewModel SelectedStydent
        {
            get { return this.selectedStydent; }
            set
            {
                this.selectedStydent = value;
                new Thread(() => Results = service.GetResults().Where(x => x.StudentId == value.Id)
                    .Select(x => new ResultControlViewModel(x))).Start();                
                this.OnPropertyChanged("SelectedStydent");
            }
        }
        private TestViewModel selectedTest;
        public TestViewModel SelectedTest
        {
            get { return this.selectedTest; }
            set
            {
                this.selectedTest = value;
                new Thread(() => Results = service.GetResults().Where(x => x.TestId == value.Id)
                    .Select(x => new ResultControlViewModel(x))).Start();
                this.OnPropertyChanged("SelectedTest");
            }
        }
        private IEnumerable<StudentViewModel> students;
        public IEnumerable<StudentViewModel> Students
        {
            get { return this.students; }
            set
            {
                this.students = value;
                
                this.OnPropertyChanged("Students");
            }
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
        private IEnumerable<ResultControlViewModel> results;
        public IEnumerable<ResultControlViewModel> Results
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
            if(this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
