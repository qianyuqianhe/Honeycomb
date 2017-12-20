using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Honeycomb
{
    public partial class frm : Form
    {
        public frm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 获取网页源码
        /// </summary>
        /// <param name="url">网页地址</param>
        /// <returns>页面源码</returns>
        private string GetHttpWebRequest(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(uri);
                myReq.Method = "GET";
                myReq.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.84 Safari/537.36";
                myReq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
                myReq.KeepAlive = true;
                myReq.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
                myReq.Referer = url;
                myReq.AllowAutoRedirect = false;
                HttpWebResponse result = (HttpWebResponse)myReq.GetResponse();
                Stream receviceStream = result.GetResponseStream();
                StreamReader readerOfStream = new StreamReader(receviceStream, System.Text.Encoding.GetEncoding("utf-8"));
                string strHTML = readerOfStream.ReadToEnd();
                readerOfStream.Close();
                receviceStream.Close();
                result.Close();
                return strHTML;
            }
            catch (Exception e)
            {
                return "";
            }
        }
        /// <summary>
        /// 通过正则表达式匹配内容
        /// </summary>
        /// <param name="text">待匹配的内容</param>
        /// <param name="regular">正则表达式</param>
        /// <returns>匹配结果的集合</returns>
        private List<string> GetRegularExpressionValue(string text, string regular)
        {
            List<string> value = new List<string>();
            try
            {
                MatchCollection _matchCollection = Regex.Matches(text, regular);
                foreach (Match match in _matchCollection)
                {
                    value.Add(match.Groups[1].Value.ToString());
                }
            }
            catch
            {
                value = new List<string>();
            }
            return value;
        }
        private String GetAbsoluteUrl(String relativeUrl, string baseUrl)
        {
            Uri baseUri = new Uri(baseUrl);
            Uri absoluteUri = new Uri(baseUri, relativeUrl);
            return absoluteUri.ToString();
        }
        List<string> AllUrlList = new List<string>();
        Queue AllUrlQueue = new Queue();
        List<string> ContentUrlList = new List<string>();
        //Queue ContentUrlQueue = new Queue();


        public string GetURLContentByWebBrowser(string url)
        {
            try
            {
                string result = null;
                WebBrowser wb = new WebBrowser();
                wb.ScriptErrorsSuppressed = true;
                //在这里Navigate一个空白页面
                wb.Navigate("about:blank");
                string htmlcode = GetHttpWebRequest(url);
                wb.Document.Write(htmlcode);
                return result;

            }
            catch (Exception ex)
            {
                //WriteLog.Writelog("这是获取页面全部html代码时发生的错误：" + url, ex); 
                throw ex;
                //return ;
            }
        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            string startUrl = "https://www.alibaba.com";
            AllUrlList.Add(startUrl);
            AllUrlQueue.Enqueue(startUrl);
            Thread Thread = new Thread(new ThreadStart(ThreadUrl));
            Thread.SetApartmentState(ApartmentState.STA);
            Thread.Start();
        }
        private void ThreadUrl()
        {
            while (AllUrlQueue.Count!=0&&AllUrlList.Count<=100)
            {
                string log = "";
                string url = AllUrlQueue.Dequeue().ToString();
                string text = GetURLContentByWebBrowser(url);
                File.WriteAllText("text.txt", text);
                foreach (string u in GetRegularExpressionValue(text, "<a[^>]*href=\"(.*?)\""))
                {
                    string link = GetAbsoluteUrl(u,url);
                    log += link + "\r\n";
                    AllUrlList.Add(link);
                    AllUrlQueue.Enqueue(link);
                    if (link.Contains("product-detail"))
                    {
                        ContentUrlList.Add(link);
                    }                   
                }
                log += AllUrlQueue.Count+"   "+AllUrlList.Count+"   "+ ContentUrlList.Count+"\r\n";
                File.WriteAllText("log.txt", log);
            }
            string newText = "";
            foreach(string l in ContentUrlList)
            {
                newText += l + "\r\n";
            }
            File.WriteAllText("test.txt",newText);
            MessageBox.Show("OK!");
        }
        


    }
}
