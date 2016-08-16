using AdminWPFClient.ServiceReference;
using FirstFloor.ModernUI.Windows.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace AdminClient.ViewModels
{
    public class GroupListViewModel : INotifyPropertyChanged
    {
        #region Private Variables
        private StudentTestServiceClient service = new StudentTestServiceClient();
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructor
        public GroupListViewModel()
        {
            Groups = new ObservableCollection<GroupViewModel>(service.GetGroups());

            this.DeleteCommand = new SimpleCommand
            {
                ExecuteDelegate = x => DeleteGroup(x as GroupViewModel)
            };            
        }
        #endregion

        #region Commands
        public ICommand DeleteCommand { get; set; }
        #endregion

        #region Properties
        private ObservableCollection<GroupViewModel> groups;
        public ObservableCollection<GroupViewModel> Groups
        {
            get { return this.groups; }
            set
            {
                this.groups = value;
                this.OnPropertyChanged("Groups");
            }
        }
        #endregion

        #region Public Methods
        public void CreateGroup(GroupViewModel group)
        {
            service.CreateGroup(group);
        }   

        public void UpdateGroup(GroupViewModel group)
        {
            service.UpdateGroup(group);
        }
        #endregion

        #region Private Methods
        private void DeleteGroup(GroupViewModel group)
        {
            if (group != null)
            {
                var result = ModernDialog.ShowMessage("Всі дані пов'язані з даною групою будуть видалені. Продовжити видалення?", "Видалення групи", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    service.DeleteGroup(group);
                    Groups.Remove(group);
                }
            }
        }

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
