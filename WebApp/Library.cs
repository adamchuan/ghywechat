using System;
using System.Collections.Generic;
using System.Text;
using Factory;
using System.IO;
using System.Text;
using Entity;
using System.Net;
namespace WebApp
{
    public class Library:WeChatFactory
    {
        string introduce = "回复 书名（例如：读者文摘） 查找你要的书籍 \r\n 回复#回到主页面";
        /*
            图书馆的系统现在大概是貌似是 
            根据你的sessionID 他存了个缓存在你的session里面
            所以先要得到一个包含搜索结果的cookie(sessionID)
            然后带上该cookie，get访问seachList.aspx得到结果
         */
        public override string Entrance(Entity.User user, Entity.WeiXinData weixindata, Entity.ResponseMsg rm)
        {
            string reply = "";
            if (user.State2[0].Equals("2")) //
            {
                user.State2[0] = "3";//
                user.State[0] = "图书馆"; //给状态1赋值
                user.Value[0] = "";
                reply = rm.ResponseText(introduce);
            }
            else if (user.State2[0] == "3")
            {
                if (weixindata.MsgType.Equals("text"))
                {

                    string bookname = weixindata.Content,
                           cookstr = GetCookieStr(bookname);
                    //开始下一次的跳转
                    HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create("http://10.8.12.2:8081/ScarchList.aspx");
                    request2.Credentials = CredentialCache.DefaultCredentials;
                    request2.Method = "Get";
                    request2.KeepAlive = true;
                    request2.Headers.Add("Cookie:" + cookstr);
                    request2.AllowAutoRedirect = false;

                    HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse();
                    Stream myResponseStream = response2.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                    string retString = myStreamReader.ReadToEnd();
            
                    request2.Abort();
                    response2.Close();
                    myStreamReader.Close();
                    myResponseStream.Close();

                    ///分离数据        
                    int postionstart;
                    int postionend ;
                    postionstart = retString.IndexOf(@"命中目标数:")+6;
                     postionend = retString.IndexOf(@"耗时")-2;
                    int totalcount = Int32.Parse(retString.Substring(postionstart,postionend-postionstart));
                    if (totalcount != 0)
                    {
                        List<news> BookList = new List<news>();
                        news title = new news();
                        title.Title = "数据来源西南财经大学图书馆";
                        BookList.Add(title);
                        for (int i = 0; i < totalcount && i < 5; i++)
                        {
                            news book = new news();

                            string seachstring = "href='BaseView.aspx?ID=" + i + "'>";
                            postionstart = retString.IndexOf(seachstring,postionend)+seachstring.Length;
                            postionend = retString.IndexOf("&nbsp", postionstart); 
                            book.Title = retString.Substring(postionstart, postionend - postionstart)+"\r\n";//获得书名


                            postionstart = retString.IndexOf("索书号：", postionend);//每次都从上一次的postionend开始搜索 减少搜索量
                            postionend=retString.IndexOf("&nbsp",postionstart);
                            book.Title += retString.Substring(postionstart, postionend - postionstart)+"\r\n";//获得索书号

                            postionstart = retString.IndexOf("馆藏数:", postionend);
                            postionend = retString.IndexOf("&nbsp", postionstart);
                            book.Title+= retString.Substring(postionstart, postionend - postionstart);
                         
                            BookList.Add(book);                     
                        }
                        news notice = new news();
                        notice.Title = "发送 # 回到主界面";
                        BookList.Add(notice);
                        reply = rm.ResponseNews(BookList);
                    }
                    else
                        reply = rm.ResponseText("没有找到该书 回复#回到主页面");
                }
                else
                {
                    reply = rm.ResponseText("发送的信息格式不对 回复#回到主页面");
                }
            }
            return reply;
        }
        private string GetCookieStr(string bookname)
        {
            string Url = "http://10.8.12.2:8081/CombinationScarch.aspx";
            string postData = "ScriptManager1=UpdatePanel1%7CImageButton1&__EVENTTARGET=&__EVENTARGUMENT=&__LASTFOCUS=&__VIEWSTATE=%2FwEPDwUKLTM2NzQzMzU0Nw9kFgICAw9kFgICBQ9kFgJmD2QWDgIBD2QWBAIBDxYCHglpbm5lcmh0bWwFJOilv%2BWNl%2Bi0oue7j%2BWkp%2BWtpummhuiXj%2Bafpeivouezu%2Be7n2QCAw8PFgIeBFRleHQF4gY8dGQgc3R5bGU9ImhlaWdodDogMjFweCI%2BPEEgaHJlZj0nZGVmYXVsdC5hc3B4Jz48c3Bhbj7pppbpobU8L3NwYW4%2BPC9BPjwvdGQ%2BPHRkIHN0eWxlPSJoZWlnaHQ6IDIxcHgiPjxBIGhyZWY9J2RlZmF1bHQuYXNweCc%2BPHNwYW4%2B5Lmm55uu5p%2Bl6K%2BiPC9zcGFuPjwvQT48L3RkPjx0ZCBzdHlsZT0iaGVpZ2h0OiAyMXB4Ij48QSBocmVmPSdNYWdhemluZUNhbnRvU2NhcmNoLmFzcHgnPjxzcGFuPuacn%2BWIiuevh%2BWQjTwvc3Bhbj48L0E%2BPC90ZD48dGQgc3R5bGU9ImhlaWdodDogMjFweCI%2BPEEgaHJlZj0nUmVzZXJ2ZWRMaXN0LmFzcHgnPjxzcGFuPumihOe6puWIsOmmhjwvc3Bhbj48L0E%2BPC90ZD48dGQgc3R5bGU9ImhlaWdodDogMjFweCI%2BPEEgaHJlZj0nRXhwaXJlZExpc3QuYXNweCc%2BPHNwYW4%2B6LaF5pyf5YWs5ZGKPC9zcGFuPjwvQT48L3RkPjx0ZCBzdHlsZT0iaGVpZ2h0OiAyMXB4Ij48QSBocmVmPSdOZXdCb29LU2NhcmNoLmFzcHgnPjxzcGFuPuaWsOS5pumAmuaKpTwvc3Bhbj48L0E%2BPC90ZD48dGQgc3R5bGU9ImhlaWdodDogMjFweCI%2BPEEgaHJlZj0nQWR2aWNlc1NjYXJjaC5hc3B4Jz48c3Bhbj7mg4XmiqXmo4DntKI8L3NwYW4%2BPC9BPjwvdGQ%2BPHRkIHN0eWxlPSJoZWlnaHQ6IDIxcHgiPjxBIGhyZWY9J1dyaXRlSkdCb29rLmFzcHgnPjxzcGFuPuaWsOS5puW%2Bgeiuojwvc3Bhbj48L0E%2BPC90ZD48dGQgc3R5bGU9ImhlaWdodDogMjFweCI%2BPEEgaHJlZj0nUmVhZGVyTG9naW4uYXNweCc%2BPHNwYW4%2B6K%2B76ICF55m75b2VPC9zcGFuPjwvQT48L3RkPjx0ZCBzdHlsZT0iaGVpZ2h0OiAyMXB4Ij48QSBocmVmPSdPbmxpbmVTdHVkeS5hc3B4Jz48c3Bhbj7lnKjnur%2Flkqjor6Iv5Z%2B56K6tPC9zcGFuPjwvQT48L3RkPmRkAgMPDxYCHwEFJOilv%2BWNl%2Bi0oue7j%2BWkp%2BWtpuS5puebruaVsOaNruafpeivomRkAgUPZBYEAgIPDxYCHwEFMjxzcGFuPuasoui%2FjuaCqDpHdWVzdCDor7fpgInmi6nkvaDnmoTmk43kvZw8L3NwYW4%2BZGQCAw8PFgIeB1Zpc2libGVoZGQCCw8QDxYGHg1EYXRhVGV4dEZpZWxkBQzlrZfmrrXlkI3np7AeDkRhdGFWYWx1ZUZpZWxkBQnmiYDlsZ7ooageC18hRGF0YUJvdW5kZxYCHghvbmNoYW5nZQUMR2V0VmFsdWUxKCk7EBUKBumimOWQjQnmnaHlvaLnoIEJ6LSj5Lu76ICFDOmmhuiXj%2BWcsOWdgAzmoIflh4bnvJbnoIEM6aKY5ZCN57yp5YaZCeS4u%2BmimOivjQnlh7rniYjogIUJ57Si5Lmm5Y%2B3CeaWh%2BeMruWQjRUKD%2BmmhuiXj%2BS5puebruW6kw%2Fppobol4%2Flhbjol4%2FlupMS5qOA57Si6LSj5Lu76ICF5bqTD%2BmmhuiXj%2BWFuOiXj%2BW6kw%2Fmo4DntKLnvJbnoIHlupMP6aaG6JeP5Lmm55uu5bqTEuajgOe0ouS4u%2BmimOivjeW6kw%2Fppobol4%2Fkuabnm67lupMP6aaG6JeP5Lmm55uu5bqTEuajgOe0ouS4gOWvueWkmuW6kxQrAwpnZ2dnZ2dnZ2dnZGQCEw8QDxYGHwMFDOWtl%2BauteWQjeensB8EBQnmiYDlsZ7ooagfBWcWAh8GBQxHZXRWYWx1ZTIoKTsQFQoG6aKY5ZCNCeadoeW9oueggQnotKPku7vogIUM6aaG6JeP5Zyw5Z2ADOagh%2BWHhue8lueggQzpopjlkI3nvKnlhpkJ5Li76aKY6K%2BNCeWHuueJiOiAhQnntKLkuablj7cJ5paH54yu5ZCNFQoP6aaG6JeP5Lmm55uu5bqTD%2BmmhuiXj%2BWFuOiXj%2BW6kxLmo4DntKLotKPku7vogIXlupMP6aaG6JeP5YW46JeP5bqTD%2BajgOe0oue8lueggeW6kw%2Fppobol4%2Fkuabnm67lupMS5qOA57Si5Li76aKY6K%2BN5bqTD%2BmmhuiXj%2BS5puebruW6kw%2Fppobol4%2Fkuabnm67lupMS5qOA57Si5LiA5a%2B55aSa5bqTFCsDCmdnZ2dnZ2dnZ2dkZAIbDxAPFgYfAwUM5a2X5q615ZCN56ewHwQFCeaJgOWxnuihqB8FZxYCHwYFDEdldFZhbHVlMygpOxAVCgbpopjlkI0J5p2h5b2i56CBCei0o%2BS7u%2BiAhQzppobol4%2FlnLDlnYAM5qCH5YeG57yW56CBDOmimOWQjee8qeWGmQnkuLvpopjor40J5Ye654mI6ICFCee0ouS5puWPtwnmlofnjK7lkI0VCg%2Fppobol4%2Fkuabnm67lupMP6aaG6JeP5YW46JeP5bqTEuajgOe0oui0o%2BS7u%2BiAheW6kw%2Fppobol4%2Flhbjol4%2FlupMP5qOA57Si57yW56CB5bqTD%2BmmhuiXj%2BS5puebruW6kxLmo4DntKLkuLvpopjor43lupMP6aaG6JeP5Lmm55uu5bqTD%2BmmhuiXj%2BS5puebruW6kxLmo4DntKLkuIDlr7nlpJrlupMUKwMKZ2dnZ2dnZ2dnZ2RkAiEPEA8WBh8EBQnlupPplK7noIEfAwUM5Lmm55uu5bqT5ZCNHwVnZBAVBgzkuK3mloflm77kuaYM5aSW5paH5Zu%2B5LmmDOS4reaWh%2Bacn%2BWIigzlpJbmlofmnJ%2FliIoS5Lit5paH6KeG5ZCs6LWE5paZEuilv%2BaWh%2BinhuWQrOi1hOaWmRUGATEBMgEzATQBNQE2FCsDBmdnZ2dnZxYBZmQYAQUeX19Db250cm9sc1JlcXVpcmVQb3N0QmFja0tleV9fFgMFDEltYWdlQnV0dG9uMgUMSW1hZ2VCdXR0b24zBQxJbWFnZUJ1dHRvbjEjXPjaAYsPqQATq0ARbjdi224J8w%3D%3D&__EVENTVALIDATION=%2FwEWSgL4itTVAQLgnZ70BALSwtXkAgLSwsGJCgLK0f%2B6DwL816qKAQLqzM%2FlDAL816qKAQKbh4jwBgLK0f%2B6DwKqvOiVCgLK0f%2B6DwLK0f%2B6DwKO2oDNCwLA456VCgKd4ezEDwK9uNjCBAL8rNjCBAKtgdjUAgKSsMX9DgLy2ImNCwLK0cu6DwL8156KAQLqzPvlDAL8156KAQKbh7zwBgLK0cu6DwKqvNyVCgLK0cu6DwLK0cu6DwKO2rTNCwLvp8XVCwKe4ezEDwK%2BuNjCBAL%2FrNjCBAKugdjUAgLj%2FKO8DwKDlO%2FMCgLK0ce6DwL815KKAQLqzPflDAL815KKAQKbh7DwBgLK0ce6DwKqvNCVCgLK0ce6DwLK0ce6DwKO2rjNCwKKkePqBQKf4ezEDwK%2FuNjCBAL%2BrNjCBAKvgdjUAgLlhPOaBwLq69n0CwLr69n0CwLo69n0CwLp69n0CwLu69n0CwLv69n0CwKqkLzdBgLeh9P7CgKI9%2BrkDgKBkZCRCALKzY3JDQKU3I3JDQL3jKLTDQLSwpnTCALB3%2FSfBgL8ueK8CALB37iODAL%2FueK8CALB38zpBAL%2BueK8CIAnY4hvilVMP0at8V6RRiZFztKb&DropScarchKay1=%E9%A6%86%E8%97%8F%E4%B9%A6%E7%9B%AE%E5%BA%93&txtKay1=" +
                bookname
                + "&Drop1=%E4%B8%AD%E9%97%B4%E4%B8%80%E8%87%B4&DropTJ1=%E5%B9%B6%E4%B8%94&DropScarchKay2=%E9%A6%86%E8%97%8F%E5%85%B8%E8%97%8F%E5%BA%93&txtKay2=&Drop2=%E4%B8%AD%E9%97%B4%E4%B8%80%E8%87%B4&DropTJ2=%E5%B9%B6%E4%B8%94&DropScarchKay3=%E6%A3%80%E7%B4%A2%E8%B4%A3%E4%BB%BB%E8%80%85%E5%BA%93&txtKay3=&Drop3=%E4%B8%AD%E9%97%B4%E4%B8%80%E8%87%B4&DropDB=1&DropSort=%E5%85%A5%E8%97%8F%E6%97%A5%E6%9C%9F&RadioButtonList1=%E6%AD%A3%E5%BA%8F&hidtext1=%E9%A2%98%E5%90%8D&hidValue1=%E9%A6%86%E8%97%8F%E4%B9%A6%E7%9B%AE%E5%BA%93&hidtext2=%E6%9D%A1%E5%BD%A2%E7%A0%81&hidValue2=%E9%A6%86%E8%97%8F%E5%85%B8%E8%97%8F%E5%BA%93&hidtext3=%E8%B4%A3%E4%BB%BB%E8%80%85&hidValue3=%E6%A3%80%E7%B4%A2%E8%B4%A3%E4%BB%BB%E8%80%85%E5%BA%93&__ASYNCPOST=true&ImageButton1.x=28&ImageButton1.y=5";
            WebRequest webRequest = WebRequest.Create(Url);
            HttpWebRequest request = webRequest as HttpWebRequest;

            //设置cookie
            CookieContainer cc = new CookieContainer();
            request.CookieContainer = cc;//返回的cookie会附加在这个容器里面
            request.Method = "Post";
            request.Credentials = CredentialCache.DefaultCredentials;
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.2; rv:20.0) Gecko/20100101 Firefox/20.0";
            request.Referer = "	http://10.8.12.2:8081/";
            request.KeepAlive = true;
            request.AllowAutoRedirect = false;

            byte[] byteArray = Encoding.UTF8.GetBytes(postData); // 转化
            request.ContentLength = byteArray.Length;
            Stream newStream = request.GetRequestStream();

            newStream.Write(byteArray, 0, byteArray.Length);    //写入参数
            newStream.Close();

            // Send the data.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            request.Abort();
            response.Close();
            //分析
            response.Cookies = cc.GetCookies(request.RequestUri);//获得cookies

            CookieCollection cook = response.Cookies; //得到结果的Cookie容器
            String cookiestr = request.CookieContainer.GetCookieHeader(request.RequestUri); //得到cookie头 

            return cookiestr;
        }
    }
}
