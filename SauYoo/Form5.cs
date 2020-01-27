using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace SauYoo
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            Common.form5 = this;
            InitializeComponent();
            Deactivate += Control_Class.Form_Deactivate;           
        }

        Auto_Class Auto_class = new Auto_Class();
        public bool First_Load = true;
        public void Form5_Load(object sender, EventArgs e)
        {
            get_User_Info();
            Auto_class.Change_Font(new Label[] { label1, label2 });
            Auto_class.Change_Font(new Button[] { button2, button3, button4 });
            get_location(Common.form3);
            Auto_class.Show_Dynamic(this,Common.form3.Button_enabled);
            First_Load = false;
        }


        public void get_User_Info() {
            //label1.Text = Common.User_Name;

            label1.Left = (this.Width - label1.Width) / 2;
            int expiry_Date = Auto_class.Expiry_time(Common.ReNew_time);
            if (Convert.ToInt32(expiry_Date) > 0)
            {
                label2.Text = "剩余有效期:" + expiry_Date + "天";
            }
            else
            {
                label2.Text = "剩余有效期:已到期";
            }

            label2.Left = (this.Width - label2.Width) / 2;
        }

        /// <summary>
        /// 使窗口在Alt+Tab中隐藏
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_TOOLWINDOW = 0x80;
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_TOOLWINDOW;      // 不显示在Alt+Tab
                return cp;
            }
        }

        public void get_location(Form M_Form) {
            this.Left = M_Form.Left + M_Form.Width + 3;
            this.Top = M_Form.Top + 40;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            Auto_class.Close_Dynamic(this,Common.form3.Button_enabled);
            this.Hide();
        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Auto_class.Expiry_time(Common.ReNew_time)) > 0)
            {
                Process.Start(Common.Web_adress + "/token.php?token=" + Common.Http_Cookies + "&to=renew");
            }
            else {
                Process.Start(Common.Web_adress + "/token.php?token=" + Common.Http_Cookies + "&to=cart");
            }
            
        }


        #region 控件样式
        private void Button1_MouseEnter(object sender, EventArgs e)
        {
            button1.Image = images_Res.销毁点亮;
        }

        private void Button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Image = images_Res.销毁默认;
        }

        private void Button2_MouseEnter(object sender, EventArgs e)
        {
            button2.Image = images_Res.客服_点燃;
        }

        private void Button2_MouseLeave(object sender, EventArgs e)
        {
            button2.Image = images_Res.客服_默认;
        }

        private void Button3_MouseEnter(object sender, EventArgs e)
        {
            button3.Image = images_Res.会员_点燃;
        }

        private void Button3_MouseLeave(object sender, EventArgs e)
        {
            button3.Image = images_Res.会员_默认;
        }

        private void Button4_MouseEnter(object sender, EventArgs e)
        {
            button4.Image = images_Res.游戏_点燃;
        }

        private void Button4_MouseLeave(object sender, EventArgs e)
        {
            button4.Image = images_Res.游戏_默认;
        }

        private void Button5_MouseEnter(object sender, EventArgs e)
        {
            button5.Image = images_Res.续费_点燃;
        }

        private void Button5_MouseLeave(object sender, EventArgs e)
        {
            button5.Image = images_Res.续费;
        }
        #endregion

    }
}
