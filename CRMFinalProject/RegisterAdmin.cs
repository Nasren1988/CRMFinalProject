using BE;
using FoxLearn.License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;

namespace CRMFinalProject
{
    public partial class RegisterAdmin : UserControl
    {
        public RegisterAdmin()
        {
            InitializeComponent();
        }
        msgbox m = new msgbox();
        Timer t1 = new Timer();
        
        UserBLL Ubll = new UserBLL();   
        UserGroup Ug = new UserGroup();
        UserGroupBLL UGbll= new UserGroupBLL(); 
        void SwichPanels()
        {

            t1.Enabled = true;
            t1.Interval = 15;
            t1.Tick += Timer_Tick;
            t1.Start(); 
        }
       
       

        OpenFileDialog ofd = new OpenFileDialog();
        Image PIC;
        string SavePic(string UserName)
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + @"\UserPic";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string PicName = UserName + ".JPG";
            try
            {
                string PicPath = ofd.FileName;
                File.Copy(PicPath, path + PicName, true);
            }
            catch (Exception e)
            {
                MessageBox.Show("سیستم قادر به بارگزاری عکس نمی باشد" + e.Message);
            }
            return path + PicName;
        }
        int y = 227;
        UserAccessRole FillAccessRole(string Section, bool CanEnter, bool CanCreate, bool CanUpdate, bool CanDelete)
        {
            UserAccessRole role = new UserAccessRole();
            role.Section = Section;
            role.CanEnter = CanEnter;
            role.CanCreate = CanCreate;
            role.CanUpdate = CanUpdate;
            role.CanDelete = CanDelete;
            return role;

        }
        void CreateAdminGroup()
        {
            UserGroup Ug= new UserGroup();
            Ug.Title = "مدیریت";
            Ug.UserAccessRoles.Add(FillAccessRole("بخش مشتریان", true, true, true, true));
            Ug.UserAccessRoles.Add(FillAccessRole("بخش کالاها", true, true, true, true));
            Ug.UserAccessRoles.Add(FillAccessRole("بخش فاکتورها", true, true, true, true));
            Ug.UserAccessRoles.Add(FillAccessRole("بخش فعالیت ها", true, true, true, true));
            Ug.UserAccessRoles.Add(FillAccessRole("بخش یادآورها", true, true, true, true));
            Ug.UserAccessRoles.Add(FillAccessRole("بخش کاربران", true, true, true, true));
            Ug.UserAccessRoles.Add(FillAccessRole("پنل پیامکی", true, true, true, true));
            Ug.UserAccessRoles.Add(FillAccessRole("بخش گزارشات", true, true, true, true));
            Ug.UserAccessRoles.Add(FillAccessRole("بخش تنظیمات", true, true, true, true));
            UGbll.Create(Ug);
        }
      
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (panel4.Location.Y < 755)
            {
                y = y + 15;
                panel4.Location = new Point(24, y);
            }
            else
            {
                t1.Stop();
                panel2.Visible = true;


            }
        }

        private void RegisterAdmin_Load(object sender, EventArgs e)
        {
            textBoxX5.Text = ComputerInfo.GetComputerId();
        }

        private void symbolBox3_Click(object sender, EventArgs e)
        {
            KeyManager km = new KeyManager(textBoxX5.Text);
            string productKey = textBoxX6.Text;
            if (km.ValidKey(ref productKey))
            {
                KeyValuesClass kv = new KeyValuesClass();
                if (km.DisassembleKey(productKey, ref kv))
                {
                    LicenseInfo lic = new LicenseInfo();
                    lic.ProductKey = productKey;
                    lic.FullName = "Personal accounting";
                    if (kv.Type == LicenseType.TRIAL)
                    {
                        lic.Day = kv.Expiration.Day;
                        lic.Month = kv.Expiration.Month;
                        lic.Year = kv.Expiration.Year;
                    }

                    km.SaveSuretyFile(string.Format(@"{0}\Key.lic", Application.StartupPath), lic);
                    m.MyShowDialog("تبریک", "نرم افزار با موفقیت فعال شد", "", false, false);
                    SwichPanels();
                }
            }
            else
            {
                m.MyShowDialog("اخطار", "لایسنس وارد شده صحیح نمی باشد", "", false, true);
            }
        }

        private void symbolBox4_Click(object sender, EventArgs e)
        {
            User u = new User();
            CreateAdminGroup();
            u.UserName = textBoxX4.Text;
            u.Password = textBoxX1.Text;
            if (textBoxX2.Text == textBoxX1.Text)
            {
                u.Password = textBoxX2.Text;
            }
            else
            {
                m.MyShowDialog("اخطار", "پسورد و تکرار آن با هم مطابقت ندارند", "", false, false);
            }
            u.RegDate = DateTime.Now;
            u.Pic = SavePic(textBoxX4.Text);
             m.MyShowDialog("نتیجه ثبت نام", Ubll.Create(u, UGbll.ReadN("مدیریت")),"",false,false);
            this.Visible = false;
            ((LoginForm)Application.OpenForms["LoginForm"]).LoadLoginForm();    
        }

        private void symbolBox2_Click(object sender, EventArgs e)
        {
            ofd.Filter = "JPG(*.JPG)|*.JPG";
            ofd.Title = "تصویر کاربر را انتخاب کنید";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                PIC = Image.FromFile(ofd.FileName);

            }
        }

       
    }
 
}
