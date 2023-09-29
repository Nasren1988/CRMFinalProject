using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
   public class InvoiceDAL
    {
        DB db = new DB();   
        public string Create(Invoice i,Customer c,List<Product> p)
        {
            try
            {
                i.Customer = db.Customers.Find(c.id);
                foreach(var item in p)
                {
                    i.products.Add(db.Products.Find(item.id));
                }
                Random rnd = new Random();  
                string s=rnd.Next(1000000).ToString();
                var q = db.Invoices.Where(z => z.InvoiceNumber == s);
                while(q.Count() > 0)
                {
                    s=rnd.Next(10000000).ToString() ;   
                }
                i.InvoiceNumber = s;    
                db.Invoices.Add(i); 
                db.SaveChanges();
                return "فاکتور با موفقیت اضافه شد";
            }
            catch(Exception e)
            {
                return" دراضافه کردن فاکتور مشکلی پیش آمده\n"+e.Message;
            }

        }
        public string Read()
        {
            var q = db.Invoices.OrderByDescending(i=>i.id).FirstOrDefault();
            return q.InvoiceNumber;
        }
    }
}
