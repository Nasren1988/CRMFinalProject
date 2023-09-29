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
    public partial class LoginForm : Form
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

        public LoginForm()
        {
            this.Controls.Add(r);
            this.Controls["RegisterAdmin"].Location = new Point(385, 798);
            this.Controls.Add(l);
            this.Controls["LoginUser"].Location = new Point(385, 798);

            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));

        }
        User u = new User();    
        UserBLL Ubll= new UserBLL();  
        List<string> usernames = new List<string>();        
        RegisterAdmin r = new RegisterAdmin();  
        Timer t1 = new Timer(); 
        Timer t2 = new Timer();
        Timer t3 = new Timer();
        
        LoginUser l = new LoginUser();  
        bool _IseRegisterd;
        public void LoadLoginForm()
        {
            t3.Enabled = true;
            t3.Interval = 15;
            t3.Tick += timer3_Tick;
            t3.Start();
        }
        int y = 390;
        int y2 = 798;
        int y3 = 798;
        private void LoginForm_Load(object sender, EventArgs e)
        {
            labelX1.Visible = true;
            t1.Enabled = true;
            t1.Interval = 15;
            t1.Tick += timer1_Tick;
            t1.Start();
  


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(progressBarX1.Value >= 100)
            {
                t1.Stop();   
                progressBarX1.Visible = false;
                labelX1.Visible = false;
                labelX2.Visible = true;
                t2.Enabled = true;
                t2.Interval = 1;
                t2.Tick += timer2_Tick;
                t2.Start(); 

            }
            else if(progressBarX1.Value ==45)
            {
                _IseRegisterd = Ubll.IseRegisterd();
                progressBarX1.Value++;
            }
            else
            {
                progressBarX1.Value++;
            }

        }
      

        private void timer2_Tick(object sender, EventArgs e)
        {
            if(labelX1.Location.Y >= 55)
            {
                y = y - 15;
                y2 = y2 - 30;
                labelX1.Location= new Point(468,y);
                if (_IseRegisterd)
                {
                    this.Controls["LoginUser"].Location = new Point(385, y2);

                }
                else
                {
                    this.Controls["RegisterAdmin"].Location = new Point(385, y2);
            }

        }
            else
            {
                t2.Stop();
                panel2.Visible = false;
            }
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            if (this.Controls["LoginUser"].Location.Y >= 55)
            {
                y3= y3 - 15;    
                this.Controls["LoginUser"].Location = new Point(385, y3);
            }
            else
            {
                t3.Stop();  
            }

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}