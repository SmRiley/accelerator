using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SauYoo
{
    class Control_Class
    {

        public  Control_Class() {
            Active_Form_event += When_Need_Active;
        }

        static bool is_wait_actived = false;
         bool Is_wait_actived {
            get { return is_wait_actived; }
            set {
                if (is_wait_actived != value && !value) {
                    On_Active_Form_Change();
                }
                is_wait_actived = value;
            }
        }

        private delegate void Active_Form_delegate();
        private event Active_Form_delegate Active_Form_event;

        private void On_Active_Form_Change() {
            if (Active_Form_event != null) {
                Active_Form_event();
            }
        }


        /// <summary>
        /// 激活子窗口
        /// </summary>
        private void When_Need_Active() {
                if (Common.form5.Visible)
                {
                    Common.form5.Activate();
                }
                else if(Common.form6.Visible)
                { 
                    Common.form6.Activate();
                }
        }

        private bool Is_Active_Form(Form form) {
            try
            {
                if (Auto_Class.GetForegroundWindow() != form.Handle)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch {
                return false;
            }
        }

        public static void Form_Deactivate(object sender, EventArgs e) {
            Control_Class Control_class = new Control_Class();
            bool Is_Form3_Handle = Control_class.Is_Active_Form(Common.form3);
            bool Is_Form4_Handle = Control_class.Is_Active_Form(Common.form4);
            bool Is_Form5_Handle = Control_class.Is_Active_Form(Common.form5);
            bool Is_Form6_Handle = Control_class.Is_Active_Form(Common.form6);
            bool Is_Form7_Handle = Control_class.Is_Active_Form(Common.form7);
            if (Is_Form3_Handle && Is_Form4_Handle && Is_Form5_Handle && Is_Form6_Handle && Is_Form7_Handle)
            {
                Control_class.Is_wait_actived = true;
            }
            
        }

        public static void Form_Activated(object sender, EventArgs e) {
            new Control_Class().Is_wait_actived = false;
        }
    }
}
