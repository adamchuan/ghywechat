using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text;
using System.Xml;
using Common;
using Entity;
using System.Reflection;
using Factory;
namespace GhyWeChat
{
    /// <summary>
    /// Handler 的摘要说明
    /// </summary>
    public class Handler : IHttpHandler
    {
        const string Token = "ghy";		//你的token
        List<User> user;
     
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.HttpMethod.ToLower() == "post")
            {   string postStr = "";
                if (context.Application["User"] == null)
                {
                    user = new List<User>();
                    context.Application["User"] = user;
                }
                else
                {
                    user = (List<User>)context.Application["User"];
                }
                Used.WriteLog(user.Count.ToString() + " " + System.DateTime.Now);
                Stream s = System.Web.HttpContext.Current.Request.InputStream;
                byte[] b = new byte[s.Length];
                s.Read(b, 0, (int)s.Length);
                postStr = Encoding.UTF8.GetString(b);
                if (!string.IsNullOrEmpty(postStr))
                {
                    string reply=ResponseMsg(postStr);
                    context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                    context.Response.Charset = "utf-8";
                    context.Response.Write(reply);
                    context.Response.End();
                }
            }
            else
            {
                Valid(context);
            }
        }


        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// * 将token、timestamp、nonce三个参数进行字典序排序
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        /// <returns></returns>
        private bool CheckSignature(HttpContext context)
        {
            string signature =context.Request.QueryString["signature"].ToString();
            string timestamp = context.Request.QueryString["timestamp"].ToString();
            string nonce = context.Request.QueryString["nonce"].ToString();
            string[] ArrTmp = { Token, timestamp, nonce };
            Array.Sort(ArrTmp);     //字典排序
            string tmpStr = string.Join("", ArrTmp);

            tmpStr = Encrypt.SHA1_encrypt(tmpStr).ToLower();
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Valid(HttpContext context)
        {
            string echoStr = context.Request.QueryString["echoStr"].ToString();
            if (CheckSignature(context))
            {
                if (!string.IsNullOrEmpty(echoStr))
                {
                    context.Response.Write(echoStr);
                    context.Response.End();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>


        /// <summary>
        /// 返回信息结果(微信信息返回)
        /// </summary>
        /// <param name="weixinXML"></param>
        private string ResponseMsg(string weixinXML)
        {
            //发送消息的部分:你的代码写在这里          

            string reply = "";//最终发送用户的信息
            try
            {
                WeiXinData receivemsg= WeiXinData.ReadRecevie(weixinXML);
                ResponseMsg rm = new ResponseMsg(receivemsg.FromUserName, receivemsg.ToUserName);
                User currentuser = new User();//正在和服务器对话的用户
                ///开始判断用户的状态
                bool Exist = false; //用户是否存在
                for (int i = 0; i < user.Count; i++)
                {
                    if (user[i].Username.Equals(receivemsg.FromUserName))
                    {
                        currentuser = user[i];  //找到那个用户
                        Exist = true;
                    }
                }
                if (!Exist)//如果用户不存在 则创建用户加入
                {
                    List<string> state = new List<string>();//先实例化一个状态类
                    state.Add("");
                    List<string> value = new List<string>();//每个状态对应的值,可以为"";
                    value.Add("");
                    List<string> state2 = new List<string>();//实例化一个2级状态类
                    state2.Add("2");  // 这里不知道一级状态是什么 所以输入2 约定所有所有 0号位置的 state="" 和 state2="2" 为用户初次进入
                    User newuser = new User();
                    newuser.Username = receivemsg.FromUserName;
                    newuser.State = state;
                    newuser.Value = value;
                    newuser.State2 = state2;
                    user.Add(newuser);  //将用户添加进去;
                    currentuser = newuser;//正在对话的用户为新用户
                }
                currentuser.Exipre = System.DateTime.Now.AddMinutes(10);//设置对话过期时间
                currentuser.Ondialog = true;
                EditorMode em=new EditorMode();
                switch (receivemsg.MsgType)
                {
                    case "event":
                        
                        reply = em.EventResponse(currentuser, receivemsg, rm);
                        break;

                    default:
                    
                        string text = "";
                        if (receivemsg.MsgType.Equals("text"))
                            text = receivemsg.Content;
                        if (text.Equals("#"))
                        {
                            reply=em.GetIntroOrAutoReply(1, rm);
                        }
                        else
                        {
                            string firststate = "";//记录用户的首状态

                            if (currentuser.State2[0].Equals("2")) // 这个"2"表示 首状态还未赋值
                                firststate = text; //若是新用户发来的就已该文本作为首状态去判断，但是这里没有存入缓存里面
                            else
                                firststate = currentuser.State[0];


                            XmlDocument xml = new XmlDocument();
                            xml.Load(System.AppDomain.CurrentDomain.BaseDirectory + @"/Config.xml");
                            XmlNodeList xnl = xml.GetElementsByTagName("class");
                            bool isFind = false;
                            WeChatFactory factory;
                            foreach (XmlNode xn in xnl)
                            {
                                string[] keys = xn.Attributes["key"].Value.Split('|');
                         
                                foreach (string key in keys)
                                {
                                    if (firststate == key)
                                    {
                                        string assmblyname = xn.SelectSingleNode("assmblyname").InnerText;
                                        string strfactoryName = xn.SelectSingleNode("strfactoryName").InnerText;
                                        factory = (WeChatFactory)Assembly.Load(assmblyname).CreateInstance(assmblyname + "." + strfactoryName);
                                        reply = factory.Entrance(currentuser, receivemsg, rm);
                                        isFind = true;
                                    }
                                }
                                if (isFind)
                                    break;
                            }
                            if (!isFind)
                            {
                                reply =em.Entrance(currentuser, receivemsg, rm);
                            }
                            //根据用户的首状态进入不同的类开始处理                    
                            //switch (firststate)
                            //{
                            //    case "1":
                            //        SwufeDream_WeiXin sdlogin = new SwufeDream_WeiXin();
                            //        reply = sdlogin.SwufeDreamEntrance(currentuser, receivemsg, rm);
                            //        break;
                            //    case "照片打分":
                            //        Face_Recognition fr = new Face_Recognition();
                            //        reply = fr.Entrance(currentuser, receivemsg, rm);
                            //        break;
                            //    case "小黄鸡":
                            //        Robot rb = Robot.Instance();
                            //        reply = rb.RobotEntrance(currentuser, receivemsg, rm);
                            //        break;
                            //    //case "空教室":
                            //    //    InquiryClass ic = new InquiryClass();
                            //    //    reply = ic.Entrance(currentuser, receivemsg, rm);
                            //    //    break;
                            //    case "美食":
                            //        PublicComment pc = new PublicComment();
                            //        reply = pc.Entrance(currentuser, receivemsg, rm);
                            //        break;
                            //    //case "晚安":

                            //    //case "早安":
                            //    //    Sign.SayGoodNight saygoodnight = new Sign.SayGoodNight();
                            //    //    reply = saygoodnight.Entrance(currentuser, receivemsg, rm);
                            //    //    Used.WriteLog(reply);
                            //    //    break;
                            //    default:
                            //        reply = rm.ResponseText(introduce); break;
                            //}
                            currentuser.Ondialog = false; //标致用户对话结束
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                Used.WriteLog(e.Source + e.StackTrace + e.Message);
            }
            return reply;
           
        }





        /// <summary>
        /// 是否能复用
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}