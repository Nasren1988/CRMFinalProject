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
    public class UserDAL
    {
        DB db = new DB();   

        public string Create(User u,UserGroup ug)
        {
            try
            {
              if(!Exsit(u))
                {
                    u.userGroup= db.userGroups.Find(ug.id);
                    db.Users.Add(u);
                    db.SaveChanges();
                    return "ثبت اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "نام کاربری قبلا ثبت شده است";
                }
            }
            catch(Exception e) 
            {
                return "در ثبت مشکلی رخ دادخ است\n" + e.Message; 
            }
            
        }

        public bool Exsit(User u)
        {
            return db.Users.Any(i => i.UserName == u.UserName);
        }
        public bool IseRegisterd()
        {
            return db.Users.Count() > 0;    
        }
        public bool Read(User u)
        {
            var q= db.Users.Where(i =>i.id == u.id);
            if(q.Count() ==0) 
            {
                return true;
            }
            else
            {
                return false;   
            }
        }

        public User Read(int id)
        {
            return db.Users.Where(i=> i.id == id).FirstOrDefault(); 
        }
        public DataTable Read()
        {
            SqlConnection con = new SqlConnection("Data Source=.; Initial Catalog=CRMFirst; Integrated Security=True");
            string cmd = "SELECT id, Name AS [نام و نام خانوادگی], UserName AS [نام کاربری],  RegDate AS [تاریخ ثبت]\r\nFROM  dbo.Users\r\nWHERE  (DeleteStatus = 0)";
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public User ReadU(string s)
        {
            return db.Users.Where(i=> i.UserName== s).FirstOrDefault();
        }
        public List<string> ReadUsersName()
        {
           return db.Users.Select(i => i.UserName).ToList();  
        }
        public string Update(User u,int id)
        {
            try
            {
                var q = db.Users.Where(i => i.id == id).FirstOrDefault();
                if (q != null)
                {
                    q.Name = u.Name;
                    q.UserName = u.UserName;    
                    q.Password = u.Password;    
                    q.Pic = u.Pic;  
                    db.SaveChanges();
                    return "ویرایش اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "کاربری با این مشخصات یافت نشد";
                }
            }
            catch(Exception e) 
            {
                return" در ویرایش مشکلی پیش آمده\n"+e.Message;
            }
        }
        public string Delete(int id)
        {
            var q = db.Users.Where(i=>i.id == id).FirstOrDefault(); 
            if(q != null)
            {
                try
                {
                    q.DeleteStatus = true;
                    db.SaveChanges();
                    return "کاربر با موفقیت حذف شد";
                }
                catch (Exception e) 
                {
                    return "در حذف کاربر مشکلی پیش آمده \n" + e.Message;
                }

            }
            else
            {
                return "کاربری با این مشخصات پیدا نشد";
            }
        }
        public User Login(string u,string p)
        {
            return db.Users.Include("UserGroup").Where(i=> i.UserName==u && i.Password==p).FirstOrDefault();
        }
        public bool Access(User u , string s,int a)
        {
            UserGroup ug = db.userGroups.Include("UserAccessRoles").Where(i=>i.id==u.userGroup.id).FirstOrDefault();
            UserAccessRole uar = ug.UserAccessRoles.Where(z=> z.Section==s).FirstOrDefault();
            if (a == 1)
            {
                return uar.CanEnter;
            }
            else if(a== 2)
            {
                return uar.CanCreate;
            }
            else if(a== 3)
            {
                return uar.CanUpdate;
            }
            else
            {
                return uar.CanDelete;
            }
        }
    }
}
