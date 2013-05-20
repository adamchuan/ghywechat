using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
namespace GhyWeChat.ghyadmin
{
    public class BaseWeb : System.Web.UI.Page
    {

        public static Entity.Admin admin;

        public static string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        protected override void OnInit(EventArgs e)
        {
            if (Session["Admin"] == null)
            {
                Response.Write("<script>alert('请重新登录');self.location='Login.aspx'</script>");
                Response.End();
            }
            else
            {
                admin =(Entity.Admin)Session["Admin"];
            }
        }
    }
}