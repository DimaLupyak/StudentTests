using AdminWPFClient.ServiceReference;
using FirstFloor.ModernUI.Windows.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace AdminClient.ViewModels
{
    public class StudentsListViewModel : INotifyPropertyChanged
    {
        #region Private Variables
        private StudentTestServiceClient service = new StudentTestServiceClient();
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Commands
        public ICommand DeleteCommand { get; set; }
        #endregion

        #region Constructor
        public StudentsListViewModel()
        {
            Students = new ObservableCollection<StudentViewModel>(service.GetStudents());
            Groups = new ObservableCollection<GroupViewModel>(service.GetGroups());

            this.DeleteCommand = new SimpleCommand
            {
                ExecuteDelegate = x => DeleteStudent(x as StudentViewModel)
            };            
        }
        #endregion

        #region Properties
        private ObservableCollection<StudentViewModel> students;
        public ObservableCollection<StudentViewModel> Students
        {
            get { return this.students; }
            set
            {
                this.students = value;
                this.OnPropertyChanged("Students");
            }
        }

        private ObservableCollection<GroupViewModel> groups;
        public ObservableCollection<GroupViewModel> Groups
        {
            get { return this.groups; }
            set
            {
                this.groups = value;
                this.OnPropertyChanged("Groups");
            }
        }
        #endregion

        #region Public Methods
        public void RefreshGroups()
        {
            Students = new ObservableCollection<StudentViewModel>(service.GetStudents());
            Groups = new ObservableCollection<GroupViewModel>(service.GetGroups());
        }

        public void CreateStudent(StudentViewModel student)
        {
            service.CreateStudent(student);
        }

        public void UpdateStudent(StudentViewModel student)
        {
            service.UpdateStudent(student);
        }
        #endregion

        #region Private Methods
        private void DeleteStudent(StudentViewModel student)
        {
            if (student != null)
            {
                var result = ModernDialog.ShowMessage("Всі дані пов'язані з даним студентом будуть видалені. Продовжити видалення?", "Видалення студента", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    service.DeleteStudent(student);
                    Students.Remove(student);
                }
            }
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
