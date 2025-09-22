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
        public DateTime LastClickTime { get; set; } = DateTime.MinValue;
        public bool IsDelayOver()=>DateTime.Now - LastClickTime >= new TimeSpan(0, 0, 10);
    }
}
