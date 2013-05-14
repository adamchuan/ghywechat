using System;
using System.Collections.Generic;
using System.Text;
using Common;
using Entity;
using System.IO;
using Factory;
namespace Sign
{
    public class InquiryClass:WeChatFactory
    {
        string introduce = "请发送要查询的教学楼号，B C E F G I ";
        public override string Entrance(User user, WeiXinData weixindata, ResponseMsg rm)
        {
            string reply = "";
            if (user.State2[0].Equals("2")) //
            {
                user.State2[0] = "3";//
                user.State[0] = "空教室"; //给状态1赋值
                user.Value[0] = "";
                reply = rm.ResponseText(introduce);
            }
            else
            {
                reply = GetClasses(user, weixindata, rm);  
            }
            return reply;
        }
        private string GetClasses(User user, WeiXinData weixindata, ResponseMsg rm)
        {
            string reply="";
            if (weixindata.MsgType.Equals("text"))
            {
                string Build = "";
                switch (weixindata.Content.ToLower())
                {
                    case "b":
                        Build = "b";
                        break;
                    case "c":
                        Build = "c";
                        break;
                    case "i":
                        Build = "i";
                        break;
                    case "e":
                        Build = "e";
                        break;
                    case "f":
                        Build = "f";
                        break;
                    case "g":
                        Build = "g";
                        break;
                    default:
                        break;
                }
                if (!Build.Equals(""))
                {
                    string sql = "select * from [" + Build + "]";
                    string filename = "Data Source="+System.AppDomain.CurrentDomain.BaseDirectory + @"File/Class/Classes.mdb";
                    AccessHelper accessHelper = new AccessHelper(filename, 1);
                    int i = 0, j;
                    DateTime time1 = Convert.ToDateTime("8:30"),
                             time2 = Convert.ToDateTime("10:15"),
                             time3 = Convert.ToDateTime("12:00"),
                             time4 = Convert.ToDateTime("15:35"),
                             time5 = Convert.ToDateTime("17:25"),
                             time6 = Convert.ToDateTime("21:00");

                    if (DateTime.Now < time2)
                        i = 0;
                    else if (DateTime.Now >= time2 && DateTime.Now < time3)
                        i = 1;
                    else if (DateTime.Now >= time3 && DateTime.Now < time4)
                        i = 2;
                    else if (DateTime.Now >= time4 && DateTime.Now < time5)
                        i = 3;
                    else if (DateTime.Now >= time5 && DateTime.Now < time6)
                        i = 4;
                    else
                        i = 5;

                    string week = DateTime.Now.DayOfWeek.ToString();
                    switch (week)
                    {
                        case "Monday":
                            j = 0;
                            break;
                        case "Tuesday":
                            j = 1;
                            break;
                        case "Wednesday":
                            j = 2;
                            break;
                        case "Thursday":
                            j = 3;
                            break;
                        case "Friday":
                            j = 4;
                            break;
                        default:
                            j = 5;
                            break;
                    }
                    if (j == 5 || i == 5)
                    {
                        reply = DateTime.Now.Hour + ":" + DateTime.Now.Minute+" 不是上课时间，随便哪都能上自习";
                    }
                    else
                    {
                        System.Data.DataTable table = accessHelper.Reader(sql);
                        reply = Build + "座 " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + " 可用的教室有\r\n" + table.Rows[i][j].ToString();
                    }
                     
                }
            }
            return rm.ResponseText(reply);
        }
    }
}
