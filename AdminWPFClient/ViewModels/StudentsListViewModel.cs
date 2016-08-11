using AdminWPFClient.ServiceReference;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AdminClient.ViewModels
{
    public class StudentsListViewModel : INotifyPropertyChanged
    {        
        private StudentTestServiceClient service = new StudentTestServiceClient();

        public StudentsListViewModel()
        {
            this.DeleteCommand = new SimpleCommand
            {
                ExecuteDelegate = x => 
                {
                    if (x as StudentViewModel != null)
                    {
                        var result = ModernDialog.ShowMessage("Всі дані пов'язані з даним студентом будуть видалені. Продовжити видалення?", "Видалення студента", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            service.DeleteStudent(x as StudentViewModel);
                            Students.Remove(x as StudentViewModel);
                        }
                    }
                },
                CanExecuteDelegate = x => true
            };
            Students = new ObservableCollection<StudentViewModel>(service.GetStudents());
            Groups = new ObservableCollection<GroupViewModel>(service.GetGroups());
        }

        public void RefreshGroups()
        {
            Students = new ObservableCollection<StudentViewModel>(service.GetStudents());
            Groups = new ObservableCollection<GroupViewModel>(service.GetGroups());
        }

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


        private void OnPropertyChanged(string propertyName)
        {
            if(this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void CreateStudent(StudentViewModel student)
        {
            service.CreateStudent(student);
        }
        public void UpdateStudent(StudentViewModel student)
        {
            service.UpdateStudent(student);
        }
        
        public ICommand DeleteCommand { get; set; }


    }

}
