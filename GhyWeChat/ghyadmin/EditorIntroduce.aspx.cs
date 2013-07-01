using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Common;
using System.Data;
using Entity;
namespace GhyWeChat.ghyadmin
{
    public partial class EditorIntroduce : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string GetIntroResponse()
        {
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            AccessHelper ah = new AccessHelper(constr, 1);
            string sql = "select top 1 * from [Introduce]";
            DataTable introducetable = ah.Reader(sql);
            int msgtype = Int32.Parse(introducetable.Rows[0][0].ToString()),
                msgid =Int32.Parse( introducetable.Rows[0][1].ToString());
            WeiXinData response = new WeiXinData();
            string result = "";
            switch (msgtype)
            {
                case 1:
                    sql = string.Format("select top 1 * from [ResponseText] where TextID=%i", msgid);
                    DataTable texttable=ah.Reader(sql);
                    response.Content = texttable.Rows[0][0].ToString();
                    result = string.Format("'msgtype':'1','text':'%s'", response.Content);
                    break;
                default:
                    break;
            }
            return result;
        
                
        }
    }
}