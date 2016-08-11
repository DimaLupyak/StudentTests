using AdminWPFClient.ServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminWPFClient.ViewModels
{
    public class AccessControlViewModel : INotifyPropertyChanged
    {
        private StudentTestServiceClient service = new StudentTestServiceClient();
        private int testId;

        public AccessControlViewModel(GroupViewModel group, int testId)
        {
            this.Group = group;
            this.testId = testId;
            this.IsAccess = service.IsAcceess(group.Id, testId);
        }


        private GroupViewModel group;
        public GroupViewModel Group
        {
            get { return this.group; }
            set
            {
                this.group = value;
                this.OnPropertyChanged("Group");
            }
        }

        private bool isAccess;
        public bool IsAccess
        {
            get { return this.isAccess; }
            set
            {
                this.isAccess = value;
                if (value)
                {
                    if (!service.IsAcceess(group.Id, testId))
                        service.CreateAccess(group.Id, testId);
                }
                else
                {
                    if (service.IsAcceess(group.Id, testId))
                        service.DeleteAccess(group.Id, testId);
                }
                this.OnPropertyChanged("IsAccess");
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
