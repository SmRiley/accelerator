using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Text;
using System.Drawing;

namespace SauYoo
{
    class Common
    {
        //全局变量
        public static string Web_adress { get { return "https://www.sauyoo.com"; } }
        public static string API_adress { get { return "https://api.sauyoo.com"; } }
        public static string Running_Path { get { return @System.Environment.CurrentDirectory; } }
        public static string Program_Path { get { return @System.Reflection.Assembly.GetExecutingAssembly().Location; } }

        public static int Version { get { return 203; } }
        private static bool Remmber_pass = false;
        public static bool Remmber_Pass
        {
            get { return Remmber_pass; }
            set { Remmber_pass = value; }
        }
        private static bool Auto_login = false;
        public static bool Auto_Login {
            get { return Auto_login; }
            set { Auto_login = value; }
        }

        public static FontFamily MyFont_Families;

        public static string Http_Cookies { get; set; }
        public static string User_Name { get; set; }
        public static Double ReNew_time { get; set; }
        public static string[][] Servers_Info { get; set; }
        public static string Servers_Region { get; set; }
        public static string Error_Info { get; set; }

        //窗口变量
        public static bool Is_Linked;
        public static Form1 form1 { get; set; }
        public static Form3 form3 { get; set; }
        public static Form4 form4 { get; set; }
        public static Form5 form5 { get; set; }
        public static Form6 form6 { get; set; }
        public static Form7 form7 { get; set; }

        private static string Host;
        public static string host { get {return Host; } set {Host = value; } }
        private static string Port;
        public static string port { get {return Port; } set {Port = value; } }
        private static string Passwd ;
        public static string passwd { get {return Passwd; } set {Passwd = value; } }
        private static string Xtudp = "20";
        public static string xtudp { get { return Xtudp; } set { Xtudp = value; } }
        private static string Method;
        public static string method { get {return Method; } set { Method = value; } }
        private static string Dns = "1.2.4.8";
        public static string dns { get { return Dns; } }
    }
}
