using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;


namespace DAL
{
    public class ActivityDAL
    {
        DB db = new DB();
        public string Create(Activity a,User u,Customer c ,ActivityCategory ac)
        {
            try
            {
                
                a.user = db.Users.Find(u.id);
                a.Customer=db.Customers.Find(c.id);
                a.Category= db.ActivityCategories.Find(ac.id);
                db.Activites.Add(a);
                db.SaveChanges();
                return "فعالیت ثبت شد";
            }
            catch(Exception e)
            {
                return"در ثبت فعالیت مشکلی پیش آمده است/n" +e.Message;
            }
        }
        public DataTable Read()
        {
            SqlConnection con = new SqlConnection("Data Source=.; Initial Catalog=CRMFirst; Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.ReadActivity ";
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            var sqladapter = new SqlDataAdapter();
            sqladapter.SelectCommand = cmd;
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];

        }
        public Activity Read(int id)
        {
            return db.Activites.Where(i => i.id == id).FirstOrDefault();
        }
        public string Delete(int id)
        {
            var q = db.Activites.Where(i => i.id == id).FirstOrDefault();
            if (q != null)
            {
                try
                {
                    q.DeleteStatus = true;
                    db.SaveChanges();
                    return "فعالیت با موفقیت حذف شد";
                }
                catch (Exception e)
                {
                    return "در حذف فعالیت مشکلی پیش آمده \n" + e.Message;
                }

            }
            else
            {
                return "فعالیتی با این مشخصات پیدا نشد";
            }
        }

    }
}
