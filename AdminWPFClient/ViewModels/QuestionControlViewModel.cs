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

        public QuestionControlViewModel(QuestionViewModel question)
        {
            //this.DeleteCommand = new SimpleCommand
            //{
            //    ExecuteDelegate = x =>
            //    {
            //        if (x as TestViewModel != null)
            //        {
            //            var result = ModernDialog.ShowMessage("Всі дані пов'язані з даним тестом будуть видалені. Продовжити видалення?", "Видалення тесту", MessageBoxButton.YesNo);
            //            if (result == MessageBoxResult.Yes)
            //            {
            //                service.DeleteTest(x as TestViewModel);
            //                Tests.Remove(x as TestViewModel);
            //            }
            //        }
            //    },
            //    CanExecuteDelegate = x => true
            //};
            Question = question;
        }
        private QuestionViewModel question;
        public QuestionViewModel Question
        {
            get { return this.question; }
            set
            {
                this.question = value;
                Answers = new ObservableCollection<AnswerViewModel>(service.GetQuestionAnswers(value.Id));
                this.OnPropertyChanged("Question");
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

        public void CreateAnswer(AnswerViewModel test)
        {
            //service.CreateTest(test);
        }
        public void UpdateAnswer(AnswerViewModel test)
        {
            //service.UpdateTest(test);
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

