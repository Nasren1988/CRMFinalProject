using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace DAL
{
    public class CustomerDAL
    {
        DB db = new DB();   

        public string Create(Customer c)
        {
            try
            {
                db.Customers.Add(c);    
                db.SaveChanges();
                return "ثبت با موفقیت انجام شد ";
            }
            catch (Exception e) 
            {
                return "در ثبت مشکلی ایجاد شد !!" + e.Message;
            }
             
        }

        public bool Read(Customer c)
        {
            var q= db.Customers.Where(i => c.Phone ==  i.Phone);    
            if(q.Count()== 0)
            {
                return true;

            }
            else
            {
                return false;   
            }
        }

        public  DataTable Read()
        {
            SqlConnection con = new SqlConnection("Data Source=.; Initial Catalog=CRMFirst; Integrated Security=True");
            string cmd = "SELECT id AS [آیدی], Name AS [نام مشتری], Phone AS [شماره تماس], RegDate AS [تاریخ ثبت]\r\nFROM dbo.Customers\r\nWHERE (DeleteStatus = 0)";
            var sqladapter = new SqlDataAdapter(cmd , con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);   
            var ds = new DataSet(); 
            sqladapter.Fill(ds);    
            return ds.Tables[0];    
        
        }
        public DataTable Read(string s ,int index)
        {
            SqlCommand cmd = new SqlCommand();
            if(index == 0)
            {
                cmd.CommandText = "dbo.SearchCustomer";
            }
            else if(index == 1)
            {
                cmd.CommandText = "dbo.SearchCustomerName";
            }
            else if(index == 2) 
            {
                cmd.CommandText = "dbo.SearchCustomerPhone";
            }
            
            SqlConnection con = new SqlConnection("Data Source=.; Initial Catalog=CRMFirst; Integrated Security=True");
            
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
        public Customer Read(int id)
        {
            return db.Customers.Where(i => i.id == id).FirstOrDefault();    
        }
        public string Update(int id , Customer c) 
        {
            var q = db.Customers.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if(q != null)
                {
                    q.Name = c.Name;
                    q.Phone = c.Phone;
                    db.SaveChanges();
                    return "ویرایش با موفقیت انجام شد ";
                }
                else
                {
                    return "کاربر مورد نظر یافت نشد";
                }
            }
            catch(Exception e) 
            {
                return" در آبدیت مشکلی پیش آمده است" + e.Message;   
            }
        }
        public string Delete(int id) 
        { 
           var q = db.Customers.Where(i =>i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.DeleteStatus = true;
                    db.SaveChanges();
                    return "حذف با موفقیت انجام شد";
                }
                else
                {
                    return "کاربر مورد نظر یافت نشد";
                }
            }
            catch(Exception e) 
            { 
                return"در حذف مشکلی پیش آمده است  :\n"+ e.Message;  
            }
        }
        public List<string> ReadPhoneNumbers()
        {
            return db.Customers.Where(i=> i.DeleteStatus==false).Select(i=> i.Phone).ToList();    
        }
        public List<string> ReadNames()
        {
            return db.Customers.Where(i=> i.DeleteStatus==false).Select(i=>i.Name).ToList(); 
        }
        public Customer Read(string p)
        {
          return   db.Customers.Where(i=>i.Phone == p).FirstOrDefault();   
        }

    }
}
