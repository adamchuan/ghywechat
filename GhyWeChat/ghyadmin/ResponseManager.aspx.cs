using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Common;
using System.Data;
namespace GhyWeChat.ghyadmin
{
    public partial class ResponseManage :BaseWeb
    {
     
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string EditIntroduceOrAutoReply(string msgtype, string msgid, string edittype)
        {
            if (admin == null)
                return "7";
            string tablename = "";
            switch (edittype)
            {
                case"2":
                    tablename = "Introduce";
                    break;
                case"3":
                    tablename = "AutoReply";
                    break;
                default:
                    return "6";
            }
            if (RegularExpreesions.CheckNum(msgid))
            {
                if (RegularExpreesions.CheckNum(msgtype))
                {
                    string constr = System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                    AccessHelper ah = new AccessHelper(constr, 1);
                    string sql = string.Format("update [{0}] set  [MsgID]={1},[MsgType]={2}",tablename, Int32.Parse(msgid), Int32.Parse(msgtype));
                    if (ah.ExecuteNonQuery(sql) > 0)
                    {
                        return "1";
                    }
                    else
                        return "2";
                }
                else
                    return "3";
            }
            else
                return "4";
        }

        [WebMethod]
        public static string AddKeyResponse(string msgid,string msgtype,string key)
        {
            if (admin == null)
            {
                return "7";
            }
            if(!RegularExpreesions.CheckNum(msgtype))
            {
                return "6";//msgtype错误
            }
            if(!RegularExpreesions.CheckNum(msgid))
            {
                return "5"; // msgid错误
            } 
            AccessHelper ah = new AccessHelper(constr,1);
            string sql="";
            string Key = RegularExpreesions.MyEncodeInputString(key);
            DbOpeater dbo = new DbOpeater();
            if (!dbo.CheckKeyExist(key))
            {
                return "4"; //该关键字已经存在
            }
            switch(msgtype)
            {
                case"1":              
                    if(dbo.CheckResponseExist(Int32.Parse(msgid),Int32.Parse(msgtype)))
                    return "5";//msgid 错误
                    else
                    sql = "insert into [Response]([Creator],[CreateTime],[Key],[State],[MsgType],[MsgID])values('" + admin.AdminID + "','" + System.DateTime.Now.ToString() + "','" + Key + "','1'," + msgtype + "," + msgid + ")";
                    break;
                case"2":
                    break;
                case"3":
                    break;
                default:
                    return "6"; //msgtype错误
            }

            return ah.ExecuteNonQuery(sql) == 1 ? "1" : "2";

        }
        [WebMethod]
        public static string GetTotalNum(string msgtype)
        {
            AccessHelper ah = new AccessHelper(constr, 1);
            string tablename="",sql;
            switch(msgtype)
            {
                case "1":
                    tablename="ResponseText";
                    break;
                case "2":
                    break;
                case "3":
                    break;
                default:
                    break;
            }
            sql = string.Format("select count(*) from {0} where state=1", tablename);
            return ah.ExecuteScalar(sql).ToString();
        }
        [WebMethod]
        public static string GetTextList(string pagenum,string pagesize)
        {
            if (admin == null)
                return "7";
            AccessHelper ah = new AccessHelper(constr, 1);
            if (!RegularExpreesions.CheckNum(pagenum))
                return "9";
            if (!RegularExpreesions.CheckNum(pagesize))
                return "8";
            int PageNum = Int32.Parse(pagenum),
                PageSize = Int32.Parse(pagesize);
            string sql;
            if (PageNum == 1)
                 sql = string.Format("select top {0} * from [ResponseText] order by TextID desc", PageSize);
            else
                 sql = string.Format("select  top  {0} *  from  [ResponseText] where TextID  not  in  ( select  top {1} TextID from  [ResponseText] where state=1 ) and state=1  ",PageSize,PageSize*(PageNum-1));

            DataTable texttable = ah.Reader(sql);
            string textlist;

            textlist = "[{";
            textlist += "\"textlist\": ";
            textlist += "[";
            for (int i = 0; i < texttable.Rows.Count; i++)
            {
                DataRow dr = texttable.Rows[i];
                textlist += "{";
                textlist += "\"text\": \" " + dr[0].ToString() + " \" ,";
                textlist += "\"textid\":\"" + dr[1].ToString() + "\"";
                textlist += "}";
                if (i != texttable.Rows.Count - 1)
                    textlist += ",";
            }
            textlist+=" ],";
            textlist+=" \"count\":\"" + texttable.Rows.Count + "\"";
            textlist += "}]";
            return textlist;

        }

    }
}