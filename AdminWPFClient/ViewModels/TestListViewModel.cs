using AdminClient.ViewModels;
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

namespace AdminWPFClient.ViewModels
{
    public class TestListViewModel : INotifyPropertyChanged
    {
        private StudentTestServiceClient service = new StudentTestServiceClient();

        public TestListViewModel()
        {
            this.DeleteCommand = new SimpleCommand
            {
                ExecuteDelegate = x =>
                {
                    if (x as TestViewModel != null)
                    {
                        var result = ModernDialog.ShowMessage("Всі дані пов'язані з даним тестом будуть видалені. Продовжити видалення?", "Видалення тесту", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            service.DeleteTest(x as TestViewModel);
                            Tests.Remove(x as TestViewModel);
                        }
                    }
                },
                CanExecuteDelegate = x => true
            };
            Tests = new ObservableCollection<TestViewModel>(service.GetTests());
        }
        private TestViewModel selectedTest;
        public TestViewModel SelectedTest
        {
            get { return this.selectedTest; }
            set
            {
                this.selectedTest = value;
                Questions = new ObservableCollection<QuestionControlViewModel>(service.GetTestQuestions(value.Id)
                    .Select(x=>new QuestionControlViewModel(x)));
                this.OnPropertyChanged("SelectedTest");
            }
        }

        private ObservableCollection<TestViewModel> tests;
        public ObservableCollection<TestViewModel> Tests
        {
            get { return this.tests; }
            set
            {
                this.tests = value;
                this.OnPropertyChanged("Tests");
            }
        }

        private ObservableCollection<QuestionControlViewModel> questions;
        public ObservableCollection<QuestionControlViewModel> Questions
        {
            get { return this.questions; }
            set
            {
                this.questions = value;
                this.OnPropertyChanged("Questions");
            }
        }

        public void CreateTest(TestViewModel test)
        {
            service.CreateTest(test);
        }
        public void UpdateTest(TestViewModel test)
        {
            service.UpdateTest(test);
        }

        public ICommand DeleteCommand { get; set; }

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
