using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Common;
namespace Entity
{
    public class WeiXinData
    {

        string toUserName;
        string fromUserName;   
        string creatTime;
        string msgType;
        string content;
        string msgid;
        string picurl;
        string location_x;
        string location_y;
        string scale;
        string label;
        string title;
        string description;
        string weixinevent;
        string weixineventkey;

        public string Weixineventkey
        {
            get { return weixineventkey; }
            set { weixineventkey = value; }
        }
        public string Weixinevent
        {
            get { return weixinevent; }
            set { weixinevent = value; }
        }
        /// <summary>
        /// 发送者的id
        /// </summary>
        public string FromUserName
        {
            get { return fromUserName; }
            set { fromUserName = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }


        string url;

        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label
        {
            get { return label; }
            set { label = value; }
        }
        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public string Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public string Location_y
        {
            get { return location_y; }
            set { location_y = value; }
        }
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public string Location_x
        {
            get { return location_x; }
            set { location_x = value; }
        }


        public string Picurl
        {
            get { return picurl; }
            set { picurl = value; }
        }

        public string Msgid
        {
            get { return msgid; }
            set { msgid = value; }
        }

        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        public string MsgType
        {
            get { return msgType; }
            set { msgType = value; }
        }

        public string CreatTime
        {
            get { return creatTime; }
            set { creatTime = value; }
        }
        /// <summary>
        /// 开发者的id
        /// </summary>
        public string ToUserName
        {
            get { return toUserName; }
            set { toUserName = value; }
        }

        public static WeiXinData ReadRecevie(string postStr)
        {
            WeiXinData receivemsg = new WeiXinData();
            XmlDocument dom = new XmlDocument();

            dom.LoadXml(postStr);

            receivemsg.MsgType = dom.GetElementsByTagName("MsgType")[0].InnerText;
            receivemsg.FromUserName = dom.GetElementsByTagName("FromUserName")[0].InnerText;
            receivemsg.ToUserName = dom.GetElementsByTagName("ToUserName")[0].InnerText;
            receivemsg.CreatTime = Used.UnixTimeToTime(dom.GetElementsByTagName("CreateTime")[0].InnerText).ToString();

            switch (receivemsg.MsgType)
            {
                case "event":
                    receivemsg.Weixinevent = dom.GetElementsByTagName("Event")[0].InnerText;
                    receivemsg.Weixineventkey = dom.GetElementsByTagName("EventKey")[0].InnerText;
                    break;
                case "text":
                    receivemsg.Msgid = dom.GetElementsByTagName("MsgId")[0].InnerText;
                    receivemsg.Content = dom.GetElementsByTagName("Content")[0].InnerText;
                    break;
                case "image":
                    receivemsg.Msgid = dom.GetElementsByTagName("MsgId")[0].InnerText;
                    receivemsg.Picurl = dom.GetElementsByTagName("PicUrl")[0].InnerText;
                    break;
                case "location":
                    receivemsg.Location_x = dom.GetElementsByTagName("Location_X")[0].InnerText;
                    receivemsg.Location_y = dom.GetElementsByTagName("Location_Y")[0].InnerText;
                    receivemsg.Scale = dom.GetElementsByTagName("Scale")[0].InnerText;
                    receivemsg.Label = dom.GetElementsByTagName("Label")[0].InnerText;
                    break;
                default:
                    break;
            }
            return receivemsg;
        }
    }

    public class news
    {
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private string picUrl;

        public string PicUrl
        {
            get { return picUrl; }
            set { picUrl = value; }
        }
        private string url;

        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        /// <summary>
        /// 图文消息
        /// </summary>
        /// <param name="_title">图文消息标题</param>
        /// <param name="_description">图文消息描述</param>
        /// <param name="_picUrl">图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80，限制图片链接的域名需要与开发者填写的基本资料中的Url一致</param>
        /// <param name="_url">点击图文消息跳转链接</param>
        public news(string _title, string _description, string _picUrl, string _url)
        {
            title = _title;
            description = _description;
            picUrl = _picUrl;
            url = _url;
        }
        public news()
        {
        }

    }
}
