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
    public class QuestionControlViewModel : INotifyPropertyChanged
    {
        private StudentTestServiceClient service = new StudentTestServiceClient();
        public delegate void EventHandler();
        public event EventHandler DeleteEvent;
        void OnDeleteEvent()
        {
            if (DeleteEvent != null) DeleteEvent();
        }
        public QuestionControlViewModel(QuestionViewModel question)
        {
            this.QuestionDeleteCommand = new SimpleCommand
            {
                ExecuteDelegate = x =>
                {
                    QuestionControlViewModel questionControl = x as QuestionControlViewModel;
                    if (questionControl != null)
                    {
                        var result = ModernDialog.ShowMessage("Всі дані пов'язані з даним питанням будуть видалені. Продовжити видалення?", "Видалення питання", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            service.DeleteQuestion(questionControl.Question);
                            OnDeleteEvent();
                            //Questions.Remove(questionControl);
                        }
                    }
                },
                CanExecuteDelegate = x => true
            };
            this.AnswerDeleteCommand = new SimpleCommand
            {
                ExecuteDelegate = x =>
                {
                    if (x as AnswerViewModel != null)
                    {
                        var result = ModernDialog.ShowMessage("Всі дані пов'язані з даною відповіддю будуть видалені. Продовжити видалення?", "Видалення відповіді", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            service.DeleteAnswer(x as AnswerViewModel);
                            Answers.Remove(x as AnswerViewModel);
                        }
                    }
                },
                CanExecuteDelegate = x => true
            };
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
                    Answers = new ObservableCollection<AnswerViewModel>(service.GetQuestionAnswers(value.Id));
                    Text = value.Text;
                }
                this.OnPropertyChanged("Question");
            }
        }
        private string text;
        public string Text
        {
            get { return this.text; }
            set
            {
                this.text = value;
                question.Text = value;
                if (question.Id != 0)
                    service.UpdateQuestion(question);
                this.OnPropertyChanged("Text");
            }
        }
        private ObservableCollection<AnswerViewModel> answers;
        public ObservableCollection<AnswerViewModel> Answers
        {
            get { return this.answers; }
            set
            {
                this.answers = value;
                this.OnPropertyChanged("Answers");
            }
        }

        public void CreateAnswer(AnswerViewModel answer)
        {
            service.CreateAnswer(answer);
            Answers = new ObservableCollection<AnswerViewModel>(service.GetQuestionAnswers(question.Id));
        }
        public void UpdateAnswer(AnswerViewModel answer)
        {
            service.UpdateAnswer(answer);
        }
        public ICommand QuestionDeleteCommand { get; set; }
        public ICommand AnswerDeleteCommand { get; set; }

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

