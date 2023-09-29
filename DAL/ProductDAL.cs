using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public  class ProductDAL
    {
        DB db = new DB();   

        public string Create(Product p)
        {
            try
            {
                db.Products.Add(p);
                db.SaveChanges();
                return "محصول با موفقیت ثبت شد";
            }
            catch(Exception e) 
            {
                return"در ثبت محصول خطایی رخ داده است" +e.Message;
            }
        }
        public bool Read(Product p) 
        {
            var q = db.Products.Where(i => i.Name == p.Name);
            if(q.Count() > 0 )
            {
                return true;
            }
            else
            {
                return false;   
            }
        }
        public DataTable Read()
        {
            SqlConnection con = new SqlConnection("Data Source=.; Initial Catalog=CRMFirst; Integrated Security=True");
            string cmd = "SELECT id AS[آیدی] , name AS [نام محصول], Price AS [قیمت محصول], Stock AS [موجودی]\r\nFROM dbo.Products\r\nWHERE (DeleteStatus = 0)";
            var sqladapter = new SqlDataAdapter(cmd ,con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet(); 
            sqladapter.Fill(ds);    
            return ds.Tables[0];    

        }
        public DataTable Read(string s)
        {
            SqlConnection con = new SqlConnection("Data Source=.; Initial Catalog=CRMFirst; Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.SearchProduct";
            cmd.Parameters.AddWithValue("@Search", s);
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            var sqladapter = new SqlDataAdapter();
            sqladapter.SelectCommand = cmd;
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];

        }
        public Product Read(int id)
        {
           return  db.Products.Where(i => i.id == id).FirstOrDefault();    
        }
        public List<string> ReadNames()
        {
            return db.Products.Where(i=> i.DeleteStatus ==false).Select(i => i.Name).ToList();  
        }
        public Product ReadN(string p)
        {
            return db.Products.Where(i=>i.Name==p).SingleOrDefault();  
        }
        public string Update(int id, Product p)
        {
            var q = db.Products.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if(q != null)
                {
                    q.Name = p.Name;
                    q.Price = p.Price;
                    q.Stock = p.Stock;
                    db.SaveChanges();
                    return "محصول با موفقیت بروزرسانی شد ";
                }
                else
                {
                    return "محصول مورد نظر پیدا نشد";
                }
            }
            catch (Exception e)
            {
                return "در بروز رسانی محصول خطایی رخ دادخ است" + e.Message;
            }
        }
        public string Delete(int id)
        {
            var q = db.Products.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if(q != null)
                {
                    q.DeleteStatus = true;
                    return "محصول مورد نظر با موفقیت حذف شد ";
                }
                else
                {
                    return "محصولی با این مشخصات موجود نیست !";
                }
            }
            catch(Exception e)
            {
                return "در حذف مشکلی به وجود آمد" + e.Message;
            }
        }
    }
}
