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
using BE;
using BLL;


namespace CRMFinalProject
{
    public partial class CustomerForm : Form
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

        public CustomerForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }

        msgbox m= new msgbox();
       
        CustomerBLL bll = new CustomerBLL();
        UserBLL Ubll = new UserBLL();   
        int id;
        void FillDataGrid()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.Read();
            dataGridViewX1.Columns["آیدی"].Visible = false;
        }
        void ClearTxtBoxs()
        {
            textBoxX3.Text = "";
            textBoxX2.Text = "";
            textBoxX4.Text = "";

        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

       

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
            Customer c = new Customer();
            c.Name = textBoxX3.Text;
            c.Phone = textBoxX4.Text;
            c.RegDate = DateTime.Now;
            if (label1.Text=="ثبت اطلاعات")
            {
                m.MyShowDialog("اطلاعیه", bll.Create(c), "", false, false);
            }
            else
            {
                MessageBox.Show(bll.Update(id, c));
            }
        }
        int index;
        private void textBoxX2_TextChanged(object sender, EventArgs e)
        {
            dataGridViewX1.DataSource = null;
            if (checkBox1.Checked && checkBox2.Checked ||  !checkBox1.Checked && !checkBox2.Checked)
            {
                index = 0;
            }else if(checkBox1.Checked && !checkBox2.Checked)
            {
                index = 1;  
            }else if (checkBox2.Checked && !checkBox1.Checked)
            {
                index = 2;  
            }
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.Read(textBoxX2.Text,index);
            //dataGridViewX1.Columns["id"].Visible = false;

        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
           FillDataGrid();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);   
             id= Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["آیدی"].Value);
        }

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Customer c = bll.Read(id);
            textBoxX3.Text = c.Name;
            textBoxX4.Text = c.Phone;
            label1.Text = "ویرایش اطلاعات";
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
           DialogResult dr = m.MyShowDialog("اخطار", "در صورت حذف مشتری تمام اطلاعات مربوط به آن هم حذف میشود", "", true, true);
            if (dr ==DialogResult.Yes)
            {
                bll.Delete(id);
            }
                      
             
           
            FillDataGrid(); 
        }
    }
}
