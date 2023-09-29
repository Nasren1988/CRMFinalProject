using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;
using HandyControl.Controls;

namespace CRMFinalProject
{
    public partial class LoginUser : UserControl
    {
        public LoginUser()
        {
            InitializeComponent();
        }
        User u = new User();  
        DashBordBLL Dbll = new DashBordBLL();   
        UserBLL Ubll = new UserBLL(); 
        msgbox m = new msgbox();


        private void symbolBox4_Click(object sender, EventArgs e)
        {
            u = Ubll.Login(textBoxX4.Text, textBoxX1.Text);
            if(u != null)
            {
                m.MyShowDialog("خوش آمدید", "برای ورود به نرم افزار روی تایید کلیک کنید ", "", true, false);
               MainWindow w = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault(); 
                w.LoggedInUser = u;
                //w.UserNameTxt.Text = u.UserName;
                //w.PersonNameTxt.Text = u.Name;
                //w.RemindersCountTxt.Text=Dbll.UserRemindersCount(u);    
                ((LoginForm)System.Windows.Forms.Application.OpenForms["loginForm"]).Close();   
                
            }
            else
            {
                m.MyShowDialog("اخطار", "در ورود به نرم افزار مشکلی پیش آمده است ", "", false, true);
            }
        }
    }
}
