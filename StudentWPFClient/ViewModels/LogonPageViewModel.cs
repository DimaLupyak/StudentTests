using StudentWpfClient.ServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace StudentWpfClient.ViewModels
{
    class LogonPageViewModel : INotifyPropertyChanged
    {
        #region Private Variables
        private StudentTestServiceClient service = new StudentTestServiceClient();
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action ResetSelectedStudent;
        #endregion

        #region Constructor
        public LogonPageViewModel()
        {
            Groups = service.GetGroups();           
        }
        #endregion

        #region Properties
        private GroupViewModel selectedGroup;
        public GroupViewModel SelectedGroup
        {
            get { return this.selectedGroup; }
            set
            {
                this.selectedGroup = value;
                Students = service.GetStudents().Where(x => x.GroupID == value.Id);
                if (ResetSelectedStudent != null) ResetSelectedStudent();
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
        #endregion

        #region Private Methods
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
