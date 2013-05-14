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
                    string bookname = weixindata.Content;
                    string Url = "http://10.8.12.2:8081/Default.aspx";
                    string postData = "Button1=%E5%BC%80%E5%A7%8B%E6%A3%80%E7%B4%A2&DropDownList1=%E6%89%80%E6%9C%89&DropDownList2=%E9%A6%86%E8%97%8F%E4%B9%A6%E7%9B%AE%E5%BA%93&DropDownList3=%E5%85%A5%E8%97%8F%E6%97%A5%E6%9C%9F&DropDownList4=%E4%B8%AD%E9%97%B4%E4%B8%80%E8%87%B4&DropLanguage=%E4%B8%8D%E9%99%90&DrpHouse=%E6%89%80%E6%9C%89%E9%A6%86&HiddenValue=&RadioButtonList1=%E5%88%97%E8%A1%A8%E6%96%B9%E5%BC%8F&ScriptManager1=UpdatePanel1%7CButton1&" +
                        "TxtIndex=" + bookname+ "&__EVENTARGUMENT=&__EVENTTARGET=&__EVENTVALIDATION=%2FwEWWgK0va7%2FAwLgnZ70BALSwtXkAgLSwsGJCgKjjKTsAgKd5I%2FlCgKSi6WLBgKTi6WLBgKQi6WLBgKRi6WLBgKWi6WLBgKXi6WLBgL918izBALn%2Fa%2BACwLn%2Fa%2BACwLn%2Fa%2BACwLn%2Fa%2BACwLn%2Fa%2BACwLt37y%2FCQLt37y%2FCQLNhoi5AgKg5I%2FlCgLTrdSNCQL4rPjBBwKnururBQLxyoK0AQLY4tPQAgKOpaXzCQKKpbXzCQK9pcHzCQK1pf3zCQK2pfXzCQKPpYHzCQKJpfXzCQKMpbXzCQKMpYHzCQKNpYHzCQKNpZnzCQKNpcHzCQKOpZXyCQKPpbHzCQKPpanzCQKIpcnzCQKJpdHzCQKKpYHzCQKKpanzCQK7pa3zCQK7pcHzCQK0pYnzCQK0pY3zCQK0pa3zCQK0paXzCQK0pfXzCQK0pcnzCQK0pc3zCQK2pd3zCQK2pZHyCQK3pbHzCQKwpaXzCQKwpZHyCQKxpbnzCQKxpf3zCQKjpa3zCQKjpc3zCQKjpcHzCQK9pbnzCQK%2BpYHzCQK%2Bpa3zCQK%2FpbHzCQK%2FpbXzCQK%2Fpd3zCQK%2FpcHzCQK%2FpZHyCQK4pZnzCQK4panzCQK5paXzCQKrpbHzCQKkpcHzCQKkpZXyCQLwsfzjBgLxl8uOAwLSy7SPBQL3jKLTDQKM54rGBgLe64HXAgLB36CtCwLgyKrzCALsh7ajDQLj6JzNAQKA0PL0CyA4IHcuDTdwtbT1vKkTrlnr7XUM&__LASTFOCUS=&__VIEWSTATE=%2FwEPDwUKLTIzODUwODY1Nw9kFgICAw9kFgICBQ9kFgJmD2QWFAIBD2QWBAIBDxYCHglpbm5lcmh0bWwFJOilv%2BWNl%2Bi0oue7j%2BWkp%2BWtpummhuiXj%2Bafpeivouezu%2Be7n2QCAw8PFgIeBFRleHQF1wQ8dGQgc3R5bGU9ImhlaWdodDogMjFweCI%2BPEEgaHJlZj0nZGVmYXVsdC5hc3B4Jz48c3Bhbj7pppbpobU8L3NwYW4%2BPC9BPjwvdGQ%2BPHRkIHN0eWxlPSJoZWlnaHQ6IDIxcHgiPjxBIGhyZWY9J2RlZmF1bHQuYXNweCc%2BPHNwYW4%2B5Lmm55uu5p%2Bl6K%2BiPC9zcGFuPjwvQT48L3RkPjx0ZCBzdHlsZT0iaGVpZ2h0OiAyMXB4Ij48QSBocmVmPSdNYWdhemluZUNhbnRvU2NhcmNoLmFzcHgnPjxzcGFuPuacn%2BWIiuevh%2BWQjTwvc3Bhbj48L0E%2BPC90ZD48dGQgc3R5bGU9ImhlaWdodDogMjFweCI%2BPEEgaHJlZj0nUmVzZXJ2ZWRMaXN0LmFzcHgnPjxzcGFuPumihOe6puWIsOmmhjwvc3Bhbj48L0E%2BPC90ZD48dGQgc3R5bGU9ImhlaWdodDogMjFweCI%2BPEEgaHJlZj0nRXhwaXJlZExpc3QuYXNweCc%2BPHNwYW4%2B6LaF5pyf5YWs5ZGKPC9zcGFuPjwvQT48L3RkPjx0ZCBzdHlsZT0iaGVpZ2h0OiAyMXB4Ij48QSBocmVmPSdOZXdCb29LU2NhcmNoLmFzcHgnPjxzcGFuPuaWsOS5pumAmuaKpTwvc3Bhbj48L0E%2BPC90ZD48dGQgc3R5bGU9ImhlaWdodDogMjFweCI%2BPEEgaHJlZj0nUmVhZGVyTG9naW4uYXNweCc%2BPHNwYW4%2B6K%2B76ICF55m75b2VPC9zcGFuPjwvQT48L3RkPmRkAgMPDxYCHwEFJOilv%2BWNl%2Bi0oue7j%2BWkp%2BWtpuS5puebruaVsOaNruafpeivomRkAgcPZBYEAgIPDxYCHwEFMjxzcGFuPuasoui%2FjuaCqDpHdWVzdCDor7fpgInmi6nkvaDnmoTmk43kvZw8L3NwYW4%2BZGQCAw8PFgIeB1Zpc2libGVoZGQCDg9kFgJmDw8WAh8BBa0Q54Ot6Zeo5qOA57SiOjxTUEFOIHN0eWxlPSJCT1JERVItQk9UVE9NOiAjNDg5MUJGIDFweCBzb2xpZDsgQkFDS0dST1VORC1DT0xPUjogI0Q4RUZGNTsgQ1VSU09SOiBoYW5kOyBCT1JERVItUklHSFQ6ICM0ODkxQkYgMXB4IHNvbGlkIiBvbmNsaWNrPSJTZXRWYWx1ZSgn6IuP6I%2By55qE5LiW55WMJyk7Ij4g6IuP6I%2By55qE5LiW55WMPC9TUEFOPiZuYnNwOyZuYnNwOzxTUEFOIHN0eWxlPSJCT1JERVItQk9UVE9NOiAjNDg5MUJGIDFweCBzb2xpZDsgQkFDS0dST1VORC1DT0xPUjogI0Q4RUZGNTsgQ1VSU09SOiBoYW5kOyBCT1JERVItUklHSFQ6ICM0ODkxQkYgMXB4IHNvbGlkIiBvbmNsaWNrPSJTZXRWYWx1ZSgn5LiH5Y6G5Y2B5LqU5bm0Jyk7Ij4g5LiH5Y6G5Y2B5LqU5bm0PC9TUEFOPiZuYnNwOyZuYnNwOzxTUEFOIHN0eWxlPSJCT1JERVItQk9UVE9NOiAjNDg5MUJGIDFweCBzb2xpZDsgQkFDS0dST1VORC1DT0xPUjogI0Q4RUZGNTsgQ1VSU09SOiBoYW5kOyBCT1JERVItUklHSFQ6ICM0ODkxQkYgMXB4IHNvbGlkIiBvbmNsaWNrPSJTZXRWYWx1ZSgn6K6h6YeP57uP5rWO5a2mJyk7Ij4g6K6h6YeP57uP5rWO5a2mPC9TUEFOPiZuYnNwOyZuYnNwOzxTUEFOIHN0eWxlPSJCT1JERVItQk9UVE9NOiAjNDg5MUJGIDFweCBzb2xpZDsgQkFDS0dST1VORC1DT0xPUjogI0Q4RUZGNTsgQ1VSU09SOiBoYW5kOyBCT1JERVItUklHSFQ6ICM0ODkxQkYgMXB4IHNvbGlkIiBvbmNsaWNrPSJTZXRWYWx1ZSgn57uP5rWO5a2m5Y6f55CGJyk7Ij4g57uP5rWO5a2m5Y6f55CGPC9TUEFOPiZuYnNwOyZuYnNwOzxTUEFOIHN0eWxlPSJCT1JERVItQk9UVE9NOiAjNDg5MUJGIDFweCBzb2xpZDsgQkFDS0dST1VORC1DT0xPUjogI0Q4RUZGNTsgQ1VSU09SOiBoYW5kOyBCT1JERVItUklHSFQ6ICM0ODkxQkYgMXB4IHNvbGlkIiBvbmNsaWNrPSJTZXRWYWx1ZSgn5b6u6KeC57uP5rWO5a2mJyk7Ij4g5b6u6KeC57uP5rWO5a2mPC9TUEFOPiZuYnNwOyZuYnNwOzxTUEFOIHN0eWxlPSJCT1JERVItQk9UVE9NOiAjNDg5MUJGIDFweCBzb2xpZDsgQkFDS0dST1VORC1DT0xPUjogI0Q4RUZGNTsgQ1VSU09SOiBoYW5kOyBCT1JERVItUklHSFQ6ICM0ODkxQkYgMXB4IHNvbGlkIiBvbmNsaWNrPSJTZXRWYWx1ZSgn6LSn5biB6YeR6J6N5a2mJyk7Ij4g6LSn5biB6YeR6J6N5a2mPC9TUEFOPiZuYnNwOyZuYnNwOzxTUEFOIHN0eWxlPSJCT1JERVItQk9UVE9NOiAjNDg5MUJGIDFweCBzb2xpZDsgQkFDS0dST1VORC1DT0xPUjogI0Q4RUZGNTsgQ1VSU09SOiBoYW5kOyBCT1JERVItUklHSFQ6ICM0ODkxQkYgMXB4IHNvbGlkIiBvbmNsaWNrPSJTZXRWYWx1ZSgnRVhDRUwnKTsiPiBFWENFTDwvU1BBTj4mbmJzcDsmbmJzcDs8U1BBTiBzdHlsZT0iQk9SREVSLUJPVFRPTTogIzQ4OTFCRiAxcHggc29saWQ7IEJBQ0tHUk9VTkQtQ09MT1I6ICNEOEVGRjU7IENVUlNPUjogaGFuZDsgQk9SREVSLVJJR0hUOiAjNDg5MUJGIDFweCBzb2xpZCIgb25jbGljaz0iU2V0VmFsdWUoJ%2BWNmuW8iOiuuicpOyI%2BIOWNmuW8iOiuujwvU1BBTj4mbmJzcDsmbmJzcDs8U1BBTiBzdHlsZT0iQk9SREVSLUJPVFRPTTogIzQ4OTFCRiAxcHggc29saWQ7IEJBQ0tHUk9VTkQtQ09MT1I6ICNEOEVGRjU7IENVUlNPUjogaGFuZDsgQk9SREVSLVJJR0hUOiAjNDg5MUJGIDFweCBzb2xpZCIgb25jbGljaz0iU2V0VmFsdWUoJ%2BmHkeiejeeahOmAu%2Bi%2BkScpOyI%2BIOmHkeiejeeahOmAu%2Bi%2BkTwvU1BBTj4mbmJzcDsmbmJzcDs8U1BBTiBzdHlsZT0iQk9SREVSLUJPVFRPTTogIzQ4OTFCRiAxcHggc29saWQ7IEJBQ0tHUk9VTkQtQ09MT1I6ICNEOEVGRjU7IENVUlNPUjogaGFuZDsgQk9SREVSLVJJR0hUOiAjNDg5MUJGIDFweCBzb2xpZCIgb25jbGljaz0iU2V0VmFsdWUoJ%2BWFrOWPuOeQhui0oicpOyI%2BIOWFrOWPuOeQhui0ojwvU1BBTj4mbmJzcDsmbmJzcDs8U1BBTiBzdHlsZT0iQk9SREVSLUJPVFRPTTogIzQ4OTFCRiAxcHggc29saWQ7IEJBQ0tHUk9VTkQtQ09MT1I6ICNEOEVGRjU7IENVUlNPUjogaGFuZDsgQk9SREVSLVJJR0hUOiAjNDg5MUJGIDFweCBzb2xpZCI%2BIDxhIGhyZWY9SG90U2NhcmNoS2F5LmFzcHg%2B5pu05aSaLi4uPC9hPjwvU1BBTj5kZAIQDxAPFgYeDURhdGFUZXh0RmllbGQFDOS5puebruW6k%2BWQjR4ORGF0YVZhbHVlRmllbGQFCeW6k%2BmUrueggR4LXyFEYXRhQm91bmRnZBAVBwzkuK3mloflm77kuaYM5aSW5paH5Zu%2B5LmmDOS4reaWh%2Bacn%2BWIigzlpJbmlofmnJ%2FliIoS5Lit5paH6KeG5ZCs6LWE5paZEuilv%2BaWh%2BinhuWQrOi1hOaWmQbmiYDmnIkVBwExATIBMwE0ATUBNgbmiYDmnIkUKwMHZ2dnZ2dnZxYBAgZkAhQPEA8WBh8DBQnlrZfmrrXlkI0fBAUJ5omA5bGe6KGoHwVnFgIeCG9uY2hhbmdlBQtHZXRWYWx1ZSgpOxAVBQbpopjlkI0J6LSj5Lu76ICFCeWHuueJiOiAhQzlh7rniYjml6XmnJ8J57Si5Lmm5Y%2B3FQUP6aaG6JeP5Lmm55uu5bqTD%2BmmhuiXj%2BS5puebruW6kw%2Fppobol4%2Fkuabnm67lupMP6aaG6JeP5Lmm55uu5bqTD%2BmmhuiXj%2BS5puebruW6kxQrAwVnZ2dnZxYBZmQCHA8QDxYGHwMFBuWQjeensB8EBQbku6PnoIEfBWdkEBU1CgkJCQnkuK3mlocKCQkJCeiLseaWhwoJCQkJ5L%2BE5paHCgkJCQnml6XmlocKCQkJCeacneaWhwoJCQkJ5b635paHCgkJCQnms5XmlocWCQkJCemYv%2BWwlOW3tOWwvOS6muaWhxAJCQkJ6Zi%2F5ouJ5Lyv5paHEwkJCQnnmb3kv4TnvZfmlq%2FmlocTCQkJCeS%2FneWKoOWIqeS6muaWhw0JCQkJ57yF55S45paHCgkJCQnmjbfmlocNCQkJCei%2BvumHjOaWhw0JCQkJ5Li56bqm5paHEAkJCQnopb%2Fnj63niZnmlocNCQkJCeiKrOWFsOaWhxMJCQkJ5qC86bKB5ZCJ5Lqa5paHDQkJCQnluIzohYrmlocNCQkJCeiNt%2BWFsOaWhxAJCQkJ5YyI54mZ5Yip5paHDQkJCQnljbDlnLDor60NCQkJCeWNsOWwvOaWhxAJCQkJ5biM5Lyv6I6x5paHEAkJCQnkuYzlsJTlpJrmlocNCQkJCeazouaWr%2BaWhw0JCQkJ5Yaw5bKb5paHEAkJCQnmhI%2FlpKfliKnmlocQCQkJCeafrOWflOWvqOaWhxMJCQkJ5ZCJ5bCU5ZCJ5pav5paHDQkJCQnogIHmjJ3mlocKCQkJCeiSmeaWhw0JCQkJ6ams5p2l5paHDQkJCQnmjKrlqIHmlocQCQkJCeWwvOaziuWwlOaWhw0JCQkJ5rOi5YWw5paHEAkJCQnokaHokITniZnmlocQCQkJCeaZruS7gOWbvuaWhxMJCQkJ572X6ams5bC85Lqa5paHDQkJCQnnkZ7lhbjmlocTCQkJCeaWr%2Ba0m%2BS8kOWFi%2BaWhxAJCQkJ5aGU5ZCJ5YWL5paHCgkJCQnol4%2For60KCQkJCeazsOaWhxAJCQkJ5Zyf6ICz5YW25paHEAkJCQnlnJ%2FlupPmm7zmlocQCQkJCee7tOWQvuWwlOivrRAJCQkJ5LmM5YWL5YWw5paHDQkJCQnotorljZfmlocQCQkJCeWTiOiQqOWFi%2BaWhxMJCQkJ5Y2X5pav5ouJ5aSr5paHEwkJCQnkuYzlhbnliKvlhYvmlocG5LiN6ZmQFTUCQ04CR0ICUlUCSlACS1ICREUCRlICQUICQUUCQkUCQkcCQlUCQ1oCREECREsCRVMCRkkCR0UCR0sCSEwCSFUCSUMCSUQCSUwCSU4CSVICSVMCSVQCS0gCS1kCTEECTU4CTVkCTk8CTlACUEwCUFQCUFUCUk8CU0UCU0wCVEECVEICVEgCVFUCVFkCVUcCVUsCVk4CWEECWVUCWVoG5LiN6ZmQFCsDNWdnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZGQCHg8QZGQWAQIBZAIqDxAPFgYfAwUJ5Y2V5L2N5ZCNHwQFCemmhumUrueggR8FZ2QQFQIJ5Zu%2B5Lmm6aaGCeaJgOaciemmhhUCATEJ5omA5pyJ6aaGFCsDAmdnFgECAWQCLA8PFgIfAQVIJm5ic3AmbmJzcCZuYnNwJm5ic3AmbmJzcDxpbWcgc3JjPUltYWdlcy9pY29uLmdpZiA%2B6K6%2F6Zeu6YeP5YWxNDI1OTAw5qyhZGQYAQUeX19Db250cm9sc1JlcXVpcmVQb3N0QmFja0tleV9fFgIFDEltYWdlQnV0dG9uMgUMSW1hZ2VCdXR0b24zUd4K%2F7bP6ais1MJdD1Nxv2oNkDM%3D&hidValue=%E9%A6%86%E8%97%8F%E4%B9%A6%E7%9B%AE%E5%BA%93&hidtext=%E9%A2%98%E5%90%8D";
                    WebRequest webRequest = WebRequest.Create(Url);
                    HttpWebRequest request = webRequest as HttpWebRequest;

                    //设置cookie
                    CookieContainer cc = new CookieContainer();
                    request.CookieContainer = cc;//返回的cookie会附加在这个容器里面
                    request.Method = "Post";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.UserAgent = "Mozilla/5.0 (Windows NT 6.2; rv:20.0) Gecko/20100101 Firefox/20.0";
                    request.Referer = "	http://10.8.12.2:8081/";
                    request.KeepAlive = true;
                    byte[] byteArray = Encoding.UTF8.GetBytes(postData); // 转化
                    request.ContentLength = byteArray.Length;
                    Stream newStream = request.GetRequestStream();

                    newStream.Write(byteArray, 0, byteArray.Length);    //写入参数
                    newStream.Close();

                    // Send the data.
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    response.Cookies = cc.GetCookies(request.RequestUri);//获得cookies


                    ///下面是读取返回信息
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    string retString = myStreamReader.ReadToEnd();
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
    }
}
