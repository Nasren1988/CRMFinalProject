using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BLL
{
    public class DashBordBLL
    {
        DashBordDAL dal = new DashBordDAL();
        public string UserRemindersCount(User u)
        {
            return dal.UserRemindersCount(u);       
        }
        public string CustomerCount()
        {
            return dal.CustomerCount(); 
        }
        public string SellsCount()
        {
            return dal.SellsCount();        
        }
        public List<Reminder> GetUserReminder(User u)
        {
            return dal.GetUserReminder(u);      
        }


    }
}
