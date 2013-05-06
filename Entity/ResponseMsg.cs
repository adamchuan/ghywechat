using System;
using System.Collections.Generic;
using System.Text;
using Common;
namespace Entity
{
    public class ResponseMsg
    {
        private string useropenid;
        private string devopenid;
        private string createtime;
        /// <summary>
        /// 实例化一个回复信息
        /// </summary>
        /// <param name="UserOpenId">要回复的用户id</param>
        /// <param name="DevOpenId">微信号id</param>
        public ResponseMsg(string _UserOpenId, string _DevOpenId)
        {
            useropenid = _UserOpenId;
            devopenid = _DevOpenId;
        }
        /// <summary>
        /// 回复文本消息
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string ResponseText(string content)
        {
            string Txt = "<xml>";
            Txt = Txt + "<ToUserName><![CDATA[" +useropenid + "]]></ToUserName>";
            Txt = Txt + "<FromUserName><![CDATA[" + devopenid + "]]></FromUserName>";
            Txt = Txt + "<CreateTime>"+Used.ConvertDateTimeInt(DateTime.Now)+"</CreateTime>";
            Txt = Txt + "<MsgType><![CDATA[text]]></MsgType>";
            Txt = Txt + "<Content><![CDATA[" + content + "]]></Content>";
            Txt = Txt + "<FuncFlag>0</FuncFlag>";
            Txt = Txt + "</xml>";
            return Txt;
        }
         /// <summary>
         /// 回复音乐消息
         /// </summary>
         /// <param name="title">音乐标题</param>
         /// <param name="description">音乐描述</param>
         /// <param name="url">音乐地址</param>
         /// <param name="hqurl">在wifi环境下优先播放的地址</param>
         /// <returns></returns>
        public string ResponseMusic(string title, string description, string url, string hqurl)
        {
            string Music = "<xml>";
            Music = Music + "<ToUserName><![CDATA[" + useropenid + "]]></ToUserName>";
            Music = Music + "<FromUserName><![CDATA[" + devopenid + "]]></FromUserName>";
            Music = Music + "<CreateTime>" + Used.ConvertDateTimeInt(DateTime.Now) + "</CreateTime>";
            Music = Music + "<MsgType><![CDATA[music]]></MsgType>";
            Music = Music + " <Music>";
            Music = Music + "<Title><![CDATA[" + title + "]]></Title>";
            Music = Music + "<Description><![CDATA[" + description + "]]></Description>";
            Music = Music + "<MusicUrl><![CDATA[" + url + "]]></MusicUrl>";
            Music = Music + "<HQMusicUrl><![CDATA[" + hqurl + "]]></HQMusicUrl>";
            Music = Music + "</Music>";
            Music =Music+ "<FuncFlag>0</FuncFlag>";
            return Music;
        }
        /// <summary>
        /// 回复图文消息
        /// </summary>
        /// <param name="picnews"></param>
        /// <returns></returns>
        public string ResponseNews(List<news> picnews)
        {
            string pic = "<xml>";
            pic = pic + "<ToUserName><![CDATA[" + useropenid + "]]></ToUserName>";
            pic = pic + "<FromUserName><![CDATA[" + devopenid + "]]></FromUserName>";
            pic = pic + "<CreateTime>"+Used.ConvertDateTimeInt(DateTime.Now)+"</CreateTime>";
            pic = pic + "<MsgType><![CDATA[news]]></MsgType>";
            pic = pic + "<ArticleCount>" + picnews.Count + "</ArticleCount>";
            pic = pic + "<Articles>";
            for (int i = 0; i < picnews.Count; i++)
            {
                pic = pic + "<item>";
                pic = pic + "<Title><![CDATA[" + picnews[i].Title + "]]></Title>";
                pic = pic + "<Description><![CDATA[" + picnews[i].Description + "]]></Description>";
                pic = pic + "<PicUrl><![CDATA[" + picnews[i].PicUrl + "]]></PicUrl>";
                pic = pic + "<Url><![CDATA[" + picnews[i].Url + "]]></Url>";
                pic = pic + "</item>";
            }
            pic = pic + "</Articles>";
            pic = pic + "<FuncFlag>1</FuncFlag>";
            pic = pic + "</xml>";
            return pic;
        }
    }
}
