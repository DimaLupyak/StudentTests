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

namespace AdminClient.ViewModels
{
    public class GroupListViewModel : INotifyPropertyChanged
    {        
        private StudentTestServiceClient service = new StudentTestServiceClient();

        public GroupListViewModel()
        {
            this.DeleteCommand = new SimpleCommand
            {
                ExecuteDelegate = x => 
                {
                    if (x as GroupViewModel != null)
                    {
                        var result = ModernDialog.ShowMessage("Всі дані пов'язані з даною групою будуть видалені. Продовжити видалення?", "Видалення групи", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            service.DeleteGroup(x as GroupViewModel);
                            Groups.Remove(x as GroupViewModel);
                        }
                    }
                },
                CanExecuteDelegate = x => true
            };
            Groups = new ObservableCollection<GroupViewModel>(service.GetGroups());
        }

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


        private void OnPropertyChanged(string propertyName)
        {
            if(this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void CreateGroup(GroupViewModel group)
        {
            service.CreateGroup(group);
        }
        public void UpdateGroup(GroupViewModel group)
        {
            service.UpdateGroup(group);
        }
        
        public ICommand DeleteCommand { get; set; }


    }

}
