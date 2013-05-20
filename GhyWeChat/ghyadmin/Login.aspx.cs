using System;
using System.Web.Services;
using System.IO;
using System.Net;
using Common;
using System.Text;
using System.Configuration;
using System.Data;
namespace GhyWeChat
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
			{
				Session["AdminID"] = null;
			}	
        }
         
        [WebMethod]
        public static string Login(string username, string password)
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            AccessHelper ah = new AccessHelper(constr,1);
            string UserName =Encrypt.MD5_encrypt(RegularExpreesions.MyEncodeInputString(username)),
                PassWord = Encrypt.MD5_encrypt(RegularExpreesions.MyEncodeInputString(password));

            string sql = "select * from [User] where UserName='"+UserName+"' and Password='"+PassWord+"'";
            DataTable admintable=ah.Reader(sql);
            if (admintable.Rows.Count == 1)
            {
                GhyWeChat.ghyadmin.Entity.Admin admin = new GhyWeChat.ghyadmin.Entity.Admin();
                admin.AdminName = admintable.Rows[0][0].ToString();
                admin.AdminPwd = admintable.Rows[0][1].ToString();
                admin.AdminID = Int32.Parse(admintable.Rows[0][2].ToString());
                admin.NickName = admintable.Rows[0][3].ToString();
                System.Web.HttpContext.Current.Session.Add("Admin", admin);
                return "1";
            }
            else
            {
                return "2";
            }
           
        }
    }
}