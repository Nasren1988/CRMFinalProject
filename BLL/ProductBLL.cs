using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using System.Data;

namespace BLL
{
    public  class ProductBLL
    {
        ProductDAL dal = new ProductDAL();  
        public string Create(Product p)

        {
         return dal.Create(p);
        }
        public bool Read(Product p) 
        {
          return  dal.Read(p);    
        }
        public DataTable Read()
        {
            return dal.Read();  
        }
        public DataTable Read(string s)
        {
            return dal.Read(s); 
        }
        public Product Read(int id)
        {
            return dal.Read(id);   
            
        }
        public List<string> ReadNames()
        {
            return dal.ReadNames(); 
        }
        public Product ReadN(string p)
        {
            return dal.ReadN(p);
        }
        public string Update(int id, Product p)
        {
            return dal.Update(id, p);   
        }
        public string Delete(int id)
        {
            return dal.Delete(id);  
        }
    }
}
