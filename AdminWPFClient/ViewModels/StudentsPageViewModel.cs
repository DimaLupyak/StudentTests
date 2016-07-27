using AdminWPFClient.ServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminClient.ViewModels
{
    public class StudentsPageViewModel : INotifyPropertyChanged
    {        
        private StudentTestServiceClient service = new StudentTestServiceClient();

        public StudentsPageViewModel()
        {
            Students = service.GetStudents();
        }
        private StudentViewModel selectedStydent;
        public StudentViewModel SelectedStydent
        {
            get { return this.selectedStydent; }
            set
            {
                this.selectedStydent = value;
                this.SelectedStydentTests = service.GetStudentTests(selectedStydent.Id);
                this.OnPropertyChanged("SelectedStydent");
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

        private IEnumerable<TestViewModel> selectedStydentTests;
        public IEnumerable<TestViewModel> SelectedStydentTests
        {
            get { return this.selectedStydentTests; }
            set
            {
                this.selectedStydentTests = value;
                this.OnPropertyChanged("SelectedStydentTests");
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
