using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp;
using Entity;
namespace WeChatTest
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Library lib = new Library();
            Entity.User newuser = new User();
            List<string> state = new List<string>();//先实例化一个状态类
            state.Add("");
            List<string> value = new List<string>();//每个状态对应的值,可以为"";
            value.Add("");
            List<string> state2 = new List<string>();//实例化一个2级状态类
            state2.Add("3");  // 这里不知道一级状态是什么 所以输入2 约定所有所有 0号位置的 state="" 和 state2="2" 为用户初次进入
       
            newuser.Username = "adam";
            newuser.State = state;
            newuser.Value = value;
            newuser.State2 = state2;

            WeiXinData weixindata = new WeiXinData();
            weixindata.Content = "123";
            weixindata.MsgType = "text";
            Label1.Text = lib.Entrance(newuser,weixindata, new ResponseMsg("123","321"));
        }
    }
}