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
    public partial class Form4 : Form
    {
        Auto_Class Auto_class = new Auto_Class();
        public Form4()
        {
            InitializeComponent();
            Common.form4 = this;
            Deactivate += Control_Class.Form_Deactivate;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Auto_class.Change_Font(new Button[] { button1, button2, button3 });
        }

        public void get_location(Form M_Form) {
            this.Show();
            this.Left = M_Form.Left + 245;
            this.Top = M_Form.Top + 40;          
        }

        private void Form4_Deactivate(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (Common.form3.Button_enabled)
            {
                this.Hide();
                Common.form3.Close();
                Auto_class.Show_Dynamic(Common.form1);
                Common.form1.Show();
                Common.form1.Activate();
            }
            else {
                IniFiles W_Config = new IniFiles();
                W_Config.DeleteIniFile();
                Application.Exit();
                Process.Start(Common.Program_Path);
            }
            
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (Common.form3.Button_enabled)
            {
                this.Hide();
                Common.form3.Close();
                Common.form1.button1_Click(null, null);
                Common.form5.Form5_Load(null, null);
            }
            else {
                Application.Exit();
                Process.Start(Common.Program_Path);
            }
            
        }


        private void Button3_Click(object sender, EventArgs e)
        {
            DialogResult MessageBox_result = MessageBox.Show("确定完全要退出软件吗？", "提示", MessageBoxButtons.YesNo);
            if (MessageBox_result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
