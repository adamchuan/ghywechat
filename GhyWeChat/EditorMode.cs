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
        private int creator;

        public int Creator
        {
            get { return creator; }
            set { creator = value; }
        }

        public int MsgType
        {
            get { return msgType; }
            set { msgType = value; }
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

        public override string Entrance(User user, WeiXinData weixindata, ResponseMsg rm)
        {
            string reply = "";
            string introduce = "HI~欢迎关注光华园网站官方微信平台！我是管理员香菇。\r\n发送关键字\r\n图书馆\r\n 美食\r\n 小黄鸡\r\n 照片打分\r\n 空教室 自习室 自习\r\n 可以收到相应内容哦   \r\n更多功能开发中，敬请期待哦~（点击ghy.cn，下载你的大学生活）";
   
            string constr=System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            if(weixindata.MsgType.Equals("text"))
            {
                string key = RegularExpreesions.MyEncodeInputString(weixindata.Content);
                AccessHelper ah = new AccessHelper( constr,1);
                string sql="select * from Response where Key='"+key+"' and state=1";
                DataTable ResponseTable=ah.Reader(sql);

                if (ResponseTable.Rows.Count > 0)
                {
                    Response response = new Response();
                    response.Creator = Int32.Parse(ResponseTable.Rows[0][0].ToString());
                    response.MsgType = Int32.Parse(ResponseTable.Rows[0][1].ToString());
                    response.ResponseID = Int32.Parse(ResponseTable.Rows[0][2].ToString());
                    response.Key = ResponseTable.Rows[0][3].ToString();
                    response.Createtime = Convert.ToDateTime(ResponseTable.Rows[0][4]);

                    switch (response.MsgType)
                    {
                        case 1:
                            sql = "select [Text] from [ResponseText] where [State]=1 and [ResponseID]=" + response.ResponseID;
                            try
                            {
                                reply = rm.ResponseText(ah.ExecuteScalar(sql).ToString());
                            }
                            catch
                            {
                                reply = rm.ResponseText("内部错误");
                            }
                            break;
                        default:
                            reply = rm.ResponseText(introduce);
                            break;
                    }
                }
                else
                    reply = rm.ResponseText(introduce);
                
                
            }
            return reply;
        }
    }
}