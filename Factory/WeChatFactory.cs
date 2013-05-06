using System;
using System.Collections.Generic;
using System.Web;
using Entity;

namespace Factory
{
    public abstract class WeChatFactory
    {
        public abstract string Entrance(User user, WeiXinData weixindata, ResponseMsg rm);
    }
}