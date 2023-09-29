using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Invoice
    {
        public Invoice()
        {
            DeleteStatus = false;
        }


        public int id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime RegDate { get; set; }
        public Nullable <DateTime> CheckOutDate { get; set; }   
        public bool IsCheckout { get; set; }
        public bool DeleteStatus { get; set; }  
        public Customer Customer { get; set; }  
        public User User { get; set; }
        public List<Product> products { get; set; }= new List<Product>();   
    }
}
