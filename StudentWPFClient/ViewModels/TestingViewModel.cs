using FirstFloor.ModernUI.Windows.Controls;
using StudentWpfClient.ServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StudentWpfClient.ViewModels
{
    class TestingViewModel : INotifyPropertyChanged
    {
        private StudentTestServiceClient service = new StudentTestServiceClient();
        private int studentId;
        public TestingViewModel(int studentId, TestViewModel test)
        {
            this.studentId = studentId;
            this.Test = test;
            this.CurrentQuestionsNumber = 0;
            this.Questions = service.GetTestQuestions(test.Id)
                        .Select(x => new QuestionControlViewModel(x))
                        .ToArray();
            foreach (var item in Questions)
            {
                item.AnswerSelected += UpdateLeftQuestionsCount;
            }
            this.LeftTime = test.Time;
            UpdateLeftQuestionsCount();

            this.NextQuestionCommand = new SimpleCommand
            {
                ExecuteDelegate = x =>
                {
                    CurrentQuestionsNumber++;
                },
                CanExecuteDelegate = x => CurrentQuestionsNumber < QuestionsCount
            };

            this.PreviousQuestionCommand = new SimpleCommand
            {
                ExecuteDelegate = x =>
                {
                    CurrentQuestionsNumber--;
                },
                CanExecuteDelegate = x => CurrentQuestionsNumber > 1
            };

            this.SaveResult = new SimpleCommand
            {
                ExecuteDelegate = x => SaveResults()
            };
        }

        private void SaveResults()
        {
            var result = ModernDialog.ShowMessage(LeftQuestionsCount > 0 ?
                        "Ви не дали відповідь на всі питання. Завершити тест?" :
                        "Завершити тест?",
                        "Збереження результату",
                        MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                ResultViewModel testResult = new ResultViewModel
                {
                    CorrectCount = Questions.Where(x => x.SelectedAnswer != null && x.SelectedAnswer.IsCorrect == true).Count(),
                    StudentId = studentId,
                    TestId = Test.Id,
                    SpentTime = Test.Time - LeftTime,
                    ResultDate = DateTime.Now
                };
                testResult.Id = service.CreateResult(testResult);
                foreach (var item in Questions.Where(x => x.SelectedAnswer != null ))
                {
                    
                }
            }
        }

        protected void UpdateLeftQuestionsCount()
        {
            LeftQuestionsCount = Questions.Where(x => x.SelectedAnswer == null).Count();
        }

        public ICommand NextQuestionCommand { get; set; }
        public ICommand PreviousQuestionCommand { get; set; }
        public ICommand SaveResult { get; set; }

        private TestViewModel test;
        public TestViewModel Test
        {
            get { return this.test; }
            set
            {
                this.test = value;
                this.OnPropertyChanged("Test");
            }
        }

        private TimeSpan leftTime;
        public TimeSpan LeftTime
        {
            get { return this.leftTime; }
            set
            {
                this.leftTime = value;
                this.OnPropertyChanged("LeftTime");
            }
        }

        private QuestionControlViewModel[] questions;

        public QuestionControlViewModel[] Questions
        {
            get { return this.questions; }
            set
            {
                this.questions = value;
                QuestionsCount = value.Count();
                if (value.Count() > 0)
                {
                    CurrentQuestionsNumber = 1;
                }
                this.OnPropertyChanged("Questions");
            }
        }



        private QuestionControlViewModel currentQuestion;

        public QuestionControlViewModel CurrentQuestion
        {
            get { return this.currentQuestion; }
            set
            {
                this.currentQuestion = value;
                this.OnPropertyChanged("CurrentQuestion");
            }
        }

        private int currentQuestionsNumber;

        public int CurrentQuestionsNumber
        {
            get { return this.currentQuestionsNumber; }
            set
            {
                if (Questions != null)
                {
                    this.currentQuestionsNumber = value;
                    CurrentQuestion = Questions[value - 1];
                    this.OnPropertyChanged("CurrentQuestionsNumber");
                }
            }
        }

        private int questionsCount;

        public int QuestionsCount
        {
            get { return this.questionsCount; }
            set
            {
                this.questionsCount = value;
                this.OnPropertyChanged("QuestionsCount");
            }
        }

        private int leftQuestionsCount;

        public int LeftQuestionsCount
        {
            get { return this.leftQuestionsCount; }
            set
            {
                this.leftQuestionsCount = value;
                this.OnPropertyChanged("LeftQuestionsCount");
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