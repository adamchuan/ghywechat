using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Web.Services;
namespace GhyWeChat.ghyadmin
{
    public partial class MaterialManager : BaseWeb
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string AddText(string text)
        {
            if (admin != null)
            {
                string Text = RegularExpreesions.MyEncodeInputString(text);
                if (Text.Length < 600)
                {
                    string constr = System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                    AccessHelper ah = new AccessHelper(constr, 1);
                    string sql = string.Format("insert into [ResponseText]([Text],[State],[CreateTime],[Creator])values('{0}',1,'{1}',{2})", Text, DateTime.Now.ToString(),admin.AdminID);
                    if (ah.ExecuteNonQuery(sql) == 1)
                        return "1";//添加成功
                    else
                        return "2";//添加失败
                }
                else
                    return "3";//含有非法字符 已过滤添加
            }
            return "7";
        }
        
    }
}