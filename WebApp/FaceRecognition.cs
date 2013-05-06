using System;
using System.Collections.Generic;
using System.Text;
using Entity;
using Common;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using Factory;
namespace WebApp
{
    public class FaceRecognition:WeChatFactory
    {
        public override string Entrance(User user, WeiXinData weixindata, ResponseMsg rm)
        {
            string reply = "";
            string introduce = "欢迎进入私密功能 香菇打分 \r\n请发送你的玉照吧\r\n如果一分钟后还未回复消息 那么上传失败了\r\n发送#回到主页面";
            if (user.State2[0].Equals("2")) //
            {
                user.State2[0] = "3";//
                user.State[0] = "照片打分"; //给状态1赋值
                user.Value[0] = "";
                reply = introduce;
            }
            else if (user.State[0].Equals("2") && user.State2[0].Equals("3"))
            {
                if (weixindata.MsgType.Equals("image"))
                {
                    string msg = weixindata.Content;

                    string url = "http://api2.sinaapp.com/recognize/picture/?appkey=0020120430&appsecert=fa6095e123cd28fd&reqtype=text&keyword=" + weixindata.Picurl;

                    Used use = new Used();
                    string a = "";
                    reply = use.Get(url, "", Encoding.UTF8, ref a);

                    string[] message = reply.Split('"');
                    char[] fenge = @"\n".ToCharArray();
                    string[] temp = message[message.Length - 2].Split(fenge);

                    reply = "";
                    for (int i = 0; i < temp.Length; i++)
                    {
                        reply += temp[i] + "\r\n";
                    }
                    if (reply == "")
                        reply = "你的照片无法被识别，请换张照片吧~";
                }
                else
                {
                    if (weixindata.Content.Equals("#"))
                        user.State2[0] = "99";
                    else
                        reply = "请发送图片";
                }
            }
            return rm.ResponseText(reply);
        }
    }
}
