using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net;
using System.Xml;
using System.IO;
namespace Common
{
    public class Used
    {
        public string Get(string Url, string Referer, Encoding Encoder, ref string CookieStr)
        {

            string result = "";

            WebClient myClient = new WebClient();

            if (CookieStr != "")
            {

                myClient.Headers.Add(CookieStr);

            }

            myClient.Encoding = Encoder;

           return myClient.DownloadString(Url);

            
        }

        public string Post(string Url, string Referer, Encoding Encoder, ref string CookieStr, string Data)
        {

            string result = "";

            WebClient myClient = new WebClient();

            if (CookieStr != "")
            {

                myClient.Headers.Add(CookieStr);

            }

            myClient.Encoding = Encoder;

            result = myClient.UploadString(Url, Data);

            if (CookieStr == "")
            {

                CookieStr = myClient.ResponseHeaders["Set-Cookie"].ToString();

                CookieStr = GetCookie(CookieStr);

            }

            return result;

        }
        private string GetCookie(string CookieStr)
        {

            string result = "";

            string[] myArray = CookieStr.Split(',');

            if (myArray.Length > 0)
            {

                result = "Cookie: ";

                foreach (var str in myArray)
                {

                    string[] CookieArray = str.Split(';');

                    result += CookieArray[0].Trim();

                    result += "; ";

                }

                result = result.Substring(0, result.Length - 2);

            }

            return result;

        }
        /// <summary>
        /// unix时间转换为datetime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimeToTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// datetime转换为unixtime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        /// <summary>
        /// 写日志(用于跟踪)
        /// </summary>
        public static void WriteLog(string strMemo)
        {
            string filename = System.AppDomain.CurrentDomain.BaseDirectory + @"/log.txt";
            StreamWriter sr = null;
            try
            {
                if (!File.Exists(filename))
                {
                    sr = File.CreateText(filename);
                }
                else
                {
                    sr = File.AppendText(filename);
                }
                sr.WriteLine(strMemo);
            }
            catch
            {
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
        public static bool CheckUserNum(string userNum, string password)
        {

            string Url = @"http://v.ghy.cn/Service.asmx/chkUser?userNum=" + userNum + "&password=" + password;

            WebClient wc = new WebClient();

            wc.Credentials = CredentialCache.DefaultCredentials;

            byte[] dataBuffer = wc.DownloadData(Url);

            string strWebData = Encoding.Default.GetString(dataBuffer);

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(strWebData);//获取webservice返回的xmlstring，读到doc中

            string a = doc.GetElementsByTagName("int")[0].InnerText;

            if (a.Equals("2"))//验证节点内容，确定返回值
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
