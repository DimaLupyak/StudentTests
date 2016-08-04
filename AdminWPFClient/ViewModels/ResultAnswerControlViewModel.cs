using AdminWPFClient.ServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminWPFClient.ViewModels
{
    public class ResultAnswerControlViewModel : INotifyPropertyChanged
    {
        private StudentTestServiceClient service = new StudentTestServiceClient();

        public ResultAnswerControlViewModel(ResultAnswerViewModel result)
        {
            this.Question = service.GetQuestions().Where(x => x.Id == result.QuestionId).FirstOrDefault();
            this.Answer = service.GetQuestionAnswers(Question.Id).Where(x => x.Id == result.AnswerId).FirstOrDefault();
        }

        private QuestionViewModel question;
        public QuestionViewModel Question
        {
            get { return this.question; }
            set
            {
                this.question = value;
                this.OnPropertyChanged("Question");
            }
        }
        private AnswerViewModel answer;
        public AnswerViewModel Answer
        {
            get { return this.answer; }
            set
            {
                this.answer = value;
                this.OnPropertyChanged("Answer");
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
