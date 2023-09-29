using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using System.Dynamic;

namespace BLL
{
    public class UserGroupBLL
    {
        UserGroupDAL dal= new UserGroupDAL();   
        public string Create(UserGroup ug)
        {
            return dal.create(ug);  
        }
        public bool Read(string Name)
        {
            return dal.Read(Name);  
        }
        public List<string> ReadUGNames()
        {
            return dal.ReadUGNames();   
        }
        public UserGroup ReadN(string n)
        {
            return dal.ReadN(n);    
        }

    }
}
