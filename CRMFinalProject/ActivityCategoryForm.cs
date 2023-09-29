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
    public partial class ActivityCategoryForm : Form
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

        public ActivityCategoryForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        ActivityCategoryBLL bll= new ActivityCategoryBLL();
        msgbox m = new msgbox();    
        int id;
        void FillDataGrid()
        {
           dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.Read();
            dataGridViewX1.Columns["آیدی"].Visible = false;
            
        }
        void ClearTxtBoxs()
        {
            textBoxX1.Text = "";
           
         }
          


        private void ActivityCategoryForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ActivityCategory ac = new ActivityCategory();
            ac.CategoryName = textBoxX1.Text;
            if (label1.Text == "ثبت نوع فعالیت جدید")
            {
               
                m.MyShowDialog("اطلاعیه",bll.Create(ac),"",true,false);
            }
            else
            {
               m.MyShowDialog("اطلاعیه", bll.Update(id, ac),"",true,false);
            }
            
            FillDataGrid(); 

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["آیدی"].Value);
        }

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActivityCategory ac = bll.Read(id);
            textBoxX1.Text = ac.CategoryName;
            label1.Text = "ویرایش نوع فعالیت جدید";
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("اخطار", "در صورت حذف مشتری تمام اطلاعات مربوط به آن هم حذف میشود");
            if (dr == DialogResult.Yes)
            {
                bll.Delete(id);
            }



            FillDataGrid();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
