using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;

namespace Spider
{
    class Program
    {
        static void Main(string[] args)
        {

            ////抓去网上图片，下载到指定路径
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            //下载源网页代码  
            //https://tieba.baidu.com/p/1777771273#!/l/p1
            //https://tieba.baidu.com/p/2460150866?pn=3
            //<img pic_type="0" class="BDE_Image" src="https://imgsa.baidu.com/forum/w%3D580/sign=286958af622762d0803ea4b790ed0849/a317fdfaaf51f3de350a5ee895eef01f3b297987.jpg" pic_ext="jpeg" height="419" width="560">
            //<img src="https://imgsa.baidu.com/forum/w%3D223/sign=eb5369af7bf40ad115e4c0e1642d1151/de5c10385343fbf25339f8b4b07eca8065388f14.jpg" style="width:223px;height:297px;left:0px;top:0px;">
            //<img src="https://imgsa.baidu.com/forum/w%3D223/sign=d160da9233fa828bd1239ae1ce1e41cd/b54bd11373f0820237f1cfa84bfbfbedaa641bc1.jpg" style="width:223px;height:297px;left:0px;top:0px;">
            //<img src = "https://imgsa.baidu.com/forum/w%3D223/sign=2fb3be9eb6003af34dbadb62062bc619/f8fe9925bc315c602dc835788db1cb13485477c3.jpg" style = "width:223px;height:297px;left:0px;top:0px;" >
            //<img src="https://imgsa.baidu.com/forum/w%3D223/sign=527ffe6dd1c8a786be2a4d0c5408c9c7/60f0f736afc379317d35d8beebc4b74542a911c3.jpg" style="width:223px;height:297px;left:0px;top:0px;">
            //src=\"(.+?\\.jpg)\" pic_ext
            for (int k = 1; k < 59; k++)
            {
                Thread.Sleep(1000);
                string html = wc.DownloadString(string.Format("https://tieba.baidu.com/p/2460150866?pn={0}",k));
                MatchCollection matches = Regex.Matches(html, "src=\"(.+?\\.jpg)\" pic_ext");
                int i = 1;
                foreach (Match item in matches)
                {
                    string str = item.Groups[1].Value.Substring(item.Groups[1].Value.LastIndexOf("."), 4);//截取图片后缀名称
                                                                                                          //下载图片到指定路径  
                                                                                                          // wc.DownloadFile(item.Groups[1].Value, @"E:\pic\" + Path.GetFileName(item.Groups[1].Value));
                    Console.WriteLine("正在下载..." + item.Groups[1].Value);
                    wc.DownloadFile(item.Groups[1].Value, @"E:\pic\" + i +k+ str);
                    Console.WriteLine( string.Format("第{0}页{1}下载完毕，准备下一张..",k, i + k + str ));
                    i++;
                }
            }


            //博客园大神文章列表 及阅读详情
            //WebClient wc = new WebClient();
            //wc.Encoding = Encoding.UTF8;
            //string html = wc.DownloadString("http://www.cnblogs.com/artech/");
            //MatchCollection matches = Regex.Matches(html, "\">(.*?)</a>");
            //foreach (Match item in matches)
            //{

            //    if (item.Groups[1].Value.Contains("c_b_p_desc") )
            //    {
            //    }
            //    else
            //    {
            //       string str =  ReplaceHtmlTag(item.Groups[1].Value);
            //       Console.WriteLine(str); 
            //    }
            //}

            //string url = "http://zhidao.baidu.com/link?url=cvF0de2o9gkmk3zW2jY23TLEUs6wX-79E1DQVZG7qaBhEVT_xlh6TO7p0W4qwuAZ_InLymC_-mJBBcpdbzTeq_";
            //WebClient wc = new WebClient();
            //wc.Encoding = Encoding.UTF8;
            //string str = wc.DownloadString(url);
            //MatchCollection matchs = Regex.Matches(str, @"\w+@([-\w])+([\.\w])+", RegexOptions.ECMAScript);
            //foreach (Match item in matchs)
            //{
            //    Console.WriteLine(item.Value);
            //}
            //Console.WriteLine(matchs.Count);  


        }
        /// <summary>
        /// 去除html代码
        /// </summary>
        /// <param name="html"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ReplaceHtmlTag(string html, int length = 0)
        {
            string strText = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "");
            strText = System.Text.RegularExpressions.Regex.Replace(strText, "&[^;]+;", "");

            if (length > 0 && strText.Length > length)
                return strText.Substring(0, length);

            return strText;
        }
    }
}
