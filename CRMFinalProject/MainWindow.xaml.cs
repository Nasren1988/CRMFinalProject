using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Windows.Media.Effects;
using Microsoft.Win32;
using BE;
using BLL;

namespace CRMFinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            LoginForm l = new LoginForm();  
            InitializeComponent();
            OpenWinForm(l);
        }
        void OpenWinForm(Form l)
        {
            //Window w = (Window)this.FindName("l");
            BlurEffect blur = new BlurEffect();
            this.Effect = blur;
             Effect = null;
             blur.Radius = 15;
            //w.BitmapEffect = blur;
            l.ShowDialog();
            //blur.Radius = 0;
            //w.BitmapEffect = blur;

        }
        public User LoggedInUser = new User(); 
        DashBordBLL Dbll= new DashBordBLL();
        LoginForm lf = new LoginForm();      
        void RefreshForm()
        {
            UserNameTxt.Text = LoggedInUser.UserName;
            PersonNameTxt.Text = LoggedInUser.Name;
            RemindersCountTxt.Text = Dbll.UserRemindersCount(LoggedInUser);
            CustCont.Text = Dbll.CustomerCount();
            SellCont.Text = Dbll.SellsCount();
            int a = 0;
            foreach (var item in Dbll.GetUserReminder(LoggedInUser))
            {
                if (a < 7)
                {
                    ReminderUc R = new ReminderUc();
                    MainGrid.Children.Add(R);
                    R.ReminderTitleTxt.Text = item.Title;
                    R.ReminderTitleTxt.Text = item.ReminderInfo;
                    Grid.SetRow(R, 5 );
                    Grid.SetColumnSpan(R, 6);
                    a++;
                }

            }

        }

        UserBLL Ubll= new UserBLL();    
        msgbox m = new msgbox();    
      

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ReminderForm Re = new ReminderForm();    
            OpenWinForm(Re);
            RefreshForm();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (Ubll.Access(LoggedInUser, "بخش مشتریان", 1))
            {
                CustomerForm customerForm = new CustomerForm();
                OpenWinForm(customerForm);
                RefreshForm();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ورود به این قسنت را ندارید", "", false, true);
            }
           
        }

        private void TextBlock_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            ProductForm productForm = new ProductForm();    
            OpenWinForm(productForm);
            RefreshForm();
        }

        private void TextBlock_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            UserForm userForm = new UserForm();
            OpenWinForm(userForm);
            RefreshForm();
        }

        private void TextBlock_MouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {
            ReminderForm reminderForm = new ReminderForm();
            OpenWinForm(reminderForm);
            RefreshForm();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           msgbox m = new msgbox();
            m.MyShowDialog("", "", "", true, true);
        }

        private void TextBlock_MouseLeftButtonDown_5(object sender, MouseButtonEventArgs e)
        {
            ActivityCategoryForm  activityCategoryForm = new ActivityCategoryForm();
            OpenWinForm(activityCategoryForm);
            RefreshForm();

        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            App.Current.Shutdown(); 
        }

        private void TextBlock_MouseLeftButtonDown_6(object sender, MouseButtonEventArgs e)
        {
            ActivityForm activityForm = new ActivityForm();
            OpenWinForm (activityForm); 
            RefreshForm();
        }

        private void TextBlock_MouseLeftButtonDown_7(object sender, MouseButtonEventArgs e)
        {
            InvoiceForm invoiceForm = new InvoiceForm();
            OpenWinForm(invoiceForm);
            RefreshForm();
           
        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            //LoginForm f = new LoginForm();
            //OpenWinForm(f);
            RefreshForm();

        }

        private void TextBlock_MouseLeftButtonDown_8(object sender, MouseButtonEventArgs e)
        {
            SmsPanel s = new SmsPanel();
            OpenWinForm(s);
            RefreshForm();  
        }

        private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            LoggedInUser = null;
            OpenWinForm(lf);
            RefreshForm();  
           
        }
    }
}
