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
    public partial class Form2 : Form
    {
        static Auto_Class Auto_class = new Auto_Class();
        static Form M_Form;
        public Form2(Form main_Form)
        {
            M_Form = main_Form;           
            InitializeComponent();
            if (Common.Auto_Login)
            {           
                button1.Image = images_Res.多选框勾选;
                button2.Image = images_Res.多选框勾选;
            }
            else
            {
                if (Common.Remmber_Pass)
                {
                    button1.Image = images_Res.多选框勾选;
                }
            }
        }

        //窗口载入
        private void Form2_Load(object sender, EventArgs e)
        {
            this.Location = new Point(M_Form.Location.X, M_Form.Location.Y + M_Form.Height);
            Auto_class.Change_Font(new Label[] { label1, label2, label3, label4 });
            Auto_class.Show_Dynamic(this);
        }   
        

        //跟随主窗口操作
        public void follow_Main() {
            this.Location = new Point(M_Form.Location.X, M_Form.Location.Y + M_Form.Height);
            //窗口跟随置顶
            this.TopMost = M_Form.TopMost;
        }

        //鼠标悬停控件变色
        private void Mouse_In(Label text_Label,bool is_Mouse_In) {
            Color Mouse_leave = Color.FromArgb(0, 179, 116);
            text_Label.ForeColor = Mouse_leave;
            if (true) {
               // MessageBox.Show(text_Label.ForeColor.ToArgb());
            }
        }

        //标签变色


        public bool button_change_img(Button button, bool status) {
            if (status)
            {
                button.Image = images_Res.多选框默认;
                return false;
            }
            else {
                button.Image = images_Res.多选框勾选;
                return true;
            }
        }

        #region 控件样式
        private void labels_MouseEnter(object sender, EventArgs e)
        {
            Auto_class.label_color_change((Label)sender, true);
        }

        private void Labels_MouseLeave(object sender, EventArgs e)
        {
            Auto_class.label_color_change((Label)sender, false);
        }


       
        #endregion

        private void Label1_Click(object sender, EventArgs e)
        {
            Common.Remmber_Pass = button_change_img(button1, Common.Remmber_Pass);
            Common.Auto_Login = button_change_img(button2, true);
        }

        private void Label2_Click(object sender, EventArgs e)
        {
            Common.Auto_Login = button_change_img(button2, Common.Auto_Login);
            Common.Remmber_Pass = button_change_img(button1, false);
        }

        private void Label3_Click(object sender, EventArgs e)
        {
            Process.Start(Common.Web_adress+ "/register.php");
        }

        private void Label4_Click(object sender, EventArgs e)
        {
            Process.Start(Common.Web_adress + "/pwreset.php");
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Auto_class.Close_Dynamic(this);
        }
    }
}
