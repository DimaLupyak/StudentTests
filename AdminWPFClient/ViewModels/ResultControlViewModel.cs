using AdminWPFClient.ServiceReference;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AdminWPFClient.ViewModels
{
    
    public class ResultControlViewModel : INotifyPropertyChanged
    {
        #region Private Variables
        private StudentTestServiceClient service = new StudentTestServiceClient();
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructor
        public ResultControlViewModel(ResultViewModel result)
        {
            this.Result = result;
            this.Test = service.GetTests()
                .Where(x => x.Id == result.TestId)
                .FirstOrDefault();
            this.Student = service.GetStudents()
                .Where(x => x.Id == result.StudentId)
                .FirstOrDefault();
            this.Answers = service.GetResultAnswers(result.Id)
                .Select(x=>new ResultAnswerControlViewModel(x));
        }
        #endregion

        #region Properties
        private ResultViewModel result;
        public ResultViewModel Result
        {
            get { return this.result; }
            set
            {
                this.result = value;
                this.OnPropertyChanged("Result");
            }
        }

        private StudentViewModel student;
        public StudentViewModel Student
        {
            get { return this.student; }
            set
            {
                this.student = value;
                this.OnPropertyChanged("Student");
            }
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

        private IEnumerable<ResultAnswerControlViewModel> answers;
        public IEnumerable<ResultAnswerControlViewModel> Answers
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
