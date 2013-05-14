using System;
using System.Collections.Generic;
using System.Text;
using Entity;
using Common;
using System.Net;
using System.IO;
using System.Collections;
using System.Xml;
using Factory;
namespace WebApp
{
    public class PublicComment:WeChatFactory
    {
        string appkey = "9130022249";
        string appsecret = "86f5c821f4a04ac89dc3301616ff1845";
        string introduce = "请发送你的坐标 \r\n右下角有个+号 点进去后选择[位置]发送";
        public override string Entrance(User user, WeiXinData weixindata, ResponseMsg rm)
        {
            string reply = "";
            if (user.State2[0].Equals("2")) //
            {
                user.State2[0] = "3";//
                user.State[0] = "美食"; //给状态1赋值
                user.Value[0] = "";
                reply = rm.ResponseText(introduce);
            }
            else if (user.State2[0].Equals("3"))
            {

                Hashtable param = new Hashtable();
                param.Add("latitude", weixindata.Location_x);
                param.Add("longitude", weixindata.Location_y);
                param.Add("sort", "7");
                param.Add("limit", "5");
                param.Add("format", "xml");
                param.Add("offset_type", "1");
                param.Add("out_offset_type", "1");
                param.Add("platform", "2");
                param.Add("radius", "5000");
                //  param.Add("categories", "美食");
                string[] keyArray = new string[param.Count];
                param.Keys.CopyTo(keyArray, 0);
                Array.Sort(keyArray);

                string result = appkey;
                foreach (string key in keyArray)
                    result += (key + param[key]);

                result += appsecret;

                result = Encrypt.SHA1_encrypt(result).ToUpper(); //SHA1 加密

                string url = "http://api.dianping.com/v1/business/find_businesses?appkey=9130022249&sign=" + result + "&latitude=" + param["latitude"] + "&longitude=" + param["longitude"] + "&sort=" + param["sort"] + "&limit=" + param["limit"] + "&format=" + param["format"] + "&offset_type=" + param["offset_type"] + "&out_offset_type=" + param["out_offset_type"] + "&platform=" + param["platform"] + "&radius=" + param["radius"];// + "&categories=" + param["categories"];


                WebRequest wr = WebRequest.Create(url);
                wr.Method = "GET";
                WebResponse response = wr.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                XmlDocument xd = new XmlDocument();
                xd.LoadXml(retString);
                XmlNodeList xnl = xd.GetElementsByTagName("business");

                List<news> picnews = new List<news>();
                news a = new news();
                a.PicUrl = "http://www.ghy.swufe.edu.cn/GHYWEIXIN/file/images/dzdp.png";
                a.Title = "来自大众点评";
                picnews.Add(a);
                foreach (XmlNode xn in xnl)
                {
                    news picnew = new news();

                    picnew.Title = xn.SelectSingleNode("name").InnerText;
                    picnew.Url = xn.SelectSingleNode("business_url").InnerText;
                    picnew.PicUrl = xn.SelectSingleNode("s_photo_url").InnerText;
                    picnew.Description = xn.SelectSingleNode("address").InnerText;
                    picnews.Add(picnew);
                }
                reply = rm.ResponseNews(picnews);

            }
            return reply;

        }
    }
}

