using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataViewModels
{
    public class QuestionViewModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public Nullable<int> TestId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private string text;
        public string Text
        {
            get { return this.text; }
            set
            {
                this.text = value;
                this.OnPropertyChanged("Text");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }        
    }
}
