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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            Common.form7 = this;
            Deactivate += Control_Class.Form_Deactivate;          
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            Auto_Class Auto_class = new Auto_Class();
            Auto_class.Change_Font(new Label[] { label1 });
            Auto_class.Change_Font(new Button[] { button1, button2 });
        }

        private void Button3_MouseEnter(object sender, EventArgs e)
        {
            button3.Image = images_Res.销毁点亮;
        }

        private void Button3_MouseLeave(object sender, EventArgs e)
        {
            button3.Image = images_Res.销毁默认;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Common.form3.Hide();
            this.Hide();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Application.Exit();
        }

        private void Button2_MouseEnter(object sender, EventArgs e)
        {
            this.button2.ForeColor = Color.FromArgb(0, 179, 116);
        }

        private void Button2_MouseLeave(object sender, EventArgs e)
        {
            this.button2.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private void Button1_MouseEnter(object sender, EventArgs e)
        {
            this.button1.ForeColor = Color.FromArgb(0, 179, 116);
        }

        private void Button1_MouseLeave(object sender, EventArgs e)
        {
            this.button1.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private void Form7_Deactivate(object sender, EventArgs e)
        {
            this.Hide();
        }


    }
}
