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
    public class CustomerBLL
    {
        CustomerDAL dal = new CustomerDAL();    
        public string Create(Customer c)
        {
            if (dal.Read(c))
            {
                return dal.Create(c);   
            }
            else
            {
                return "کاربری با همین مشخصات وجود دارد";
            }
        }
        public DataTable Read()
        {
            return dal.Read();  
        }
        public DataTable Read(string s , int index)
        {
           return  dal.Read(s,index);
        }
        public Customer Read(int id)
        {
            return (Customer)dal.Read(id);  
        }
        public string Update(int id , Customer c)
        {
            return dal.Update(id, c);
        }

        public string Delete(int id) 
        {
            return dal.Delete(id);  
        }
        public List<string> ReadPhoneNumbers()
        {
            return dal.ReadPhoneNumbers();
        }
        public List<string> ReadNames() 
        {
            return dal.ReadNames(); 
        }
        public Customer Read(string p)
        {
            return dal.Read(p); 
        }
        
    }
}
