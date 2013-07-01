using System;
using System.Collections.Generic;
using System.Web;
using Factory;
using Entity;
using Common;
using System.Data;
namespace GhyWeChat
{
 
    public class Response
    {
        private int responseID;

        public int ResponseID
        {
            get { return responseID; }
            set { responseID = value; }
        }
        private string key;
        private DateTime createtime;
        private int msgType;

        public int MsgType
        {
            get { return msgType; }
            set { msgType = value; }
        }
        private int creator;
        private int msgID;
     
        public int MsgID
        {
            get { return msgID; }
            set { msgID = value; }
        }
        public int Creator
        {
            get { return creator; }
            set { creator = value; }
        }

        public DateTime Createtime
        {
            get { return createtime; }
            set { createtime = value; }
        }

        public string Key
        {
            get { return key; }
            set { key = value; }
        }
    }
    public class EditorMode:WeChatFactory
    {
        public string EventResponse(User user,WeiXinData weixindata,ResponseMsg rm)
        {
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            AccessHelper ah = new AccessHelper(constr, 1);
            string reply="";
            switch (weixindata.Weixinevent)
            {
                case "subscribe":
                    reply = GetIntroOrAutoReply(1, rm);
                    break;
                case "unsubscribe":
                    Used.WriteLog(weixindata.FromUserName + "取消关注");
                    break;
                default:
                    break;
            }
            return reply;
        }
        public string GetIntroOrAutoReply(int mode,ResponseMsg rm)
        {
            string tablename = "";
            string reply = "";
            switch (mode)
            {
                case 1:
                    tablename = "Introduce";
                    break;
                case 2:
                    tablename = "AutoReply";
                    break;
                default:
                    return "";
            }
            AccessHelper ah = new AccessHelper(System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString, 1);
            string sql = string.Format("select top 1 * from [{0}]",tablename);
            DataTable introducetable = ah.Reader(sql);
            int msgtype = Int32.Parse(introducetable.Rows[0][0].ToString()),
                msgid = Int32.Parse(introducetable.Rows[0][1].ToString());
            switch (msgtype)
            {
                case 1:
                    sql = string.Format("select top 1 * from [ResponseText] where TextID={0}", msgid);
                    DataTable texttable = ah.Reader(sql);
                    reply = rm.ResponseText(texttable.Rows[0][0].ToString());
                    break;
                default:
                    break;
            }
            return reply;

        }
        public override string Entrance(User user, WeiXinData weixindata, ResponseMsg rm)
        {
            string reply = "";
  
            string constr=System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            if (!weixindata.MsgType.Equals("text"))
                reply = GetIntroOrAutoReply(2, rm);
            else
            {
                string key = RegularExpreesions.MyEncodeInputString(weixindata.Content);
                AccessHelper ah = new AccessHelper(constr, 1);
                string sql = "select * from Response where Key='" + key + "' and state=1";
                DataTable ResponseTable = ah.Reader(sql);

                if (ResponseTable.Rows.Count == 0)
                { 
                    reply = GetIntroOrAutoReply(1, rm);//没有找到关键字时 自动回复
                    Used.WriteLog(reply);
                }
                else
                {
                    Response response = new Response();
                    response.Creator = Int32.Parse(ResponseTable.Rows[0][0].ToString());
                    response.ResponseID = Int32.Parse(ResponseTable.Rows[0][1].ToString());
                    response.Key = ResponseTable.Rows[0][2].ToString();
                    response.Createtime = Convert.ToDateTime(ResponseTable.Rows[0][3]);
                    response.MsgType = Int32.Parse(ResponseTable.Rows[0][5].ToString());
                    response.MsgID = Int32.Parse(ResponseTable.Rows[0][6].ToString());

                    switch (response.MsgType)
                    {
                        case 1:
                            sql = "select [Text] from [ResponseText] where TextID=" + response.MsgID;
                            object result = ah.ExecuteScalar(sql);
                            if (result != null)
                            {
                                reply = rm.ResponseText(result.ToString());
                            }
                            else
                                Used.WriteLog("出错 没有找到相应的回复" + response.MsgID + sql);
                            break;
                        default:
                            //自动回复
                            reply = GetIntroOrAutoReply(2, rm);
                            break;
                    }
                }
            
            }
            reply = reply.Replace("<br>", "\r\n");
            return reply;
        }
    }
}