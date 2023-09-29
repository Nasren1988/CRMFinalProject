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
using Stimulsoft.Report;

namespace CRMFinalProject
{
    public partial class InvoiceForm : Form
    {
        public InvoiceForm()
        {
            InitializeComponent();
        }
        msgbox m = new msgbox();
        Customer C = new Customer();    
        public List<Product> Products = new List<Product>();        
        CustomerBLL Cbll = new CustomerBLL();
        UserBLL Ubll = new UserBLL(); 
        ProductBLL Pbll  = new ProductBLL();
        InvoiceBLL Ibll = new InvoiceBLL(); 
        Product p = new Product();  
        void FillDataGrid1()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = Products;
            dataGridViewX1.Columns["id"].Visible = false;
            dataGridViewX1.Columns["Stock"].Visible = false;
            dataGridViewX1.Columns["DeleteStatus"].Visible = false;
            dataGridViewX1.Columns["Name"].HeaderText = "نام محصول";
            dataGridViewX1.Columns["Price"].HeaderText = "قیمت محصول";
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();    
        }

        private void InvoiceForm_Load(object sender, EventArgs e)
        {
            label4.Text = DateTime.Now.Date.ToString("yyyy/MM/dd");

             AutoCompleteStringCollection phone = new AutoCompleteStringCollection(); 
            foreach(var item in Cbll.ReadPhoneNumbers())
            {
                phone.Add(item); 
            }
            textBoxX1.AutoCompleteCustomSource = phone;
            AutoCompleteStringCollection names = new AutoCompleteStringCollection();    
            foreach(var item in Pbll.ReadNames())
            {
                names.Add(item);    
            }
            textBoxX2.AutoCompleteCustomSource = names;    
          

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            C = Cbll.Read(textBoxX1.Text);
            textBoxX1.Enabled = false;
            label2.Text = C.Name;
            label3.Text = C.Phone;
           
        }
       
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            p=Pbll.ReadN(textBoxX2.Text);
            Products.Add(p);
            FillDataGrid1();
            double sum = 0;
            string s = p.Name + " به ارزش " + p.Price.ToString("N0") + "تومان ";
            listBox1.Items.Add(s);  
             foreach(var item in Products) 
            {
                sum += item.Price;
                
            }
                    
              label5.Text = sum.ToString("N0");
            if (textBoxX4.Text == string.Empty)
            {
                textBoxX4.Text =Convert.ToString( 0);
            }
              label15.Text = (sum - Convert.ToDouble(textBoxX4.Text)).ToString();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Invoice i = new Invoice();  
            i.RegDate = DateTime.Now;
            if (checkBox1.Checked)
            {
                i.IsCheckout = true;
                i.CheckOutDate = DateTime.Now;  

            }
            else
            {
                i.IsCheckout= false;    
            }
            DialogResult res = m.MyShowDialog("اطلاعیه", Ibll.Create(i, C, Products) + " آیا قصد چاپ فاکتور را دارید", "", true, false);
            if(res == DialogResult.Yes)
            {
                StiReport sti = new StiReport();
                sti.Load(@"C:\Users\Asus\Desktop\Report.mrt");
                sti.Dictionary.Variables["InvoiceNum"].Value = Ibll.Read();
                sti.Dictionary.Variables["Date"].Value = label4.Text;
                sti.Dictionary.Variables["CustNum"].Value = label2.Text;
                sti.Dictionary.Variables["CustPhone"].Value = label3.Text;
                sti.RegBusinessObject("Product", Products);
                sti.Render();
                sti.Show();
           }
        }

       
    }
}
