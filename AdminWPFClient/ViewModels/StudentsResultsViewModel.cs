using AdminWPFClient.ServiceReference;
using AdminWPFClient.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;

namespace AdminClient.ViewModels
{
    public class StudentsResultsViewModel : INotifyPropertyChanged
    {
        #region Private Variables
        private StudentTestServiceClient service = new StudentTestServiceClient();
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructor
        public StudentsResultsViewModel()
        {
            Students = service.GetStudents();
            Tests = service.GetTests();
            Results = service.GetResults()
                .OrderBy(x => x.ResultDate)
                .Reverse()
                .Select(x => new ResultControlViewModel(x));
            Groups = service.GetGroups();
        }
        #endregion

        #region Properties
        private StudentViewModel selectedStydent;
        public StudentViewModel SelectedStydent
        {
            get { return this.selectedStydent; }
            set
            {
                this.selectedStydent = value;
                FilterResults();
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
                FilterResults();
                this.OnPropertyChanged("SelectedTest");
            }
        }

        private GroupViewModel selectedGroup;
        public GroupViewModel SelectedGroup
        {
            get { return this.selectedGroup; }
            set
            {
                this.selectedGroup = value;
                FilterResults();
                this.OnPropertyChanged("SelectedGroup");
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

        private IEnumerable<GroupViewModel> groups;
        public IEnumerable<GroupViewModel> Groups
        {
            get { return this.groups; }
            set
            {
                this.groups = value;
                this.OnPropertyChanged("Groups");
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
                FilterResults();
                this.OnPropertyChanged("Results");
            }
        }        

        private IEnumerable<ResultControlViewModel> filteredResults;
        public IEnumerable<ResultControlViewModel> FilteredResults
        {
            get { return this.filteredResults; }
            set
            {
                this.filteredResults = value;
                this.OnPropertyChanged("FilteredResults");
            }
        }
        #endregion

        #region Public Methods
        private void FilterResults()
        {
            new Thread(() => FilteredResults = results.Where(x => IsSelect(x))).Start();
        }
        #endregion

        #region Private Methods
        private bool IsSelect(ResultControlViewModel x)
        {
            if (selectedTest != null && x.Result.TestId != selectedTest.Id)
                return false;
            if (selectedGroup != null && selectedStydent == null && x.Student.GroupID != selectedGroup.Id)
                return false;
            if (selectedStydent != null && x.Result.StudentId != selectedStydent.Id)
                return false;
            return true;
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
