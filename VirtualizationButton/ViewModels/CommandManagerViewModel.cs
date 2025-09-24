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

        public string VirtualizationStatus => _isToggleActive ? "Virtualization on" : "Virtualization off";

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
                OnPropertyChanged(nameof(VirtualizationStatus));
            }
        }
        private static void AnalysisVirtualization(Dictionary<string, bool?> values, CommandManagerViewModel obj)
        {
            if (values.ContainsKey("HyperVisorPresent") && values["HyperVisorPresent"] == true)
            {
                obj._isToggleActive = true;
                return;
            }
            obj._isToggleEnable = false;
            if(values.ContainsKey("HyperVRequirementVirtualizationFirmwareEnabled") && values["HyperVRequirementVirtualizationFirmwareEnabled"] == false)
            {
                MessageBox.Show("Virtualization is disabled in the bios, the program will be closed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
            if (values.ContainsKey("HyperVRequirementDataExecutionPreventionAvailable") && values["HyperVRequirementDataExecutionPreventionAvailable"] == false)
            {
                MessageBox.Show("Your PC does not support DEP/NX Bit.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (values.ContainsKey("HyperVRequirementSecondLevelAddressTranslation") && values["HyperVRequirementSecondLevelAddressTranslation"] == false)
            {
                MessageBox.Show("Your PC does not support SLAT.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (values.ContainsKey("HyperVRequirementVMMonitorModeExtensions") && values["HyperVRequirementVMMonitorModeExtensions"] == false)
            {
                MessageBox.Show("The virtualization extension is not supported on your computer.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public CommandManagerViewModel()
        {
            CommandManagerViewModel.AnalysisVirtualization(CommandManager.GetVirtualizationStatus(), this);
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
                          TimeSpan timeLeft = TimeSpan.FromSeconds(10) - (DateTime.Now - _timer.LastClickTime);
                          MessageBox.Show($"Wait {timeLeft.Seconds} second(s)", "Too fast!", MessageBoxButton.OK, MessageBoxImage.Error);
                          return;
                      }
                      _timer.LastClickTime = DateTime.Now;
                      if (IsToggleActive)
                      {
                          CommandManager.EnableVirtualization();
                      }
                      else
                      {
                          CommandManager.DisableVirtualization();
                      }
                      var answer = MessageBox.Show("Do you want to reboot now?", "Reboot", MessageBoxButton.YesNo, MessageBoxImage.Question);
                      if (answer == MessageBoxResult.Yes)
                      {
                          CommandManager.Reboot();
                          Application.Current.Shutdown();
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
