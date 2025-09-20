using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VirtualizationButton.Models
{
    public class Timer
    {
        private DateTime _lastClickTime = DateTime.MinValue;

        public DateTime GetLastClickTime() { return _lastClickTime; }   
        public void SetLastClickTime()
        {
            _lastClickTime = DateTime.Now;
        }

        public bool IsDelayOver()
        {
            if (DateTime.Now - _lastClickTime >= new TimeSpan(0, 0, 10))
            {
                return true;
            }
            
            return false;
        }

    }
}
