using System;
using System.Web.Services;
using System.IO;
using System.Net;
using Common;
using System.Text;
namespace GhyWeChat
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
 
        [WebMethod]
        public static string Login(string username, string password)
        {
            return "成功";
        }
    }
}