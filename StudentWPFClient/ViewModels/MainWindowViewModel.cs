using FirstFloor.ModernUI.Windows.Navigation;
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
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            this.OpenTestCommand = new SimpleCommand
            {
                ExecuteDelegate = x =>
                {
                    TestViewModel test = x as TestViewModel;
                    if (test != null)
                    {
                        TestingViewModel = new TestingViewModel(SelectedStudent.Id, test);
                        IInputElement target = NavigationHelper.FindFrame("_top", Application.Current.MainWindow); 
                        NavigationCommands.GoToPage.Execute("/Pages/TestingPage.xaml", target);
                    }
                },
                CanExecuteDelegate = x => true
            };
        }

        public ICommand OpenTestCommand { get; set; }

        private StudentViewModel selectedStudent;
        public StudentViewModel SelectedStudent
        {
            get { return this.selectedStudent; }
            set
            {
                if (value != null)
                {
                this.selectedStudent = value;                
                    TestsList = new TestsListViewModel(value.Id);
                    ResultsList = new ResultsListViewModel(value.Id);
                }
                this.OnPropertyChanged("SelectedStudent");
            }
        }

        private TestViewModel selectedTest;
        public TestViewModel SelectedTest
        {
            get { return this.selectedTest; }
            set
            {
                this.selectedTest = value;
                TestingViewModel = new TestingViewModel(selectedStudent.Id, value);
                this.OnPropertyChanged("SelectedTest");
            }
        }

        private TestsListViewModel testsList;
        public TestsListViewModel TestsList
        {
            get { return this.testsList; }
            set
            {
                this.testsList = value;
                this.OnPropertyChanged("TestsList");
            }
        }

        private ResultsListViewModel resultsList;
        public ResultsListViewModel ResultsList
        {
            get { return this.resultsList; }
            set
            {
                this.resultsList = value;
                this.OnPropertyChanged("ResultsList");
            }
        }
        
        private TestingViewModel testingViewModel;
        public TestingViewModel TestingViewModel
        {
            get { return this.testingViewModel; }
            set
            {
                this.testingViewModel = value;
                this.OnPropertyChanged("TestingViewModel");
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
