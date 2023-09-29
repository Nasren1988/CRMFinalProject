using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using System.Data;

namespace BLL
{
    public class UserBLL
    {
        private string EnCode(string pass)
        {
            byte[] encData_byte = new byte[pass.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(pass);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;

        }
        private string Decode(string Encodedpass)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(Encodedpass);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        UserDAL dal= new UserDAL(); 
        public string Create(User u,UserGroup ug)
        {
            u.Password = EnCode(u.Password);    
            return dal.Create(u,ug);   

        }
        public bool IseRegisterd()
        {
            return dal.IseRegisterd();
        }
        public DataTable Read() 

        {
            return dal.Read(); 
        }

        public User Read(int id)
        {
            return dal.Read(id);    
        }
        public User Readu(string s)
        {
            return dal.ReadU(s);    
        }
        public List<string> ReadUsersName()
        {
            return dal.ReadUsersName(); 
        }
        public string Update(int id , User u)
        {
            u.Password= EnCode(u.Password);
            return dal.Update(u,id);    
        }
        public string Delete(int id)
        { 
            return dal.Delete(id);  
        }
        public User Login(string u, string p)
        {
            return dal.Login(u,EnCode(p));      
        }
        public bool Access(User u, string s, int a)
        {
            return dal.Access(u,s,a);   
        }
    }
}
