using FirstFloor.ModernUI.Windows.Controls;
using StudentWpfClient.ServiceReference;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace StudentWpfClient.ViewModels
{
    class TestingViewModel : INotifyPropertyChanged
    {
        #region Private Variables
        private StudentTestServiceClient service = new StudentTestServiceClient();
        private int studentId;
        private DispatcherTimer timer;
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action TestingFinish;
        #endregion

        #region Commands
        public ICommand NextQuestionCommand { get; set; }
        public ICommand PreviousQuestionCommand { get; set; }
        public ICommand SaveResult { get; set; }
        #endregion

        #region Constructor
        public TestingViewModel(int studentId, TestViewModel test)
        {
            Random random = new Random();
            this.studentId = studentId;
            this.Test = test;
            this.CurrentQuestionsNumber = 0;
            this.Questions = service.GetTestQuestions(test.Id)
                        .Select(x => new QuestionControlViewModel(x))
                        .OrderBy(x => random.Next())
                        .ToArray();
            foreach (var question in Questions)
            {
                question.AnswerSelected += UpdateLeftQuestionsCount;
            }
            this.LeftTime = test.Time;
            this.UpdateLeftQuestionsCount();

            this.timer = new DispatcherTimer();
            this.timer.Tick += new EventHandler(TimerTick);
            this.timer.Interval = new TimeSpan(0, 0, 1);
            this.timer.Start();

            this.NextQuestionCommand = new SimpleCommand
            {
                ExecuteDelegate = x => CurrentQuestionsNumber++,
                CanExecuteDelegate = x => CurrentQuestionsNumber < QuestionsCount
            };

            this.PreviousQuestionCommand = new SimpleCommand
            {
                ExecuteDelegate = x => CurrentQuestionsNumber--,
                CanExecuteDelegate = x => CurrentQuestionsNumber > 1
            };

            this.SaveResult = new SimpleCommand
            {
                ExecuteDelegate = x => SaveResults()
            };
        }
        #endregion

        #region Properties
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

        private ResultViewModel testResult;
        public ResultViewModel TestResult
        {
            get { return this.testResult; }
            set
            {
                this.testResult = value;
                this.OnPropertyChanged("TestResult");
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
                Random random = new Random();
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
        #endregion

        #region Private Methods
        private void TimerTick(object sender, EventArgs e)
        {
            LeftTime -= new TimeSpan(0, 0, 1);
            if (LeftTime.TotalSeconds == 0) SaveResults();
        }

        private void SaveResults()
        {
            MessageBoxResult result = MessageBoxResult.Yes;
            if (LeftTime.TotalSeconds != 0)
            {
                result = ModernDialog.ShowMessage(LeftQuestionsCount > 0 ?
                            "Ви не дали відповідь на всі питання. Завершити тест?" :
                            "Завершити тест?",
                            "Збереження результату",
                            MessageBoxButton.YesNo);
            }
            else
            {
                result = ModernDialog.ShowMessage("Час вичерпано",
                               "Збереження результату",
                               MessageBoxButton.OK);
            }
            if (result == MessageBoxResult.Yes || result == MessageBoxResult.OK)
            {
                TestResult = new ResultViewModel
                {
                    CorrectCount = Questions
                    .Where(x => x.SelectedAnswer != null && x.SelectedAnswer.IsCorrect == true)
                    .Count(),
                    StudentId = studentId,
                    TestId = Test.Id,
                    SpentTime = Test.Time - LeftTime,
                    ResultDate = DateTime.Now
                };
                TestResult.Id = service.CreateResult(TestResult);
                foreach (var question in Questions.Where(x => x.SelectedAnswer != null))
                {
                    ResultAnswerViewModel resultAnswer = new ResultAnswerViewModel
                    {
                        AnswerId = question.SelectedAnswer.Id,
                        QuestionId = question.Question.Id,
                        ResultId = TestResult.Id
                    };
                    service.CreateResultAnswer(resultAnswer);
                }                
                if (TestingFinish != null) TestingFinish();
            }
        }

        private void UpdateLeftQuestionsCount()
        {
            LeftQuestionsCount = Questions
                .Where(x => x.SelectedAnswer == null)
                .Count();
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