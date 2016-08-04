using AdminClient.ViewModels;
using AdminWPFClient.Helpers;
using AdminWPFClient.ServiceReference;
using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;
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
            this.AnswerImageDeleteCommand = new SimpleCommand
            {
                ExecuteDelegate = x =>
                {
                    AnswerViewModel answer = x as AnswerViewModel;
                    if (answer != null)
                    {
                        var result = ModernDialog.ShowMessage("Видалити зображення?", "Видалення зображення", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            answer.Image = null;
                            service.UpdateAnswer(answer);
                        }
                    }
                },
                CanExecuteDelegate = x => true
            };
            this.ImageDeleteCommand = new SimpleCommand
            {
                ExecuteDelegate = x =>
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

                },
                CanExecuteDelegate = x => true
            };

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
            this.UploadImageCommand = new SimpleCommand
            {
                ExecuteDelegate = x =>
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
                },
                CanExecuteDelegate = x => true
            };
            this.UploadAnswerImageCommand = new SimpleCommand
            {

                ExecuteDelegate = x =>
                {
                    AnswerViewModel answer = x as AnswerViewModel;
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

        public void CreateAnswer(AnswerViewModel answer)
        {
            service.CreateAnswer(answer);
            Answers = new ObservableCollection<AnswerViewModel>(service.GetQuestionAnswers(question.Id));
        }
        public void UpdateAnswer(AnswerViewModel answer)
        {
            service.UpdateAnswer(answer);
        }
        public ICommand UploadImageCommand { get; set; }
        public ICommand UploadAnswerImageCommand { get; set; }
        public ICommand QuestionDeleteCommand { get; set; }
        public ICommand AnswerDeleteCommand { get; set; }
        public ICommand AnswerImageDeleteCommand { get; set; }
        public ICommand ImageDeleteCommand { get; set; }

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

