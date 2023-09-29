using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Reminder
    {
      public Reminder()
        {
            DeleteStatus = true;
            IsDone=false;

        }
           
        public int id { get; set; }
        public string Title { get; set; }
        public string ReminderInfo { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime RemindDate { get; set; }
        public bool DeleteStatus { get; set; }
        public Nullable<bool> IsDone { get; set; }
        public User user { get; set; }
    }
}
