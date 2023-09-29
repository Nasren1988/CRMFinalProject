using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
   public class ActivityCategoryBLL
    {
        ActivityCategoryDAL dal = new ActivityCategoryDAL();    

        public string Create(ActivityCategory ac)
        {
            return dal.Create(ac);  
        }
        public ActivityCategory Read(int id)
        {
            return dal.Read(id);
        }
        public DataTable Read()
        {
            return dal.Read();  
        }
        public string Update(int id,ActivityCategory ac)
        {
            return dal.Update(id,ac);    
        }
        public string Delete(int id) 
        {
            return dal.Delete(id);      
        }
        public List<string> ReadName()
        {
            return dal.ReadNames(); 
            
        }
        public ActivityCategory Read(string p)
        {
            return dal.Read(p); 
        }
    }
}
