using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VirtualizationButton.Models;

namespace VirtualizationButton.ViewModels
{
    public class CommandManagerViewModel : INotifyPropertyChanged
    {
        private Models.Timer _timer = new Models.Timer();


        private bool _isToggleEnable;
        public bool IsToggleEnabled => _timer.IsDelayOver();


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
                      if (!_timer.IsDelayOver())
                      {
                          IsToggleActive = !IsToggleActive;
                          TimeSpan timeLeft = TimeSpan.FromSeconds(10) - (DateTime.Now - _timer.GetLastClickTime());
                          MessageBox.Show($"Wait {timeLeft.Seconds} second(s)", "Too fast!", MessageBoxButton.OK, MessageBoxImage.Error);
                          return;
                      }
                      _timer.SetLastClickTime();
                      if (IsToggleActive)
                      {
                          CommandManager.EnableVirtualization();
                      }
                      else
                      {
                          CommandManager.DisableVirtualization();
                      }

                  }));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
