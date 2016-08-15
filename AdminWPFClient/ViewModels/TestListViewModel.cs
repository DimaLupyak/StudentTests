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
                            SelectedTest = null;
                        }
                    }
                },
                CanExecuteDelegate = x => true
            };
            this.AddQuestionCommand = new SimpleCommand()
            {
                ExecuteDelegate = x => AddQuestion(),
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
                if (value != null && value.Id != 0)
                {
                    UpdateQuestions();
                    UpdateGroups();
                    SelectedTestTime = selectedTest.Time;
                    this.OnPropertyChanged("SelectedTest");
                }
                else Questions = null;
                NewQuestion = null;


            }
        }

        private TimeSpan selectedTestTime;
        public TimeSpan SelectedTestTime
        {
            get { return this.selectedTestTime; }
            set
            {
                this.selectedTestTime = value;
                selectedTest.Time = value;
                service.UpdateTest(selectedTest);
                this.OnPropertyChanged("SelectedTestTime");
            }
        }

        private void UpdateQuestions()
        {
            Questions = new ObservableCollection<QuestionControlViewModel>(service.GetTestQuestions(SelectedTest.Id)
                        .Select(x => new QuestionControlViewModel(x)));
            foreach (var item in questions)
            {
                item.DeleteEvent += UpdateQuestions;
            }
        }
        private void UpdateGroups()
        {
            Groups = new ObservableCollection<AccessControlViewModel>(service.GetGroups()
                .Select(x=>new AccessControlViewModel(x, SelectedTest.Id)));
        }

        private ObservableCollection<TestViewModel> tests;
        public ObservableCollection<TestViewModel> Tests
        {
            get { return this.tests; }
            set
            {
                this.tests = value;
                if (selectedTest == null) SelectedTest = this.tests.First();
                this.OnPropertyChanged("Tests");
            }
        }

        private ObservableCollection<AccessControlViewModel> groups;
        public ObservableCollection<AccessControlViewModel> Groups
        {
            get { return this.groups; }
            set
            {
                this.groups = value;
                this.OnPropertyChanged("Groups");
            }
        }

        private QuestionControlViewModel newQuestion;
        public QuestionControlViewModel NewQuestion
        {
            get { return this.newQuestion; }
            set
            {
                this.newQuestion = value;
                if (value != null)
                    this.newQuestion.Question.TestId = selectedTest.Id;
                this.OnPropertyChanged("NewQuestion");
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
            Tests = new ObservableCollection<TestViewModel>(service.GetTests());
        }
        public void UpdateTest(TestViewModel test)
        {
            service.UpdateTest(test);
        }
        
        
        public ICommand DeleteCommand { get; set; }
        public ICommand AddQuestionCommand { get; set; }

        private void AddQuestion()
        {
            if (newQuestion == null)
            {
                NewQuestion = new QuestionControlViewModel(new QuestionViewModel());
            }
            else
            {
                if (NewQuestion.Question.Image != null && NewQuestion.Question.Text == null)
                    NewQuestion.Question.Text = "";
                if (NewQuestion.Question.Text != null)
                {
                    service.CreateQuestion(NewQuestion.Question);
                    UpdateQuestions();
                    NewQuestion = null;
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
        public event PropertyChangedEventHandler PropertyChanged;
    }

}
