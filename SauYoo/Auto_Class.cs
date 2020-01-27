using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using System.Threading;
using System.IO;
using System.Drawing.Text;

namespace SauYoo
{


    class Auto_Class
    {


        //获取鼠标操作
        static int enterX, enterY;
        static bool isDrag = false;
        /// <summary>
        /// 标签对象
        /// </summary>
        /// <param name="is_Drag">是否拖动</param>
        /// <param name="x">鼠标X坐标</param>
        /// <param name="y">鼠标Y坐标</param>
        public void Form_Drag(bool is_Drag, int x, int y) {
            isDrag = is_Drag;
            if (isDrag) {
                enterX = x;
                enterY = y;
            }
            else {
                isDrag = is_Drag;
                enterX = 0;
                enterY = 0;
            }
        }

        /// <summary>
        /// 无边框拖动
        /// </summary>
        /// <param name="form">窗口对象</param>
        public void Form_Drag(Form form) {
            if (isDrag) {
                int newPointX = Control.MousePosition.X;
                int newPointY = Control.MousePosition.Y;
                Point newPoint = new Point(newPointX - enterX, newPointY - enterY);
                form.Location = newPoint;
            }

        }

        /// <summary>
        /// 判断标签是否在控件内
        /// </summary>
        /// <param name="control">控件对象</param>
        /// <returns></returns>
        public bool is_Mouse_In(Control control) {
            //获取标签相对活动区坐标
            Point p = new Point(0, 0);
            //获取标签相对屏幕坐标
            int label_W_MIN = control.PointToScreen(p).X;
            int label_H_MIN = control.PointToScreen(p).Y;
            int label_W_MAX = control.PointToScreen(p).X + control.Width;
            int label_H_MAX = control.PointToScreen(p).Y + control.Height;
            //鼠标坐标
            System.Threading.Thread.Sleep(10);
            int Mouse_X = Control.MousePosition.X;
            int Mouse_Y = Control.MousePosition.Y;
            //MessageBox.Show(control.PointToScreen(p).X +"/"+ control.PointToScreen(p).Y,"标签屏幕坐标");
            //MessageBox.Show(Mouse_X + "/" + Mouse_Y,"鼠标坐标");
            if (label_W_MIN <= Mouse_X && Mouse_X <= label_W_MAX && label_H_MIN <= Mouse_Y && Mouse_Y <= label_H_MAX)
            {
                return true;
            }
            else {
                return false;
            }
        }


        /// <summary>
        /// 标签变色
        /// </summary>
        /// <param name="label">标签对象</param>
        /// <param name="is_Mouse_in">标签选中状态</param>
        public void label_color_change(Label label, bool is_Mouse_in)
        {
            Color common_color = Color.FromArgb(255, 255, 255);
            Color new_color = Color.FromArgb(0, 179, 116);
            label.ForeColor = is_Mouse_in ? new_color : common_color;
        }

        public void label_color_change(Label[] labels, bool is_Mouse_in) {
            Color common_color = Color.FromArgb(255, 255, 255);
            Color new_color = Color.FromArgb(0, 179, 116);
            foreach (Label label in labels) {
                label.ForeColor = is_Mouse_in ? new_color : common_color;
            }
        }

        /// <summary>
        /// 取随机字符串
        /// </summary>
        /// <param name="string_len">取字符串长度</param>
        /// <returns></returns>
        public string random_String(int string_len)
        {
            int[] int0_9 = { 48, 58 };
            int[] a_z = { 97, 123 };
            string random_string = null;
            Random random = new Random();
            for (int i = 0; i < string_len; i++)
            {
                int i_or_l = random.Next(0, 2);
                int random_int = i_or_l == 0 ? random.Next(int0_9[0], int0_9[1]) : random.Next(a_z[0], a_z[1]);
                random_string += Convert.ToChar(random_int).ToString();
            }

            return random_string;
        }

        /// <summary>
        /// 取字符串中间字符串
        /// </summary>
        /// <param name="s">待查询文本</param>
        /// <param name="leftStr">左字符串</param>
        /// <param name="rightStr">右字符串</param>
        /// <returns></returns>
        public string Search_string(string s, string leftStr, string rightStr)
        {
            int n1, n2;
            n1 = s.IndexOf(leftStr, 0) + leftStr.Length;   //开始位置  
            n2 = s.IndexOf(rightStr, n1);               //结束位置
            //判断左右元素是否存在
            if (n1 - leftStr.Length == -1 || n2 == -1)
            {
                return "";
            }
            else {
                //取搜索的条数，用结束的位置-开始的位置,并返回   
                return s.Substring(n1, n2 - n1);
            }

        }

        /// <summary>
        /// 取字符串匹配字符串_所有
        /// </summary>
        /// <param name="s"></param>
        /// <param name="leftStr"></param>
        /// <param name="rightStr"></param>
        /// <returns></returns>
        List<string> Search_Result = new List<string>();
        public string[] Search_string_All(string s, string leftStr, string rightStr)
        {
            int n1, n2, Star_int = 0;
            n1 = s.IndexOf(leftStr, 0) + leftStr.Length;   //开始位置  
            n2 = s.IndexOf(rightStr, n1);               //结束位置
            //判断左右元素是否存在
            if (Search_Result.Count == 0 && (n1 - leftStr.Length == -1 || n2 == -1))
            {
                return null;
            }
            else if (Search_Result.Count != 0 && (n1 - leftStr.Length == -1 || n2 == -1))
            {
                string[] Result = new string[Search_Result.Count];
                Search_Result.CopyTo(Result);
                Search_Result.Clear();
                return Result;
            }
            else {
                Search_Result.Add(s.Substring(n1, n2 - n1));
                Star_int = n1 + last_Result.Length;
                s = s.Substring(Star_int);
                return Search_string_All(s, leftStr, rightStr);
            }


        }



        /// <summary>
        /// 取字符串中间字符串—最大查询
        /// </summary>
        /// <param name="s">待查询文本</param>
        /// <param name="leftStr">左字符串</param>
        /// <param name="rightStr">右字符串</param>
        /// <returns></returns>
        static int Search_int = 0;
        static string last_Result = "";
        public string Search_string_Max_time(string s, string leftStr, string rightStr) {
            int n1, n2, Star_int;
            string Result;
            string next_search;
            n1 = s.IndexOf(leftStr, 0) + leftStr.Length;   //开始位置  
            n2 = s.IndexOf(rightStr, n1);               //结束位置
            //判断左右元素是否存在
            if ((n1 - leftStr.Length == -1 || n2 == -1) && Search_int == 0)
            {
                return "";
            }
            else if ((n1 - leftStr.Length == -1 || n2 == -1) && Search_int != 0)
            {
                Search_int = 0;
                Result = last_Result;
                last_Result = "";
                return Result;
            }
            else {
                Search_int++;
                next_search = time_Reset(s.Substring(n1, n2 - n1)).ToString();
                //比较二者结果
                if (last_Result != "") {
                    TimeSpan time_span = Convert.ToDateTime(last_Result) - Convert.ToDateTime(next_search);
                    if (time_span.TotalSeconds < 0)
                    {
                        last_Result = next_search;
                    }
                }
                else {
                    last_Result = next_search;
                }

                Star_int = n1 + last_Result.Length;
                s = s.Substring(Star_int);
                return Search_string_Max_time(s, leftStr, rightStr);
            }

        }


        /// <summary>
        /// 时间格式化
        /// </summary>
        public DateTime time_Reset(string time_string) {
            string time_reset = "";
            for (int i = 0; i < time_string.Length; i++) {
                if (time_string.Substring(i, 1) != "-")
                {
                    time_reset += time_string.Substring(i, 1);
                }
                else {
                    time_reset += "/";
                }
            }
            return Convert.ToDateTime(time_reset);
        }


        /// <summary>
        /// 时间转换
        /// </summary>
        /// <param name="Renew_time"></param>
        /// <returns></returns>
        public int Expiry_time(Double Renew_time) {
            Double Expiry_time = Renew_time / (60 * 60 * 24);
            return (int)Expiry_time;
        }


        //延迟
        public void Delay(int milliSecond)
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)
            {
                Application.DoEvents();
            }
        }

        public int Ping_IP(string Host_IP)
        {
            Ping ping = new Ping();
            int reply_time;
            try
            {
                reply_time = Convert.ToInt32(ping.Send(Host_IP, 120).RoundtripTime) / 2;
            }
            catch {
                reply_time = -1;
            }
            return reply_time;
        }

        /// <summary>
        /// 杀进程
        /// </summary>
        public void Kill_Link()
        {
            Process[] Link_Process = Process.GetProcessesByName("Gsteep");
            try
            {
                foreach (Process Link_Processes in Link_Process)
                {
                    Link_Processes.Kill();
                }
            }
            catch
            {

            }


        }

        /// <summary>
        /// 首次安装
        /// </summary>
        public void Once_Install() {
            string Get_File_Path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"/Gsteep/Win32/wpcap.dll";

            if (!File.Exists(Get_File_Path))
            {
                try
                {
                    Process Installer = new Process();
                    string File_Path = Common.Running_Path + "/Cap/install.msi";
                    Installer.StartInfo = new ProcessStartInfo(File_Path);
                    Installer.Start();
                    Installer.WaitForExit();
                }
                catch {
                    MessageBox.Show("驱动文件丢失,请重新安装软件!");
                }
            }

        }

        public void Read_Font() {
            string Font_Path = Common.Running_Path + @"/Fonts/FZSJ.TTF";
            PrivateFontCollection pfc = new PrivateFontCollection();
            try
            {
                pfc.AddFontFile(Font_Path);
                Common.MyFont_Families = pfc.Families[0];
            }
            catch {
                Font WRYH = new Font("微软雅黑", 4);
                Common.MyFont_Families = WRYH.FontFamily;
            }
        }

        public void Change_Font(Label[] Labels) {
            foreach (Label label in Labels) {
                label.Font = new Font(Common.MyFont_Families, label.Font.Size);
            }
        }
        public void Change_Font(TextBox[] TextBoxs)
        {
            foreach (TextBox textBox in TextBoxs)
            {
                textBox.Font = new Font(Common.MyFont_Families, textBox.Font.Size);
            }
        }

        public void Change_Font(Button[] Buttons)
        {
            foreach (Button button in Buttons)
            {
                button.Font = new Font(Common.MyFont_Families, button.Font.Size);
            }
        }

        public void output(string Out_text) {
            Console.WriteLine(Out_text);
        }

        /// <summary>
        /// 获取活动窗口句柄
        /// </summary>
        /// <returns></returns>
        [DllImport("user32", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        //自左向右显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        private const int AW_HOR_POSITIVE = 0x0001;
        //自右向左显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        private const int AW_HOR_NEGATIVE = 0x0002;
        //自顶向下显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        private const int AW_VER_POSITIVE = 0x0004;
        //自下向上显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志该标志
        private const int AW_VER_NEGATIVE = 0x0008;
        //若使用了AW_HIDE标志，则使窗口向内重叠；否则向外扩展
        private const int AW_CENTER = 0x0010;
        //隐藏窗口
        private const int AW_HIDE = 0x10000;
        //激活窗口，在使用了AW_HIDE标志后不要使用这个标志
        private const int AW_ACTIVE = 0x20000;
        //使用淡入淡出效果
        private const int AW_BLEND = 0x80000;
        //使用滑动类型动画效果，默认为滚动动画类型，当使用AW_CENTER标志时，这个标志就被忽略
        private const int AW_SLIDE = 0x40000;

        /// <summary>
        /// 窗口加载特效
        /// </summary>
        public void Show_Dynamic(Form form)
        {
            AnimateWindow(form.Handle, 500,AW_BLEND);
        }

        /// <summary>
        /// 窗口关闭特效
        /// </summary>
        /// <param name="form"></param>
        public  void Close_Dynamic(Form form) {
            AnimateWindow(form.Handle, 500, AW_BLEND | AW_HIDE);
        }

        public void Show_Dynamic(Form form,bool button_enable)
        {
            if (button_enable) {
                AnimateWindow(form.Handle, 500, AW_BLEND);
            }
            
        }

        /// <summary>
        /// 窗口关闭特效
        /// </summary>
        /// <param name="form"></param>
        public void Close_Dynamic(Form form,bool button_enable)
        {
            if (button_enable)
            {
                AnimateWindow(form.Handle, 500, AW_BLEND | AW_HIDE);
            }
        }
    }
}
