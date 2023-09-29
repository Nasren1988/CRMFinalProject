using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using BE;
using BLL;
using DevComponents.AdvTree;

namespace CRMFinalProject
{
    public partial class UserForm : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
       (
           int nLeftRect,     // x-coordinate of upper-left corner
           int nTopRect,      // y-coordinate of upper-left corner
           int nRightRect,    // x-coordinate of lower-right corner
           int nBottomRect,   // y-coordinate of lower-right corner
           int nWidthEllipse, // width of ellipse
           int nHeightEllipse // height of ellipse
       );

        public UserForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));

        }
        UserBLL bll = new UserBLL();
        int id;
        UserGroupBLL UGbll = new UserGroupBLL();   
        UserGroup ug = new UserGroup();     
        void FillDataGrid()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.Read();
            //dataGridView1.Columns["آیدی"].Visible = false;
        }
        void ClearTxtBoxs()
        {
            textBoxX3.Text = "";
            textBoxX2.Text = "";
            textBoxX4.Text = "";

        }
        msgbox m = new msgbox();    
        UserAccessRole FillAccessRole(string Section,bool CanEnter,bool CanCreate,bool CanUpdate,bool CanDelete)
        {
            UserAccessRole uar = new UserAccessRole();
            uar.Section = Section;
            uar.CanEnter = CanEnter;
            uar.CanCreate = CanCreate;  
            uar.CanUpdate = CanUpdate;  
            uar.CanDelete = CanDelete;  
            return uar;
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


        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            ofd.Filter = "JPG(*.JPG)|*.JPG";
            ofd.Title = "تصویر کاربر را انتخاب کنید";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                PIC = Image.FromFile(ofd.FileName);
                pictureBox2.Image = PIC;
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }




        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            User u = new User();
            u.Name = textBoxX3.Text;
            u.UserName = textBoxX4.Text;
            if (textBoxX1.Text == textBoxX2.Text)
            {
                u.Password = textBoxX1.Text;
            }
            else
            {
                MessageBox.Show("کلمه عبور و تکرار آن با هم مطابقت ندارد");
            }
            if (label1.Text == "ثبت اطلاعات")
            {
                u.Pic = SavePic(textBoxX4.Text);
                u.RegDate = DateTime.Now;
                MessageBox.Show(bll.Create(u,ug));
            }
            else
            {
                u.Pic = SavePic(textBoxX4.Text);
                u.RegDate = DateTime.Now;
                MessageBox.Show(bll.Update(id, u));
            }

            FillDataGrid();
            ClearTxtBoxs();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["id"].Value);

        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            FillDataGrid();

        }

     

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                bll.Delete(id);
            }
            FillDataGrid();

        }

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User u = bll.Read(id);
            textBoxX3.Text = u.Name;
            textBoxX4.Text = u.UserName;
            pictureBox2.Image = Image.FromFile(u.Pic);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            label1.Text = "ویرایش اطاعات";


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            { 
                checkBox5.Checked = true;
                checkBox6.Checked = true;
                checkBox7.Checked = true;
                checkBox8.Checked = true;
                checkBox9.Checked = true;
                checkBox10.Checked = true;
                checkBox11.Checked = true;
                checkBox12.Checked = true;
                checkBox13.Checked = true;

            }
            else
            {
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
                checkBox9.Checked = false;
                checkBox10.Checked = false;
                checkBox11.Checked = false;
                checkBox12.Checked = false;
                checkBox13.Checked = false;

            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked) 
            { 
                
                checkBox22.Checked = true;
                checkBox14.Checked = true;
                checkBox15.Checked = true;
                checkBox16.Checked = true;
                checkBox17.Checked = true;
                checkBox18.Checked = true;
                checkBox19.Checked = true;
                checkBox20.Checked = true;
                checkBox21.Checked = true;
                
            }
            else
            {
                checkBox14.Checked = false;
                checkBox15.Checked = false;
                checkBox16.Checked = false;
                checkBox17.Checked = false;
                checkBox18.Checked = false;
                checkBox19.Checked = false;
                checkBox20.Checked = false;
                checkBox21.Checked = false;
                checkBox22.Checked = false; 

            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox23.Checked = true;
                checkBox24.Checked = true;
                checkBox25.Checked = true;
                checkBox26.Checked = true;
                checkBox27.Checked = true;
                checkBox28.Checked = true;
                checkBox29.Checked = true;
                checkBox30.Checked = true;
                checkBox31.Checked = true;
            }
            else
            {
                checkBox23.Checked = false;
                checkBox24.Checked = false;
                checkBox25.Checked = false;
                checkBox26.Checked = false;
                checkBox27.Checked = false;
                checkBox28.Checked = false;
                checkBox29.Checked = false;
                checkBox30.Checked= false;
                checkBox31.Checked= false;  
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox4.Checked)
            {
                checkBox32.Checked = true;
                checkBox33.Checked = true;
                checkBox34.Checked = true;
                checkBox35.Checked = true;
                checkBox36.Checked = true;
                checkBox37.Checked = true;
                checkBox38.Checked = true;
                checkBox39.Checked = true;
            }
            else
            {
                checkBox32.Checked = false;
                checkBox33.Checked = false;
                checkBox34.Checked = false;
                checkBox35.Checked = false;
                checkBox36.Checked = false;
                checkBox37.Checked = false;
                checkBox38.Checked = false;
                checkBox39.Checked = false;
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            UserGroup ug = new UserGroup();
            ug.Title = textBoxX8.Text;
            ug.UserAccessRoles.Add(FillAccessRole(label12.Text, checkBox5.Checked, checkBox14.Checked, checkBox23.Checked, checkBox32.Checked));
            ug.UserAccessRoles.Add(FillAccessRole(label13.Text, checkBox6.Checked, checkBox15.Checked, checkBox24.Checked, checkBox33.Checked));
            ug.UserAccessRoles.Add(FillAccessRole(label14.Text, checkBox7.Checked, checkBox16.Checked, checkBox25.Checked, checkBox72.Checked));
            ug.UserAccessRoles.Add(FillAccessRole(label15.Text, checkBox8.Checked, checkBox17.Checked, checkBox26.Checked, checkBox34.Checked));
            ug.UserAccessRoles.Add(FillAccessRole(label16.Text, checkBox9.Checked, checkBox18.Checked, checkBox27.Checked, checkBox35.Checked));
            ug.UserAccessRoles.Add(FillAccessRole(label17.Text, checkBox10.Checked, checkBox19.Checked, checkBox28.Checked, checkBox36.Checked));
            ug.UserAccessRoles.Add(FillAccessRole(label18.Text, checkBox11.Checked, checkBox20.Checked, checkBox29.Checked, checkBox37.Checked));
            ug.UserAccessRoles.Add(FillAccessRole(label19.Text, checkBox12.Checked, checkBox21.Checked, checkBox30.Checked, checkBox38.Checked));
            ug.UserAccessRoles.Add(FillAccessRole(label20.Text, checkBox13.Checked, checkBox22.Checked, checkBox31.Checked, checkBox39.Checked));

            m.MyShowDialog("نتیجه ثبت اطلاعات", UGbll.Create(ug), "", false, false);
        }   
    } 

    }
