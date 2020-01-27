using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SauYoo
{
    public partial class Form6 : Form
    {

        Label[][] Label_Array = new Label[5][];
        Button[] Button_Array;
        Auto_Class Auto_class = new Auto_Class();
        public bool First_Load = true;
        static string[] Servers_IP;
        static int Server_ID = 0;
        public Form6()
        {
            InitializeComponent();
            Common.form6 = this;
            Deactivate += Control_Class.Form_Deactivate;           
            Button_Array = new Button[]{ C_button1, C_button2, C_button3, C_button4, C_button5 };
            Label_Array = new Label[][]{
                new Label[]{ Cloud_Server1, Cloud_Ping1, Cloud_State1 },
                new Label[]{ Cloud_Server2, Cloud_Ping2, Cloud_State2 },
                new Label[]{ Cloud_Server3, Cloud_Ping3, Cloud_State3 },
                new Label[]{ Cloud_Server4, Cloud_Ping4, Cloud_State4 },
                new Label[]{ Cloud_Server5, Cloud_Ping5, Cloud_State5 }
            };
            get_Servers_Info();         
        }


        private void Form6_Load(object sender, EventArgs e)
        {          
            Auto_class.Change_Font(new Label[]{ label1,label2,label3,label4});            
            for (int i=0;i<Label_Array.Length;i++) {
                Auto_class.Change_Font(Label_Array[i]);
            }
            get_location(Common.form3);
            Auto_class.Show_Dynamic(this,Common.form3.Button_enabled);
            First_Load = false;
        }

        /// <summary>
        /// 重写show()以加载服务器数据
        /// </summary>
       /*public new void  Show() {           
            Auto_Class.AnimateWindow(this.Handle, 500, Auto_Class.AW_BLEND);
            this.Visible = true;
        }*/

        public void get_location(Form M_Form)
        {
            this.Left = M_Form.Left + M_Form.Width+3;
            this.Top = M_Form.Top + 40;
        }

        

        /// <summary>
        /// 获取服务器信息
        /// </summary>
        public void get_Servers_Info() {
           /* Servers_IP = Common.Servers_Info[2];           
            int i = 0;
            foreach (string Server_Region in Common.Servers_Info[1])
            {
                foreach (Label label in Label_Array[i]) {
                    label.Visible = true;
                }
                Button_Array[i].Visible = true;
                
                Label_Array[i][0].Text = Server_Region;
                int Ping_Roundtriptime = Auto_class.Ping_IP(Servers_IP[i]);
                //检查节点可用性
                if (Common.Servers_Info[3][i] == "1")
                {
                    if (Ping_Roundtriptime >= 0)
                    {
                        Label_Array[i][1].Text = Ping_Roundtriptime.ToString() + "ms";
                        Label_Array[i][2].Text = "0%";
                    }
                    else
                    {
                        Label_Array[i][1].Text = "待重试";
                        Label_Array[i][2].Text = "99%";
                    }
                }
                else {
                    Label_Array[i][1].Text = "维护";
                    Label_Array[i][2].Text = "99%";
                    Button_Array[i].Enabled = Label_Array[i][0].Enabled = Label_Array[i][1].Enabled = false;
                }
                
                i++;
            }
            //获取完成,设置默认IP
            Servers_Change(0);*/
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

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Common.Is_Linked = false;
            Common.form3.Button6_Click(null, null);
        }

        
        

        private void Button7_Click(object sender, EventArgs e)
        {
            Auto_class.Close_Dynamic(this,Common.form3.Button_enabled);
            this.Hide();
        }
        private void Servers_Change(int Be_Change_ID)
        {
            //重置所有标签及按钮
            for (int i=0;i<Button_Array.Length;i++) {
                Button_Array[i].Image = images_Res.单选_未选中;
                Auto_class.label_color_change(Label_Array[i], false);
            }
            Server_ID = Be_Change_ID;
            Auto_class.label_color_change(Label_Array[Be_Change_ID], true);
            Button_Array[Be_Change_ID].Image = images_Res.单选;
            Common.host = Servers_IP[Be_Change_ID];
            Common.Servers_Region = Common.Servers_Info[1][Be_Change_ID];

        }



        private void C_buttons_Click(object sender, EventArgs e)
        {         
            for (int i=0;i<Button_Array.Length;i++) {
                if ((Button)sender == Button_Array[i]) {
                    Servers_Change(i);
                    break;
                }
            }          
        }

        private void Cloud_Servers_Click(object sender, EventArgs e)
        {
            for (int i=0;i<Label_Array.Length;i++) {
                if (one_to_three((Label)sender,Label_Array[i])) {
                    Servers_Change(i);
                    break;
                }
            }
        }

     

        #region 控件样式
        private void Cloud_Labels_MouseEnter(object sender, EventArgs e)
        {
            for (int i = 0; i < Label_Array.Length; i++)
            {
                if (one_to_three(sender,Label_Array[i]))
                {
                    Auto_class.label_color_change(Label_Array[i], true);
                    break;
                }
            }
        }

        private void Cloud_Labels_MouseLeave(object sender, EventArgs e)
        {
            for (int i=0;i<Label_Array.Length;i++) {
                bool Not_Now_ID = !one_to_three(sender,Label_Array[Server_ID]);
                if (one_to_three(sender,Label_Array[i]) && Not_Now_ID) {
                    Auto_class.label_color_change(Label_Array[i], false);
                    break;
                }
            }
            
        }

        /// <summary>
        /// 一对多比较
        /// </summary>
        /// <param name="one">单参,对比值</param>
        /// <param name="three">数组,被对比值</param>
        /// <returns></returns>
        private bool one_to_three(object one, object three)
        {
            bool result = false;

            foreach (Label one_X in (Label[])three)
            {
                if ((Label)one == one_X)
                {
                    result = true;
                    break;
                }
            }


            return result;
        }

        private void Button7_MouseEnter(object sender, EventArgs e)
        {
            button7.Image = images_Res.销毁点亮;
        }

        private void Button7_MouseLeave(object sender, EventArgs e)
        {
            button7.Image = images_Res.销毁默认;
        }

        private void Button1_MouseEnter(object sender, EventArgs e)
        {
            button1.Image = images_Res.确认设置_点燃;
        }

        private void Button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Image = images_Res.确认设置_默认;
        }





        #endregion

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, panel1.ClientRectangle,
                Color.DimGray, 1, ButtonBorderStyle.Solid, //左
　　　　　      Color.DimGray, 1, ButtonBorderStyle.Solid, //上
                Color.DimGray, 1, ButtonBorderStyle.Solid, //右
　　　　　      Color.DimGray, 1, ButtonBorderStyle.Solid);//底边
        }
    }
}
