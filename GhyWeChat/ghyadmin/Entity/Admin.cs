using System;
using System.Collections.Generic;
using System.Web;

namespace GhyWeChat.ghyadmin.Entity
{
    public class Admin
    {
        private int adminID;

        public int AdminID
        {
            get { return adminID; }
            set { adminID = value; }
        }
        private string adminName;

        public string AdminName
        {
            get { return adminName; }
            set { adminName = value; }
        }
        private string adminPwd;
        
        public string AdminPwd
        {
            get { return adminPwd; }
            set { adminPwd = value; }
        }
        private string nickName;

        public string NickName
        {
            get { return nickName; }
            set { nickName = value; }
        }
        public Admin()
        {
        }
    }
}