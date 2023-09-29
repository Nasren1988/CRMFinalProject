using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class InvoiceBLL
    {
      InvoiceDAL dal = new InvoiceDAL();        

        public string Create(Invoice i, Customer c, List<Product> p)
        {
            return dal.Create(i, c, p);     
        }
        public string Read()
        {
            return dal.Read();      
        }
    }
}
