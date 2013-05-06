using System;
using System.Collections.Generic;
using System.Text;
using Entity;
using System.Net;
using System.IO;
using Common;
using Newtonsoft.Json;
using Factory;
namespace WebApp
{

    /// <summary>
    /// 这个功能其实比较简单 数据字典说明
    /// 
    /// 状态0    state    state2   含义   
    ///                   2        进入小黄鸡
    ///           3       3        等待发送信息和小黄鸡对话
    /// </summary>

    public class Robot:WeChatFactory
    {
        string introduce = "你有什么要对小黄鸡说的呢？ 如果不想跟小黄鸡说话了 发送#回到主页面";

        public override string Entrance(User user, WeiXinData recivedata, ResponseMsg rm)
        {
            string reply = "";

            if (user.State2[0].Equals("2")) //进入小黄鸡
            {
                user.State2[0] = "3";//
                user.State[0] = "小黄鸡"; //给状态1赋值
                user.Value[0] = "";
                reply = introduce;
            }
            else if (user.State2[0].Equals("3"))
            {
                if (recivedata.MsgType.Equals("text"))
                {
                    string msg = recivedata.Content;

                    string pageHtml = SendDataByGET("http://www.simsimi.com/func/req?msg=" + msg + "！&lc=ch");
                    pageHtml = "[" + pageHtml + "]";
                    List<SiriResult> _List = JsonConvert.DeserializeObject<List<SiriResult>>(pageHtml);
                    foreach (SiriResult c in _List)
                    {
                        reply = c.Response;
                    }
                    if (CheckSensitiveString(reply))
                    {
                        reply = "伦家听不懂这句话啦~ 请换句说";
                    }
                }
                else
                    reply = "小黄鸡只支持发送文本哦";
            }
            return rm.ResponseText(reply);
        }
        public bool CheckSensitiveString(string s)
        {
            string[] SensitiveString = { "约炮", "微信", "泡妞" };
            bool isexist = false;
            for (int i = 0; i < SensitiveString.Length; i++)
            {
                if (s.Trim().IndexOf(SensitiveString[i]) != -1)
                    isexist = true;
            }
            return isexist;
        }
        public string ReturnStr(string msg)
        {
            string pageHtml = SendDataByGET("http://www.simsimi.com/func/req?msg=" + msg + "！&lc=ch");
            pageHtml = "[" + pageHtml + "]";
            return pageHtml;
        }
        #region 同步通过GET方式发送数据
        /// <summary>
        /// 通过GET方式发送数据
        /// </summary>
        /// <param name="Url">url</param>
        /// <param name="postDataStr">GET数据</param>
        /// <param name="cookie">GET容器</param>
        /// <returns></returns>
        private string SendDataByGET(string Url)
        {
            string host = "http://www.simsimi.com";
            WebRequest webRequest = WebRequest.Create(Url);
            HttpWebRequest request = webRequest as HttpWebRequest;

            //设置cookie
            CookieContainer cc = new CookieContainer();
            cc.Add(new Uri(host), new Cookie("JSESSIONID", "FC50E1B413D61FF258ED76121D15DBC8"));
            cc.Add(new Uri(host), new Cookie("__utma", "119922954.1431887846.1365044080.1365044080.1365044080.1"));
            cc.Add(new Uri(host), new Cookie("__utmb", "119922954.3.9.1365044144105"));
            cc.Add(new Uri(host), new Cookie("__utmc", "119922954"));
            cc.Add(new Uri(host), new Cookie("__utmz", "119922954.1365044080.1.1.utmcsr=tieba.baidu.com|utmccn=(referral)|utmcmd=referral|utmcct=/p/2166507443"));
            cc.Add(new Uri(host), new Cookie("sagree", "true"));
            request.CookieContainer = cc;

            request.Method = "GET";
            request.ContentType = "application/json; charset=utf-8";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.2; rv:20.0) Gecko/20100101 Firefox/20.0";
            request.Referer = "	http://www.simsimi.com/talk.htm";


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        #endregion

    }


    public class SiriResult
    {
        private string response;

        public string Response
        {
            get { return response; }
            set { response = value; }
        }
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string result;

        public string Result
        {
            get { return result; }
            set { result = value; }
        }
        private string msg;

        public string Msg
        {
            get { return msg; }
            set { msg = value; }
        }
    }
}