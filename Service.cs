using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
namespace WindowsFormsApp1
{
    class Service
    {
        private CookieContainer container;
        Image loadImage()
        {
            String url = "http://210.42.121.241/servlet/GenImg";
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(url);
            request1.Method = "GET";
            request1.Accept = "image/png, image/svg+xml, image/*; q=0.8, */*; q=0.5";
            request1.CookieContainer = new CookieContainer();
            container = request1.CookieContainer;
            request1.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.140 Safari/537.36 Edge/17.17134";
            HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
            response1.Cookies = container.GetCookies(request1.RequestUri);
            Stream stream = response1.GetResponseStream();
            return Image.FromStream(stream);
        }
        bool logIn(string ID, string passWord, string key)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://210.42.121.241/servlet/Login");
            request.Method = "POST";
            //根据正常登陆的过程，抓包看数据怎么传输，然后模拟
            request.CookieContainer = new CookieContainer();
            //传入验证码cookie
            request.CookieContainer = container;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.140 Safari/537.36 Edge/17.17134";
            request.Accept = "text/html, application/xhtml+xml, application/xml; q=0.9, */*; q=0.8";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Referer = "http://210.42.121.241/servlet/Login";

            //发现密码上传时，用md5加密，所以也要加密上传
            byte[] result = Encoding.Default.GetBytes(passWord);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            String pwd = BitConverter.ToString(output).Replace("-", "").ToLower();


            //根据抓包发现的表单结构传入数据
            String postData = String.Format("id={0}&pwd={1}&xdvfb={2}", ID, pwd, key);
            byte[] postdatabyte = Encoding.ASCII.GetBytes(postData);
            request.ContentLength = postdatabyte.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(postdatabyte, 0, postdatabyte.Length);
            }


            //接收输出数据
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string strWebData = string.Empty;
            Encoding encoding =Encoding.GetEncoding( response.CharacterSet);
            using (StreamReader reader = new StreamReader(response.GetResponseStream(),encoding))
            {
                strWebData = reader.ReadToEnd();
            }
            return true;
        }
        string getToken(string webData)
        {
            string tokenPatten = "csrftoken=(?<token>[a-zA-Z0-9-]+)";
            Regex r = new Regex(tokenPatten);
            Match match = r.Match(webData);
            string token = match.Result("${token}");
            return token;

        }
        string getTable(string token)
        {

            //模拟get操作
            HttpWebRequest requestTable = (HttpWebRequest)WebRequest.Create("http://210.42.121.241/servlet/Svlt_QueryStuLsn?action=queryStuLsn&csrftoken=" + token);
            requestTable.Method = "GET";
            requestTable.CookieContainer = container;
            requestTable.Referer = "http://210.42.121.241/stu/stu_index.jsp";
            requestTable.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.140 Safari/537.36 Edge/17.17134";
            requestTable.Accept = "text/html, application/xhtml+xml, application/xml; q=0.9, */*; q=0.8";
            requestTable.ContentType = "application/x-www-form-urlencoded";


            HttpWebResponse responseTable = (HttpWebResponse)requestTable.GetResponse();
            string studenTable = string.Empty;
            Encoding en = Encoding.GetEncoding(responseTable.CharacterSet);
            using (StreamReader reader = new StreamReader(responseTable.GetResponseStream(), en))
            {
                studenTable = reader.ReadToEnd();
            }
            return studenTable;
        }
        string getGrade(string token, string year, string term, string leanType, string scoreFlag)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://210.42.121.241/servlet/Svlt_QueryStuScore?csrftoken=" + token
                + "&year=" + year
                + "&term=" + term
                + "&learnType=" + leanType
                + "&scoreFlag=" + scoreFlag
                + "&t=");
            return"";
        }

    }
}
