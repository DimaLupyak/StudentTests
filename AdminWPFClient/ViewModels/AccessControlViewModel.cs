using AdminWPFClient.ServiceReference;
using System.ComponentModel;

namespace AdminWPFClient.ViewModels
{
    public class AccessControlViewModel : INotifyPropertyChanged
    {
        #region Private Variables
        private StudentTestServiceClient service = new StudentTestServiceClient();
        private int testId;
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructor
        public AccessControlViewModel(GroupViewModel group, int testId)
        {
            this.Group = group;
            this.testId = testId;
            this.IsAccess = service.IsAcceess(group.Id, testId);
        }
        #endregion

        #region Properties
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
