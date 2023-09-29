using BE;
using BLL;
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

namespace CRMFinalProject
{
    public partial class ReminderForm : Form
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

        public ReminderForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        ReminderBLL rbll= new ReminderBLL();    
        UserBLL ubll= new UserBLL();   
        User u = new User();    
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ReminderForm_Load(object sender, EventArgs e)
        {
            
            AutoCompleteStringCollection names = new AutoCompleteStringCollection();
            foreach(var item in ubll.ReadUsersName())
            {
                names.Add(item);
                textBoxX1.AutoCompleteCustomSource = names;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBoxX1.Enabled = false;
            u = ubll.Readu(textBoxX1.Text);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Reminder r = new Reminder();
            r.Title = textBoxX2.Text;
            r.ReminderInfo = richTextBox1.Text;
            r.RegDate = DateTime.Now;
            r.RemindDate = dateTimeInput1.Value;
            MessageBox.Show(rbll.Crate(r, u));
           
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
