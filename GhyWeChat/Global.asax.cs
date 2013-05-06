using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Caching;
using Entity;
using Common;
using System.Threading;
namespace GhyWeChat
{
    public class Global : System.Web.HttpApplication
    {

        public Timer a_timer;
        /// <summary>
        /// 删除会话时间过期的用户
        /// </summary>
        private void DialogueOver(Object state)
        {
            List<User> DialogueUser;
            int remove = 0;
            if (Application["User"] != null)
            {
                DialogueUser = (List<User>)Application["User"];
                foreach (User u in DialogueUser)
                {
                    if (DateTime.Now >= u.Exipre && !u.Ondialog) //并且用户不处于对话中
                    {
                        DialogueUser.Remove(u);
                        remove++;
                    }
                }
            }
        }
        protected void Application_Start(object sender, EventArgs e)
        {
            try
            {
                Application["User"] = new List<User>();
                System.Threading.AutoResetEvent autoEvent = new System.Threading.AutoResetEvent(false);
                System.Threading.TimerCallback timerDelegate = new System.Threading.TimerCallback(DialogueOver);
                a_timer = new Timer(timerDelegate, autoEvent, 0, 5 * 60 * 1000);
            }
            catch (Exception ex)
            {
                Used.WriteLog(ex.InnerException + ex.Message + ex.StackTrace);
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}