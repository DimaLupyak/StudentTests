using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;
using StudentWpfClient.ServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace StudentWpfClient.ViewModels
{
    public class QuestionControlViewModel : INotifyPropertyChanged
    {
        private StudentTestServiceClient service = new StudentTestServiceClient();
        
        public QuestionControlViewModel(QuestionViewModel question)
        {    
            Question = question;
        }
        private QuestionViewModel question;
        public QuestionViewModel Question
        {
            get { return this.question; }
            set
            {
                this.question = value;
                if (value.Id != 0)
                {
                    Answers = service.GetQuestionAnswers(value.Id);
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

