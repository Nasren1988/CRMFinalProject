using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class ActivityCategoryDAL
    {
        DB db = new DB();
        public string Create(ActivityCategory ac)
        {
            try
            {
                db.ActivityCategories.Add(ac);
                db.SaveChanges();
                return "دسته بندی با موفقیت ایجاد شد";
            }
            catch (Exception e)
            {
                return "در ایجاد دسته بندی مشکلی پیش آمده است\n" + e.Message;
            }
        }
        public ActivityCategory Read(int id)
        {
            return db.ActivityCategories.Where(i => i.id == id).FirstOrDefault();
        }
        public DataTable Read()
        {
            SqlConnection con = new SqlConnection("Data Source=.; Initial Catalog=CRMFirst; Integrated Security=True");
            string cmd = "SELECT  id AS آیدی, CategoryName AS [نام دسته بندی]\r\nFROM  dbo.ActivityCategories\r\nWHERE  (DeleteStatus = 0)";
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];

        }
        public ActivityCategory Read(string p)
        {
            return db.ActivityCategories.Where(i=>i.CategoryName == p).FirstOrDefault();    
        }
        public string Update(int id,ActivityCategory ac)
        {
            var q = db.ActivityCategories.Where(i=> i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.CategoryName = ac.CategoryName;
                    db.SaveChanges();
                    return "دسته بندی با موفقیت بروزرسانی شد";
                }
                else
                {
                    return "دسته بندی مورد نظر پیدا نشد";
                }

            }
            catch(Exception e)
            {
                return "در بروزرسانی دسته بندی مشکلی پیش آمده\n"+e.Message;
            }
           
        }
        
        public string Delete(int id)
        {
            var q = db.ActivityCategories.Where(i => i.id == id).FirstOrDefault();
            if (q != null)
            {
                try
                {
                    q.DeleteStatus = true;
                    db.SaveChanges();
                    return "دسته بندی با موفقیت حذف شد";
                }
                catch (Exception e)
                {
                    return "در حذف دسته بندی مشکلی پیش آمده \n" + e.Message;
                }

            }
            else
            {
                return "دسته بندی با این مشخصات پیدا نشد";
            }
        }
        public List<string> ReadNames()
        {
           return  db.ActivityCategories.Where(i=> i.DeleteStatus==false).Select(i=>i.CategoryName).ToList();   
        }
    }
}