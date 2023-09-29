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
using DevComponents.DotNetBar.Controls;

namespace CRMFinalProject
{
    public partial class ActivityForm : Form
    {
        public ActivityForm()
        {
            InitializeComponent();
        }
        ActivityBLL bll = new ActivityBLL();
        UserBLL Ubll = new UserBLL();
        CustomerBLL Cbll = new CustomerBLL();
        ActivityCategoryBLL Acbll = new ActivityCategoryBLL();
        ReminderBLL Rbll= new ReminderBLL();    
        Customer c= new Customer();
        User u = new User();    
        ActivityCategory ac= new ActivityCategory();    
        void FillDataGrid()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.Read();
            dataGridViewX1.Columns["id"].Visible = false;
        }


        private void ActivityForm_Load(object sender, EventArgs e)
        {
            AutoCompleteStringCollection phone = new AutoCompleteStringCollection();
            foreach (var item in Cbll.ReadPhoneNumbers())
            {
                phone.Add(item);
            }
            textBoxX1.AutoCompleteCustomSource = phone; 
            AutoCompleteStringCollection names= new AutoCompleteStringCollection(); 
            foreach(var item in Ubll.ReadUsersName())
            {
                names.Add(item);    
            }
            textBoxX2.AutoCompleteCustomSource = names; 
            AutoCompleteStringCollection AcNames= new AutoCompleteStringCollection();   
            foreach(var item in Acbll.ReadName())
            {
                AcNames.Add(item);  
            }
            textBoxX3.AutoCompleteCustomSource = AcNames;
            FillDataGrid(); 
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            c = Cbll.Read(textBoxX1.Text);
            textBoxX1.Enabled = false;  

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            u = Ubll.Readu(textBoxX2.Text); 
            textBoxX2.Enabled = false;  

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ac = Acbll.Read(textBoxX3.Text);
            textBoxX3.Enabled = false;      
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Activity a = new Activity();
            a.Title = textBoxX4.Text;
            a.Info = richTextBox1.Text;
            a.RegDate = DateTime.Now;
            MessageBox.Show(bll.Create(a, u, c, ac));
            if (checkBox1.Checked)
            {
                Reminder r= new Reminder();
                r.Title = textBoxX4.Text;   
                r.ReminderInfo = richTextBox1.Text; 
                r.RegDate= DateTime.Now;
                r.RemindDate = dateTimeInput1.Value;
                MessageBox.Show(Rbll.Crate(r, u));
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

       
    }
}
