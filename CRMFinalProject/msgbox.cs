using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace CRMFinalProject
{
   public class msgbox
    {
        public DialogResult MyShowDialog(string Title,string FaInfo,string EngInfo,bool button,bool type)
        {
            MymsgBox m = new MymsgBox();
            m.label1.Text = Title;
            m.label2.Text = FaInfo;
            m.label3.Text = EngInfo;
           if(button)
            {
                m.buttonX2.Text= "خیر";
            }
            else
            {
                m.buttonX1.Visible = false;
            }
           if(type) 
            {
                m.BackColor = Color.FromArgb(216, 49, 91);
                m.pictureBox1.Image = Properties.Resources.icons8_warning_50; 
            }

            m.ShowDialog();
            return m.DialogResult;
            
        }
    }
}
