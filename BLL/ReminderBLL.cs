using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class ReminderBLL
    {
        ReminderDAL dal = new ReminderDAL();    

        public string Crate(Reminder r,User u)
        {
            return dal.Create(r,u);
        }
        public DataTable Read(string s)
        {
            return dal.Read(s); 
        }
        public DataTable Read()
        {
            return dal.Read();  
        }
        public string Update(int id,Reminder r)
        {
            return dal.Update(id,r);    
        }
        public string Delete(int id) 
        {
            return dal.Delete(id);  
        }
        public string Done(int id) 
        {
           return dal.Done(id); 
        }
    }
}
