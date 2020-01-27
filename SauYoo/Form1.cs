using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace SauYoo
{
    public partial class Form1 : Form
    {

       public Form1()
        {
            InitializeComponent();
            Common.form1 = this;
            this.StartPosition = FormStartPosition.CenterScreen;
            Auto_class.Read_Font();
        }

        Form2 form2;

        


        //窗口载入事件
        private void Form1_Load(object sender, EventArgs e)
        {
            Read_Config();
            form2 = new Form2(this);                               
            Auto_class.Kill_Link();
            Auto_class.Once_Install();
            Auto_class.Change_Font(new Label[]{ label1,label2});
            Auto_class.Change_Font(new TextBox[] { textBox1, textBox2 });
            Auto_class.Show_Dynamic(this);
            Http_class.Check_version();
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            if (Common.Auto_Login)
            {
                button1_Click(null, null);
            }
            else if(!Common.Remmber_Pass){
                form2.Show();
            }
            
            
        }

        //自动处理类
        Auto_Class Auto_class = new Auto_Class();
        //HTTP请求类
        Http_Class Http_class = new Http_Class();
        //鼠标点击传值
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Auto_class.Form_Drag(true,e.Location.X, e.Location.Y);
        }
        //鼠标松开重置坐标
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Auto_class.Form_Drag(false, 0, 0);           
        }
        //鼠标移动赋值
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Auto_class.Form_Drag(this);
            form2.follow_Main();
        }

        //检查编辑框内容合法性
        private bool check_TextBox() {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox1.Text != "请输入账号" && textBox2.Text != "请输入密码")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //子程序登录
        static bool button_enabled = true;   
        public async void button1_Click(object sender, EventArgs e)
        {
            if (button_enabled) {
                if (check_TextBox())
                {
                    button_enabled = false;
                    label2.Text = "";
                    string Post_User = textBox1.Text;
                    string Post_Pass = textBox2.Text;
                    button1.Image = images_Res.登录_登录中_;
                    bool Login_Result = await Http_class.Login(Post_User, Post_Pass);
                    if (Login_Result)
                    {
                            Form form3 = new Form3();
                            Set_Config(Post_User, Post_Pass);
                            form2.Hide();
                            Auto_class.Close_Dynamic(this);
                            this.Hide();
                            form3.Show();                                                                           
                    }
                    else {
                        label2.Text = Common.Error_Info;                       
                    }
                   
                    
                    //还原按钮
                    button_enabled = true;
                    TextBox1_TextChanged(null, null);
                    
                }
            }
            


        }

        IniFiles W_Conifg = new IniFiles();
        Pass_Deal encryption = new Pass_Deal();
          /// <summary>
          /// 写入记住密码
          /// </summary>
          /// <param name="User">账号</param>
          /// <param name="Pass">密码</param>
        private void Set_Config(string User,string Pass) {          
            string[] Pass_Deal_Result;
            if (Common.Remmber_Pass)
            {
                Pass_Deal_Result = encryption.encode_Str(Pass);
                W_Conifg.IniWriteValue("UserInfo", "UserName", User);
                W_Conifg.IniWriteValue("UserInfo", "UserPass", Pass_Deal_Result[0]);
                W_Conifg.IniWriteValue("UserInfo", "PassKeys", Pass_Deal_Result[1]);
                W_Conifg.IniWriteValue("Config", "Remmber_Pass", "true");
                if (Common.Auto_Login)
                {
                    W_Conifg.IniWriteValue("Config", "Auto_Login", "true");
                }
                else {
                    W_Conifg.IniWriteValue("Config", "Auto_Login", "false");
                }

                //配置文件隐藏
                File.SetAttributes(W_Conifg.FilesPath, FileAttributes.Hidden);
            }
            else {
                W_Conifg.DeleteIniFile();
            }

        }

        /// <summary>
        /// 读取配置
        /// </summary>
        private void Read_Config() {
            try {
                Common.Remmber_Pass = Convert.ToBoolean(W_Conifg.IniReadValue("Config", "Remmber_Pass"));
                Common.Auto_Login = Convert.ToBoolean(W_Conifg.IniReadValue("Config", "Auto_Login"));
                if (Common.Remmber_Pass)
                {
                    this.textBox1.Text = W_Conifg.IniReadValue("UserInfo", "UserName");
                    string Encode_Pass = W_Conifg.IniReadValue("UserInfo", "UserPass");
                    string Pass_Keys = W_Conifg.IniReadValue("UserInfo", "PassKeys");
                    textBox2_Enter(null, null);
                    this.textBox2.Text = encryption.decode_Str(Encode_Pass, Pass_Keys);
                }
                else {
                    form2.Show();
                }
            } catch {
                Common.Remmber_Pass = false;
                Common.Auto_Login = false;
            }
            
        }


        private void button1_MouseEnter(object sender, EventArgs e)
        {
            if (check_TextBox() && button_enabled)
            {
                button1.Image = images_Res.登录点燃;
            }
            else if(button_enabled)
            {
                button1.Image = images_Res.登录默认_无内容_;
            }
        }
        
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            if (button_enabled) {
                TextBox1_TextChanged(null, null);
            }
            
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            pictureBox1.Image = images_Res.编辑框选中;
            string checkText = textBox1.Text;
            switch (checkText)
            {
                case "请输入账号":
                    textBox1.Text = "";
                    break;
                case "":
                    textBox1.Text = "请输入账号";
                    break;
                default:
                    break;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            pictureBox2.Image = images_Res.编辑框选中;
            string checkText = textBox2.Text;
            switch (checkText)
            {
                case "请输入密码":
                    textBox2.Text = "";
                    textBox2.UseSystemPasswordChar = true;
                    break;
                case "":
                    textBox2.UseSystemPasswordChar = false;
                    textBox2.Text = "请输入密码";
                    break;
                default:
                    textBox2.UseSystemPasswordChar = true;
                    break;
            }
        }
      
        private void button2_Click(object sender, EventArgs e)
        {
            form2.Hide();
            Auto_class.Close_Dynamic(this);
            this.WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            form2.Hide();
            Auto_class.Close_Dynamic(this);
            Application.Exit();
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            button4.Image = images_Res.下端按钮点燃;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.Image = images_Res.下端按钮默认;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (form2.Visible) {
                form2.Visible = false;
            }
            else {
                form2.Show();
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (check_TextBox())
            {
                button1.Image = images_Res.登录默认_有内容_;
            }
            else {
                button1.Image = images_Res.登录默认_无内容_;
            }
        }

        #region 控件样式
        private void textBox1_Leave(object sender, EventArgs e)
        {
            this.textBox1_Enter(null, null);
            pictureBox1.Image = images_Res.编辑框背景;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            this.textBox2_Enter(null, null);
            pictureBox2.Image = images_Res.编辑框背景;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.Image = images_Res.最小化点亮;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.Image = images_Res.最小化默认;
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.Image = images_Res.销毁点亮;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.Image = images_Res.销毁默认;
        }


        #endregion


    }
}
