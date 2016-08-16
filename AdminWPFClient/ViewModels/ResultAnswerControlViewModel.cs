using AdminWPFClient.ServiceReference;
using System.ComponentModel;
using System.Linq;

namespace AdminWPFClient.ViewModels
{
    public class ResultAnswerControlViewModel : INotifyPropertyChanged
    {
        #region Private Variables
        private StudentTestServiceClient service = new StudentTestServiceClient();
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructor
        public ResultAnswerControlViewModel(ResultAnswerViewModel result)
        {
            this.Question = service.GetQuestions()
                .Where(x => x.Id == result.QuestionId)
                .FirstOrDefault();
            this.Answer = service.GetQuestionAnswers(Question.Id)
                .Where(x => x.Id == result.AnswerId)
                .FirstOrDefault();
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
        #endregion

        #region Private Methods
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
