using AdminClient.ViewModels;
using AdminWPFClient.Helpers;
using AdminWPFClient.ServiceReference;
using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace AdminWPFClient.ViewModels
{
    public class QuestionControlViewModel : INotifyPropertyChanged
    {
        #region Private Variables
        private StudentTestServiceClient service = new StudentTestServiceClient();
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action DeleteEvent;
        #endregion

        #region Commands
        public ICommand UploadImageCommand { get; set; }
        public ICommand UploadAnswerImageCommand { get; set; }
        public ICommand QuestionDeleteCommand { get; set; }
        public ICommand AnswerDeleteCommand { get; set; }
        public ICommand AnswerImageDeleteCommand { get; set; }
        public ICommand ImageDeleteCommand { get; set; }
        #endregion

        #region Constructor
        public QuestionControlViewModel(QuestionViewModel question)
        {
            Question = question;

            this.AnswerImageDeleteCommand = new SimpleCommand
            {
                ExecuteDelegate = x => AnswerImageDelete(x as AnswerViewModel)
            };

            this.ImageDeleteCommand = new SimpleCommand
            {
                ExecuteDelegate = x => ImageDelete()
            };

            this.QuestionDeleteCommand = new SimpleCommand
            {
                ExecuteDelegate = x => QuestionDelete(x as QuestionControlViewModel)
            };

            this.AnswerDeleteCommand = new SimpleCommand
            {
                ExecuteDelegate = x => AnswerDelete(x as AnswerViewModel)
            };

            this.UploadImageCommand = new SimpleCommand
            {
                ExecuteDelegate = x => UploadImage()
            };

            this.UploadAnswerImageCommand = new SimpleCommand
            {
                ExecuteDelegate = x => UploadAnswerImage(x as AnswerViewModel)
            };
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
                    Answers = new ObservableCollection<AnswerViewModel>(service.GetQuestionAnswers(value.Id));
                    Text = value.Text;
                    Image = value.Image;
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

        private byte[] image;
        public byte[] Image
        {
            get { return this.image; }
            set
            {
                this.image = value;
                this.question.Image = value;
                if (question.Id != 0)
                    service.UpdateQuestion(this.question);
                this.OnPropertyChanged("Image");
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
        #endregion

        #region Public Methods
        public void CreateAnswer(AnswerViewModel answer)
        {
            service.CreateAnswer(answer);
            Answers = new ObservableCollection<AnswerViewModel>(service.GetQuestionAnswers(question.Id));
        }

        public void UpdateAnswer(AnswerViewModel answer)
        {
            service.UpdateAnswer(answer);
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

        private void AnswerImageDelete(AnswerViewModel answer)
        {
            if (answer != null)
            {
                var result = ModernDialog.ShowMessage("Видалити зображення?", "Видалення зображення", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    answer.Image = null;
                    service.UpdateAnswer(answer);
                }
            }
        }

        private void ImageDelete()
        {
            if (image != null)
            {
                var result = ModernDialog.ShowMessage("Видалити зображення?", "Видалення зображення", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    question.Image = null;
                    Image = null;
                    service.UpdateQuestion(question);
                }
            }
        }

        private void QuestionDelete(QuestionControlViewModel questionControl)
        {
            if (questionControl != null)
            {
                var result = ModernDialog.ShowMessage("Всі дані пов'язані з даним питанням будуть видалені. Продовжити видалення?", "Видалення питання", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    service.DeleteQuestion(questionControl.Question);
                    if (DeleteEvent != null) DeleteEvent();
                }
            }
        }

        private void AnswerDelete(AnswerViewModel answer)
        {
            var result = ModernDialog.ShowMessage("Всі дані пов'язані з даною відповіддю будуть видалені. Продовжити видалення?", "Видалення відповіді", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                service.DeleteAnswer(answer as AnswerViewModel);
                Answers.Remove(answer as AnswerViewModel);
            }
        }

        private void UploadImage()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                Image = ImageHelper.ToByteArray(new BitmapImage(new Uri(op.FileName)));
            }
        }

        private void UploadAnswerImage(AnswerViewModel answer)
        {
            if (answer != null)
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                if (op.ShowDialog() == true)
                {
                    answer.Image = ImageHelper.ToByteArray(new BitmapImage(new Uri(op.FileName)));
                    if (answer.Id != 0)
                        service.UpdateAnswer(answer);
                }
            }
        }
        #endregion
    }
}

