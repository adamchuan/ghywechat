using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Common;
namespace GhyWeChat.ghyadmin
{
    public partial class AddTextResponse : BaseWeb
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string AddText(string key,string text,string msgType)
        {
            if (admin == null)
            {
                return "7";
            }
            AccessHelper ah = new AccessHelper(constr, 1);
            string Text=RegularExpreesions.MyEncodeInputString(text),
                Key=RegularExpreesions.MyEncodeInputString(key);
            if (key.Trim() != "")
            {
                if (text.Trim() != "")
                {
                    DbOpeater dbopeater = new DbOpeater();
                    if (dbopeater.CheckKeyExist(key))
                    {  
                        string sql = "insert into [ResponseText]([Text],[State],[CreateTime])values('" + Text + "',1,'"+DateTime.Now.ToString()+"')";

                        if (ah.ExecuteNonQuery(sql) == 1)
                        {
                              
                            try
                            {
                            sql = "select top 1 [TextID] from [ResponseText] where [Text]='" + Text+"' order by TextID desc";

                            string MsgID = ah.ExecuteScalar(sql).ToString();

                            sql = "insert into [Response]([Creator],[CreateTime],[Key],[State],[MsgType],[MsgID])values('" + admin.AdminID + "','" + System.DateTime.Now.ToString() + "','" + Key + "','1',"+msgType+","+MsgID+")";

                            ah.ExecuteNonQuery(sql);

                            return "1";        
                            }
                            catch(Exception e)
                            {
                             
                                return "2"; //添加素材时出错

                            }
                        }
                    
                        else
                            return "6";//添加关键字时出错
                    
                    }
                    else
                        return "3"; //关键字存在
                }
                else
                    return "4";//文本为空
            }
            else
                return "5";//关键字为空
                     
        }
    }
}