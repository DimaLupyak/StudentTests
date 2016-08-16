using StudentWpfClient.ServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace StudentWpfClient.ViewModels
{
    public class QuestionControlViewModel : INotifyPropertyChanged
    {
        #region Private Variables
        private StudentTestServiceClient service = new StudentTestServiceClient();
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action AnswerSelected;
        #endregion

        #region Constructor
        public QuestionControlViewModel(QuestionViewModel question)
        {    
            Question = question;
        }
        #endregion

        #region Properties
        private QuestionViewModel question;
        public QuestionViewModel Question
        {
            get { return this.question; }
            set
            {
                this.question = value;
                if (value.Id != 0)
                {
                    Random random = new Random();
                    Answers = service.GetQuestionAnswers(value.Id).OrderBy(x => random.Next());
                }
                this.OnPropertyChanged("Question");
            }
        }

        private AnswerViewModel selectedAnswer;
        public AnswerViewModel SelectedAnswer
        {
            get { return this.selectedAnswer; }
            set
            {
                this.selectedAnswer = value;
                OnAnswerSelected();
                this.OnPropertyChanged("SelectedAnswer");
            }
        }

        private IEnumerable<AnswerViewModel> answers;
        public IEnumerable<AnswerViewModel> Answers
        {
            get { return this.answers; }
            set
            {
                this.answers = value;
                this.OnPropertyChanged("Answers");
            }
        }
        #endregion

        #region Private Methods
        private void OnAnswerSelected()
        {
            if (AnswerSelected != null)
            {
                AnswerSelected();
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

