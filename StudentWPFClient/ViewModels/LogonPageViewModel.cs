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
    class LogonPageViewModel : INotifyPropertyChanged
    {
        private StudentTestServiceClient service = new StudentTestServiceClient();

        public LogonPageViewModel()
        {
            Groups = service.GetGroups();
           
        }

        private GroupViewModel selectedGroup;
        public GroupViewModel SelectedGroup
        {
            get { return this.selectedGroup; }
            set
            {
                this.selectedGroup = value;
                Students = service.GetStudents().Where(x => x.GroupID == value.Id);
                this.OnPropertyChanged("SelectedGroup");
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

        private StudentViewModel selectedStudent;
        public StudentViewModel SelectedStudent
        {
            get { return this.selectedStudent; }
            set
            {
                this.selectedStudent = value;
                this.OnPropertyChanged("SelectedStudent");
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
