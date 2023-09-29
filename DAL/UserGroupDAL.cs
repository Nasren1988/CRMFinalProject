using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;


namespace DAL
{
    public class UserGroupDAL
    {
        DB db = new DB();   
        public string create( UserGroup ug)
        {
            try
            {
                db.userGroups.Add(ug);  
                db.SaveChanges();
                return "گروه کاربری با موفقیت ایجاد شد";
            }
            catch(Exception e) 
            {
                return "در ایجاد گروه کاربری مشکلی پیش آمده\n" + e.Message;
            }
        }
        public bool Read(string Name)
        {
            return !db.userGroups.Any(i => i.Title == Name);
        }
        public List<string> ReadUGNames() 
        {
            return db.userGroups.Select(i =>i.Title).ToList();      
        }
        public UserGroup ReadN(string n) 
        {
            return db.userGroups.Where(i => i.Title == n).FirstOrDefault(); 
        }
    }
}
