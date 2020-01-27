using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace SauYoo
{
    class Http_Class
    {
        Auto_Class Auto_class = new Auto_Class();
        string Post_Info;
        Uri User_API_adress = new Uri(Common.API_adress + @"/user_api.php");
        Uri services_adress = new Uri(Common.Web_adress + @"/clientarea.php?action=services");
        Uri productdetails_adress = new Uri(Common.Web_adress + @"/clientarea.php?action=productdetails&id=");
        Uri Servers_Info_adress = new Uri(Common.API_adress + @"/server_list.php");
        Uri IP_check_adress = new Uri(Common.API_adress + @"/index.php");
        Uri Update_adress = new Uri(Common.API_adress + @"/update.php");
        string JsonStr = "";
        string UserName;


        public async void Check_version()
        {
            HttpClient WebClient = new HttpClient();
            try
            {
                WebClient.Timeout = TimeSpan.FromSeconds(7);
                string JsonStr = await WebClient.GetStringAsync("http://127.0.0.1/update.php");
                JObject JsonObj = JObject.Parse(JsonStr);
                int New_Version = (int)JsonObj["version"];
                int dead_Version = (int)JsonObj["dead_version"];
                if (Common.Version < New_Version)
                {
                    MessageBox.Show("有新版本等待更新");
                    Process Update_Process = new Process();
                    String Download_Link = "Download_Link:" + JsonObj["download_link"];
                    Update_Process.StartInfo = new ProcessStartInfo(Common.Running_Path + @"\Update.exe",Download_Link);
                    Application.Exit();
                    Update_Process.Start();
                }
            }
            catch
            {

            }

        }


        /*public async Task<bool> Login(string User, string Pass)
        {
            HttpClientHandler webRequestHandler = new HttpClientHandler();
            //禁止Http对象重定义跳转
            webRequestHandler.AllowAutoRedirect = false;
            //实例化HttpClient类
            HttpClient WebClient = new HttpClient(webRequestHandler);
            WebClient.Timeout = TimeSpan.FromSeconds(12);
            WebClient.BaseAddress = Post_adress;
            //取随机40长度Token字符串
            Post_Token = Auto_class.random_String(40);
            //发送数据包
            Post_Info = "token=" + Post_Token + "&username=" + User + "&password=" + Pass;
            StringContent Post_Content = new StringContent(Post_Info, Encoding.UTF8, "application/x-www-form-urlencoded");
            try
            {
                HttpResponseMessage response = await WebClient.PostAsync("/dologin.php", Post_Content);

                //取Cookie
                Common.Http_Cookies = response.Headers.GetValues("Set-Cookie").ToArray().ElementAt(0).Split(';').ElementAt(0);
                HtmlText = await WebClient.GetStringAsync(services_adress);
            }
            catch (Exception)
            {
                //访问主网站异常
                Common.Error_Info = "服务器通讯失败，请稍后重试";
                return false;
            }
            //取用户名
            string leftStr = "<a href=\"javascript:void(0)\" class=\"username\">";
            string rightStr = "</a></h5>";
            UserName = Auto_class.Search_string(HtmlText, leftStr, rightStr);
            Common.User_Name = UserName;
            if (UserName != "")
            {
                //取用户到期时长
                leftStr = "<span class=\"hidden\">";
                rightStr = "</span>";
                string ReNew_time = Auto_class.Search_string_Max_time(HtmlText, leftStr, rightStr);
                if (ReNew_time != "")
                {
                    try
                    {
                        Common.ReNew_time = Convert.ToDateTime(ReNew_time);
                        //取产品ID
                        leftStr = "'clientarea.php?action=productdetails&amp;id=";
                        rightStr = "', false)";
                        string[] Producte_Ids = Auto_class.Search_string_All(HtmlText, leftStr, rightStr);
                        foreach (string id in Producte_Ids)
                        {
                            HtmlText = await WebClient.GetStringAsync(productdetails_adress + id);
                            leftStr = "下次付款日期 : ";
                            rightStr = " 账单周期(月付) ";
                            DateTime ReNew_Times = Auto_class.time_Reset(Auto_class.Search_string(HtmlText, leftStr, rightStr));
                            //产品时间相匹配,返回链接信息
                            if (ReNew_Times == Common.ReNew_time)
                            {
                                rightStr = "</td>";
                                //由于右标签基本不变,所以无需重复
                                leftStr = "id=\"server_method\">";
                                Common.method = Auto_class.Search_string(HtmlText, leftStr, rightStr);
                                leftStr = "id=\"server_port\">";
                                Common.port = Auto_class.Search_string(HtmlText, leftStr, rightStr);
                                leftStr = "id=\"server_pass\">";
                                Common.passwd = Auto_class.Search_string(HtmlText, leftStr, rightStr);
                                break;
                            }
                        }
                    }
                    catch
                    {
                        Common.method = "";
                        Common.port = "";
                        Common.passwd = "";
                    }
                }
                else
                {
                    Common.ReNew_time = Convert.ToDateTime("1970/01/01");
                }

                //取云节点信息
                try
                {
                    HttpClient APIClient = new HttpClient();
                    HtmlText = await APIClient.GetStringAsync(Servers_Info_adress);
                    rightStr = "</td>";
                    //如上右标签不变,无需多次重复;
                    leftStr = "<td class=\"ID\">";
                    string[] Servers_ID = Auto_class.Search_string_All(HtmlText, leftStr, rightStr);
                    leftStr = "<td class=\"Region\">";
                    string[] Servers_Region = Auto_class.Search_string_All(HtmlText, leftStr, rightStr);
                    leftStr = "<td class=\"Servers_IP\">";
                    string[] Servers_IP = Auto_class.Search_string_All(HtmlText, leftStr, rightStr);
                    leftStr = "<td class=\"State\">";
                    string[] Servers_State = Auto_class.Search_string_All(HtmlText, leftStr, rightStr);
                    Common.Servers_Info = new string[][] { Servers_ID, Servers_Region, Servers_IP, Servers_State };
                }
                catch
                {
                    //云节点信息获取错误
                    Common.Error_Info = "云节点信息获取失败,请稍后重试";
                    return false;
                }
                return true;
            }
            else
            {
                Common.Error_Info = "账号或密码错误,请检查后重试";
                return false;
            }
            //密码错误


        }*/

        public async Task<bool> Login(string User, string Pass)
        {
            HttpClientHandler webRequestHandler = new HttpClientHandler();
            //禁止Http对象重定义跳转
            webRequestHandler.AllowAutoRedirect = false;
            //实例化HttpClient类
            HttpClient WebClient = new HttpClient(webRequestHandler);
            WebClient.Timeout = TimeSpan.FromSeconds(12);
            //发送数据包
            Post_Info = "&username=" + User + "&password=" + Pass;
            String GetAdress = string.Format("{0}/user_api.php?user={1}&pass={2}","http://127.0.0.1", User, Pass);
            try
            {
                JsonStr = await WebClient.GetStringAsync(GetAdress);
            }
            catch (Exception)
            {
                //访问主网站异常
                Common.Error_Info = "服务器通讯失败，请稍后重试";
                return false;
            }
            //Json反序列化
            JObject JsonObj = JObject.Parse(JsonStr);

            //判断状态
            
            if (JsonObj["state"].ToString() != "success")
            {
                Common.Error_Info = "账号或密码错误,请检查后重试";
                return false;
            }
            else {
                UserName = Common.User_Name = JsonObj["name"].ToString();
                if (UserName != "")
                {
                    //取用户到期时长
                    TimeSpan ReNew_time = TimeSpan.FromSeconds((Double)JsonObj["regdate"]);
                    if (ReNew_time.TotalDays >0)
                    {
                        try
                        {
                            Common.ReNew_time = ReNew_time.TotalDays;
                            Common.method = JsonObj["sock_info"]["method"].ToString();
                            Common.passwd = JsonObj["sock_info"]["passwd"].ToString();
                            Common.port = JsonObj["sock_info"]["port"].ToString();
                        }
                        catch
                        {
                            Common.method = "";
                            Common.port = "";
                            Common.passwd = "";
                        }
                    }
                    else
                    {
                        Common.ReNew_time = 0;
                    }

                    //取云节点信息
                    //try
                   // {
                        JToken Servers_Info = JsonObj["servers_info"];
                        int Servers_Count = Servers_Info.Count();
                        string[] Servers_ID = new String[5], Servers_Region = new String[5], Servers_IP = new String[5], Servers_State = new String[5];

                        //权限控制
                        int i = 0;
                        foreach (JToken Server_Info in Servers_Info)
                        {
                            if (Common.ReNew_time >= 0 && Server_Info["Servers_IP"] != null)
                            {
                                Servers_ID[i] = Server_Info["ID"].ToString();
                                Servers_Region[i] = Server_Info["Region"].ToString();
                                Servers_IP[i] = Server_Info["Servers_IP"].ToString();
                                Servers_State[i] = Server_Info["State"].ToString();
                            }
                            else {
                                Servers_ID[i] = Server_Info["ID"].ToString();
                                Servers_Region[i] = Server_Info["Region"].ToString();
                                Servers_IP[i] = "127.0.0.1";
                                Servers_State[i] = Server_Info["State"].ToString();
                            }
                            i++;
                        }

                    Console.WriteLine(Servers_Region[1]);
                    Common.Servers_Info = new string[][] { Servers_ID, Servers_Region, Servers_IP, Servers_State };
           
                    // }
                    /* catch
                     {
                         //云节点信息获取错误
                         Common.Error_Info = "云节点信息获取失败,请稍后重试";
                         return false;
                     }*/
                    return true;
                }
                else
                {
                    Common.Error_Info = "账号或密码错误,请检查后重试";
                    return false;
                }
                //密码错误


            }


        }
        //实例化HttpClient类
        public async Task<bool> get_Link_state()
        {
            HttpClient API_WebClient = new HttpClient();
            API_WebClient.Timeout = TimeSpan.FromSeconds(7);
            try
            {
                //API_WebClient.BaseAddress(Common.API_adress);
                string HtmlText = await API_WebClient.GetStringAsync(IP_check_adress);
                if (HtmlText == "success")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
