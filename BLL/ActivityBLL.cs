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
    public class ActivityBLL
    {
        ActivityDAL dal= new ActivityDAL();
        public string Create(Activity a, User u,Customer c,ActivityCategory ac)
        {
            return dal.Create(a, u,c,ac);
        }

        public DataTable Read()
        {
            return dal.Read();  
        }

        public Activity Read(int id)
        {

            return dal.Read(id);    
        }

        public string Delete(int id)
        {
            return dal.Delete(id);
        }
       
    }
}
