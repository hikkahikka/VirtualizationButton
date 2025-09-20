using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VirtualizationButton.Models;

namespace VirtualizationButton.ViewModels
{
    public class CommandManagerViewModel : INotifyPropertyChanged
    {
        private bool _isToggleActive ;
        public bool IsToggleActive
        {
            get { return _isToggleActive; }
            set
            {
                _isToggleActive = value;
                OnPropertyChanged(nameof(IsToggleActive));
                
            }
        }
        public CommandManagerViewModel()
        {
            _isToggleActive = CommandManager.GetVirtualizationStatus();
            OnPropertyChanged(nameof(IsToggleActive));
        }
        private RelayCommand virtualizatuonCommand;
        public RelayCommand VirtualizationCommand
        {
            get
            {
                return virtualizatuonCommand ??
                  (virtualizatuonCommand = new RelayCommand(obj =>
                  {   
                      
                      if(IsToggleActive){
                          CommandManager.EnableVirtualization();
                      }
                      else
                      {
                          CommandManager.DisableVirtualization();
                      }
                  }));
            }
        }
        // (virtualizatuonCommand = new RelayCommand(obj => IsToggleActive? CommandManager.EnableVirtualizatuon() : CommandManager.DisableVirtualizatuon())); 

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
