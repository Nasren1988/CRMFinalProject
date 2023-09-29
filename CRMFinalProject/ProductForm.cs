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
    public partial class ProductForm : Form
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

        public ProductForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        ProductBLL bll = new ProductBLL();
        msgbox m = new msgbox();   
        void FillDataGrid()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.Read();
            dataGridViewX1.Columns["آیدی"].Visible = false;
        }
        int id;
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Product p = new Product();
            p.Name = textBoxX3.Text;
            p.Price = Convert.ToDouble(textBoxX4.Text);
            p.Stock = Convert.ToInt32(textBoxX1.Text);
            if (label1.Text == "ثبت اطلاعات")
            {
                m.MyShowDialog("اطلاعیه", bll.Create(p), "", false, false);
            }
            else
            {
                MessageBox.Show(bll.Update(id, p));
            }
       
          FillDataGrid(); 
        }

        private void textBoxX2_TextChanged(object sender, EventArgs e)
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.Read(textBoxX2.Text);  
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["آیدی"].Value);
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = m.MyShowDialog("اخطار", "در صورت حذف کالا تمام اطلاعات مربوط به آن هم حذف میشود", "", true, true);
            if (dr == DialogResult.Yes)
            {
                bll.Delete(id);
            }

            FillDataGrid();
        }

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product p = bll.Read(id);
            textBoxX3.Text = p.Name;
            textBoxX4.Text =Convert.ToString( p.Price);
            textBoxX1.Text =Convert.ToString( p.Stock);
            label1.Text = "ویرایش اطلاعات";
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            FillDataGrid(); 
        }
    }
}
