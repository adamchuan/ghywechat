using System;
using System.Collections.Generic;
using System.Web;
using Common;
namespace GhyWeChat.ghyadmin
{
    public class DbOpeater
    {
        string constr = "Data Source=" + System.AppDomain.CurrentDomain.BaseDirectory + @"ghyadmin/database/ghywc.mdb";

        /// <summary>
        /// 检查msgid的回复是否存在  true为不存在 false为存在
        /// </summary>
        /// <param name="textid"></param>
        /// <returns></returns>
        public bool CheckResponseExist(int msgid,int msgtype)
        {
            string sql = "";
            switch (msgtype)
            {
                case 1:
                    sql = "select [TextID] from [ResponseText] where TextID=" + msgid;
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;
            }
            AccessHelper ah = new AccessHelper(constr, 1);
            return ah.ExecuteNonQuery(sql) == 0 ? true : false;
        }
        /// <summary>
        /// 检查该关键字的回复是否存在 true为不存在 false为存在
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        public bool CheckKeyExist(string key)
        {
            AccessHelper ah = new AccessHelper(constr, 1);
            string sql="update [Response] set [Key]='"+key+"' where [Key]='"+key+"'";
            return ah.ExecuteNonQuery(sql) == 0 ? true : false;
        }
    }
}