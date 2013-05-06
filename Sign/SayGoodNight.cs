using System;
using System.Collections.Generic;
using System.Text;
using Common;
using Entity;
using System.IO;
using Factory;
namespace Sign
{
    public class SayGoodNight:WeChatFactory
    {
        public override string Entrance(User user, WeiXinData weixindata, ResponseMsg rm)
        {

            string reply = rm.ResponseMusic("晚安", "送给你的歌", "http://aliuwmp3.changba.com/userdata/userwork/57247801.mp3", "http://aliuwmp3.changba.com/userdata/userwork/57247801.mp3");
            if (weixindata.MsgType.Equals("text"))
            {
                string text = weixindata.Content;
                string filename;
                switch(text)
                {
                    case "晚安":
                        filename = System.AppDomain.CurrentDomain + "nightlog.txt";
                        break;
                    case "早安":
                        filename = System.AppDomain.CurrentDomain + "morninglog.txt";
                        break;
                    default:
                        break;
                }
            }
            return reply;
        }
    }
}
