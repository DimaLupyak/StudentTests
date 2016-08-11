using StudentWpfClient.ServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentWpfClient.ViewModels
{
    class TestingViewModel : INotifyPropertyChanged
    {
        private StudentTestServiceClient service = new StudentTestServiceClient();

        public TestingViewModel(int studentId, TestViewModel test)
        {
            Test = test;
            CurrentQuestionsNumber = 0;
            Questions = service.GetTestQuestions(test.Id)
                        .Select(x => new QuestionControlViewModel(x))
                        .ToArray();
            LeftTime = test.Time;
        }


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
                this.OnPropertyChanged("Questions");
            }
        }

        private QuestionControlViewModel currentQuestions;

        public QuestionControlViewModel CurrentQuestions
        {
            get { return this.currentQuestions; }
            set
            {
                this.currentQuestions = value;
                this.OnPropertyChanged("CurrentQuestions");
            }
        }

        private int currentQuestionsNumber;

        public int CurrentQuestionsNumber
        {
            get { return this.currentQuestionsNumber; }
            set
            {
                if(value< Questions.Count())
                {
                    this.currentQuestionsNumber = value;
                    CurrentQuestions = Questions[value];
                    this.OnPropertyChanged("CurrentQuestionsNumber");
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