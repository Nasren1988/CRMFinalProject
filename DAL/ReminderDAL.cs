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
    public class ReminderDAL
    {
        DB db= new DB(); 
        
        public string Create(Reminder r,User u)
        {
            try
            {
                r.user= db.Users.Find(u.id);    
                db.Reminders.Add(r);
                db.SaveChanges();
                return "یادآور با موفقیت انجام شد";
            }
            catch (Exception e)
            {
                return "ثیت یادآور بت مشکل مواجه شده\n" + e.Message;
            }

        }

        public DataTable Read(string s)
        {
            SqlConnection con = new SqlConnection("Data Source=.; Initial Catalog=CRMFirst; Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.SearchReminder";
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

        public DataTable Read()
        {
            SqlConnection con = new SqlConnection("Data Source=.; Initial Catalog=CRMFirst; Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.ReadReminders";
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            var sqladapter = new SqlDataAdapter();
            sqladapter.SelectCommand = cmd;
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];

        }
        public string Update(int id, Reminder r)
        {
            var q = db.Reminders.Where(i => i.id == id).SingleOrDefault();
            try
            {
                if (q != null)
                {
                    q.Title = r.Title;
                    q.ReminderInfo = r.ReminderInfo;
                    q.RemindDate = r.RemindDate;
                    db.SaveChanges();
                    return "یادآور با موفقیت تغییر کرد";
                }
                else
                {
                    return "یادآور موردنظر پیدا نشد";
                }
            }
            catch(Exception e)
            {
                return "در آبدیت یادآور مشکلی پیش آمده است\n"+e.Message;    
            }
        }

        public string Delete(int id)
        {
            var q = db.Reminders.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.DeleteStatus = true;
                    db.SaveChanges();
                    return "حذف یادآور با موفقیت انجام شد";
                }
                else
                {
                    return "یادآور مورد نظر پیدا نشد";
                }
            }
            catch (Exception e)
            {
                return "در حذف یادآور مشکلی پیش آمده است  :\n" + e.Message;
            }
        }
        public string Done(int id)
        {
            var q = db.Reminders.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.IsDone = true;
                    db.SaveChanges();
                    return " یادآوری  برای یادآورانجام شد";
                }
                else
                {
                    return "یادآوری برای یادآوری نیست";
                }
            }
            catch (Exception e)
            {
                return " مشکلی در یادآوری پیش آمده :\n" + e.Message;
            }
        }
    
    }
}
