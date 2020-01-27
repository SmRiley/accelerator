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
using System.Runtime.InteropServices;
using System.Threading;

namespace SauYoo
{
    public partial class Form3 : Form
    {
        static Form5 form5;
        static Form6 form6;
        static Http_Class Http_class = new Http_Class();
        public Form3()
        {
            InitializeComponent();
            Common.form3 = this;
            Activated += Control_Class.Form_Activated;
            Deactivate += Control_Class.Form_Deactivate;
            if (Common.form5 == null || Common.form6 == null)
            {
                form5 = new Form5();
                form6 = new Form6();
            }
            else {
                form5 = Common.form5;
                form5.get_User_Info();
                form6 = Common.form6;            
            }
        }

        //功能类
        static Auto_Class Auto_class = new Auto_Class();
        //定时器创建
        static System.Threading.Timer Timer_Link_Date;

        private IntPtr Active_Handle;
        private IntPtr active_Handle {
            set { if (value != Active_Handle) {
                    Active_Handle = value;
                    ActiveForm_BeChange(Active_Handle);
                }
            }
            get { return Active_Handle; }
        }

        private void ActiveForm_BeChange(IntPtr ActiveHandle) {
            if (this.Handle == ActiveHandle && form5.Visible)
            {
                form5.TopMost = true;
            }
            else {
                form5.TopMost = false;
            }
        }





        private void Form3_Load(object sender, EventArgs e)
        {
            Auto_ChangePicBoxSize();
            Auto_class.Change_Font(new Label[] { label1,label2,label3,link_State});
            Auto_class.Show_Dynamic(this);
        }


        //鼠标点击传值
        private void Form3_MouseDown(object sender, MouseEventArgs e)
        {
            Auto_class.Form_Drag(true, e.Location.X, e.Location.Y);
        }
        //鼠标松开重置坐标
        private void Form3_MouseUp(object sender, MouseEventArgs e)
        {
            Auto_class.Form_Drag(false, 0, 0);
        }
        //鼠标移动赋值
        private void Form3_MouseMove(object sender, MouseEventArgs e)
        {
            Auto_class.Form_Drag(this);
            form5.get_location(this);
            form6.get_location(this);
        }

        //自动适配图片框位置
        private void Auto_ChangePicBoxSize() {
            pictureBox1.Height = pictureBox1.Image.Height;
            pictureBox1.Width = pictureBox1.Image.Width;
            pictureBox1.Left = (this.Width - pictureBox1.Width) / 2;
            pictureBox1.Top = pictureBox2.Top - ((pictureBox1.Image.Height - pictureBox2.Height) / 2);
        }

        public void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {          
            if (Common.Is_Linked)
            {
                Closure_Link();
            }
            form5.Hide();
            form6.Hide();
            Auto_class.Close_Dynamic(this);
        }

        /// <summary>
        /// 链接服务器
        /// </summary>
        public bool Button_enabled = true;
        public async void Button6_Click(object sender, EventArgs e)
        {
            link_State.Text = "";
            if (Button_enabled) {
                Button_enabled = false;
                //判断状态
                if (!Common.Is_Linked)
                {
                    //判断是否到期

                    if (Common.ReNew_time > 0)
                    {
                        if (Common.host == "")
                        {
                            form6.Show();
                        }
                        else {
                            await Start_Link();
                            link_State.ForeColor = Color.FromArgb(250, 250, 250);
                        }
                       
                    }
                    else {
                        link_State.Text = "账号已到期,请续费后使用";
                        link_State.ForeColor = Color.FromArgb(180,0,0);
                    }
                    
                }
                else
                {
                    Closure_Link();
                }
                Button_enabled = true;
                Button6_MouseLeave(null, null);
            }
        }

        private async Task Start_Link() {
            pictureBox2.Hide();
            bool link_result = Server_Start();
            if (link_result)
            {
                button6.Image = images_Res.加速_进行时;
                pictureBox1.Image = images_Res.加速_初始;
                Auto_ChangePicBoxSize();
                Auto_class.Delay(1750);
                label1.Text = "云节点部署中";
                pictureBox1.Image = images_Res.加速_等待判断状态;
                //判断是否成功链接
                bool Check_Result = await Check_Link_State(5);
                if (Check_Result)
                {
                    pictureBox1.Image = images_Res.加速_善后;
                    Auto_class.Delay(1600);
                    pictureBox1.Image = images_Res.加速_成功;
                    Common.Is_Linked = true;
                    Timer_Link_Date = new System.Threading.Timer(Timer_Event, null, 0, 1000);
                    TimeNow = DateTime.Now;
                    label1.Text = Common.Servers_Region+ "云节点加速中";
                }
                else {
                    link_State.Text = "无法连接到服务器,请稍后重试";
                    Auto_class.Delay(1500);
                    Closure_Link();
                }
               
            }
            else
            {
                Closure_Link();
            }
        }

        private void Closure_Link() {         
            Auto_class.Kill_Link();
            pictureBox1.Image = images_Res.加速_断开;
            Auto_class.Delay(650);
            pictureBox1.Image = images_Res.中心圆;          
            Auto_ChangePicBoxSize();
            Common.Is_Linked = false;
            link_State.Text = "";
            pictureBox2.Show();
            if (Timer_Link_Date != null) {
                Timer_Link_Date.Dispose();
            }
            label1.Text = "等待加速";
        }

        /// <summary>
        /// 执行检测
        /// </summary>
        /// <param name="Check_Num">检测次数</param>
        /// <returns></returns>
        private async Task<bool> Check_Link_State(int Check_Num) {
            bool Link_State = false;
            for (int i =0;i<Check_Num; i++) {
                Auto_class.Delay(3200);
                Link_State = await Http_class.get_Link_state();
                if (Link_State)
                {
                    break;                        
                }
            
            }
            if (Link_State)
            {
                return true;
            }
            else {
                return false;
            }
            
        }

        /// <summary>
        /// 开始加速进程
        /// </summary>
        private bool Server_Start() {
            //链接参数
            string host = Common.host ;
            string port = Common.port;
            string passwd = Common.passwd;
            string xtudp = Common.xtudp;
            string method = Common.method;
            string dns = Common.dns;
            string File_Path =  Common.Running_Path+ @"\Bin\Gsteep.exe";
            string Link_Par = "--host " + host + " --port " + port + " --passwd " + passwd + " --xtudp " + xtudp + " --method " + method + " --dns "+ dns;
            Process Link_Process = new Process();
            Link_Process.StartInfo = new ProcessStartInfo(File_Path, Link_Par);
            Link_Process.StartInfo.CreateNoWindow = true;
            Link_Process.StartInfo.UseShellExecute = false;
            try
            {
                Link_Process.Start();
            }
            catch
            {
                MessageBox.Show("文件丢失,请重新安装程序");
                return false;
            }
            return true;
        }


        /// <summary>
        /// 结束加速进程
        /// </summary>




     

        static Form4 form4 = new Form4();
        private void Button1_Click(object sender, EventArgs e)
        {
            form4.get_location(this);
        }


        private void Button4_Click(object sender, EventArgs e)
        {
            if (form5.Visible)
            {
                Auto_class.Close_Dynamic(form5,this.Button_enabled);
                form5.Hide();
            }
            else
            {
                form6.Hide();
                if (form5.First_Load)
                {
                    form5.Show();
                }
                else {
                    Auto_class.Show_Dynamic(form5,this.Button_enabled);
                    form5.Show();
                    form5.Activate();
                }
                              
            }


        }
      
        private void Button5_Click(object sender, EventArgs e)
        {
            if (form6.Visible)
            {
                Auto_class.Close_Dynamic(form6,this.Button_enabled);
                form6.Hide();
            }
            else
            {
                form5.Hide();
                if (form6.First_Load)
                {
                    form6.Show();
                }
                else {
                    Auto_class.Show_Dynamic(form6,this.Button_enabled);
                    form6.Show();
                    form6.Activate();
                }
                               
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            form5.Hide();
            form6.Hide();
            Auto_class.Close_Dynamic(this);
            this.WindowState = FormWindowState.Minimized;
        }

        Form7 exitform = new Form7();
        private void Button3_Click(object sender, EventArgs e)
        {
            
            exitform.Show();
            exitform.Left = this.Left + 30;
            exitform.Top = this.Top + 200;
        }


        /// <summary>
        /// 小图标被单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>



        private void Show_FormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Auto_class.Show_Dynamic(this);
            this.Show();
        }

        private void Exit_FormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        //因为菜单窗口失去焦点即消失,所以以下函数不考虑菜单窗口的情况
        /// <summary>
        /// 窗口被最小化.隐藏其余窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form3_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized) {
                form5.Hide();
                form6.Hide();
            }
        }

       

        DateTime TimeNow = new DateTime();
        TimeSpan TimeCount = new TimeSpan();

        //定义操作label值委托
        private delegate void Set_Date_Delegate();

        int label_end_num = 0;
        private  void Set_Date() {
            link_State.Text = "加速时长:" + string.Format("{0:00}:{1:00}:{2:00}", TimeCount.Hours, TimeCount.Minutes, TimeCount.Seconds);
            if (label_end_num < 3)
            {
                label1.Text = label1.Text + ".";
                label_end_num++;
            }
            else {
                label1.Text = label1.Text.Substring(0, label1.Text.Length - 3);
                label_end_num = 0;
            }
        }

        private void Timer_Event(Object Ob)
        {
            TimeCount = DateTime.Now - TimeNow;
            link_State.Invoke(new Set_Date_Delegate(Set_Date));
        }

        #region 控件样式
        private void Button1_MouseEnter(object sender, EventArgs e)
        {
            button1.Image = images_Res.菜单按钮点燃;
        }

        private void Button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Image = images_Res.菜单按钮默认;
        }

        private void Button2_MouseEnter(object sender, EventArgs e)
        {
            button2.Image = images_Res.最小化点亮;
        }

        private void Button2_MouseLeave(object sender, EventArgs e)
        {
            button2.Image = images_Res.最小化默认;
        }

        private void Button3_MouseEnter(object sender, EventArgs e)
        {
            button3.Image = images_Res.销毁点亮;
        }

        private void Button3_MouseLeave(object sender, EventArgs e)
        {
            button3.Image = images_Res.销毁默认;
        }

        private void Button4_MouseEnter(object sender, EventArgs e)
        {
            button4.Image = images_Res.右按钮点亮;
        }

        private void Button4_MouseLeave(object sender, EventArgs e)
        {
            button4.Image = images_Res.右按钮默认;
        }

        private void Button5_MouseEntere(object sender, EventArgs e)
        {
            button5.Image = images_Res.设置按钮_点燃;
        }

        private void Button5_MouseLeave(object sender, EventArgs e)
        {
            button5.Image = images_Res.设置按钮_默认;
        }

        private void Button6_MouseEnter(object sender, EventArgs e)
        {
            if (Button_enabled)
            {
                if (Common.Is_Linked)
                {
                    button6.Image = images_Res.加速停止点燃;
                }
                else
                {
                    button6.Image = images_Res.加速_点燃;
                }
            }
        }

        private void Button6_MouseLeave(object sender, EventArgs e)
        {
            if (Button_enabled)
            {
                if (Common.Is_Linked)
                {
                    button6.Image = images_Res.加速停止默认;
                }
                else
                {
                    button6.Image = images_Res.加速_默认;
                }
            }

        }










        #endregion


    }
}
