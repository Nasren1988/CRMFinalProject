using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DashBordDAL
    {
        DB db= new DB();        
        public string UserRemindersCount(User u)
        {
            var q= db.Users.Include("Reminders").Where(i=> i.id==u.id).FirstOrDefault();    
            return q.Reminders.Count().ToString();      
        }
        public string CustomerCount()
        {
            return db.Customers.Count().ToString();     
        }
        public string SellsCount()
        {
            int sum = 0;
            foreach(var item in db.Invoices)
            {
                if (item.RegDate.Date == DateTime.Today)
                    sum++;
            }
            return sum.ToString();  
        }
        public List<Reminder> GetUserReminder(User u)
        {
            return db.Reminders.Include("User").Where(i=> i.id==u.id).ToList(); 
        }   
    }
}
