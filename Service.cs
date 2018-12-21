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
        //private List<Lesson> Lessons =new List<Lesson>();
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
            Encoding encoding = Encoding.GetEncoding(response.CharacterSet);
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
            {
                strWebData = reader.ReadToEnd();
            }
            if (request.Timeout == 200)
                return false;
            else
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
            return "";
        }
        public string[] getInfo(string a)//返回一个课程表数组
        {

            string[] classtable = new string[25];
            // Regex spaceReg = new Regex("\\s{2,}|\\ \\;", RegexOptions.Compiled | RegexOptions.IgnoreCase);//把一个以上的空格替换为一个空格
            int i = 0, j = 0;
            string pattern = "var lessonName = \".+\".+[\\s\\S]+?var day = \".+\".+[\\s\\S]+?var note .+";
            Regex rx = new Regex(pattern);
            Match m = rx.Match(a);
            while (m.Success)
            {
                classtable[i] = m.Value;
                i++;
                m = rx.Match(a, m.Index + m.Length);

            }
            while (j < 25)
            {

                try
                {
                    classtable[j] = classtable[j].Replace("=", "").Replace("\"", "").Replace(";", " ").Replace("var", "");//去掉等于号和引号
                    classtable[j] = classtable[j].Replace("lessonName", "").Replace("day", "").Replace("beginWeek", "");
                    classtable[j] = classtable[j].Replace("endWeek", "").Replace("beginTime", "").Replace("endTime", "");
                    classtable[j] = classtable[j].Replace("classRoom", "").Replace("teacherName", "").Replace("professionName", "");
                    classtable[j] = classtable[j].Replace("planType", "").Replace("credit", "").Replace("areaName", "").Replace("weekInterVal", "");

                }
                catch (Exception e)
                {

                }
                StreamWriter sw = new StreamWriter("class.txt", true, Encoding.UTF8);
                sw.WriteLine(classtable[j]);
                sw.Close();
                //定义一个变量用来存读到的东西
                string text = "";
                //用一个读出流去读里面的数据           
                using (StreamReader reader2 = new StreamReader(@"D:\class.txt", Encoding.Default))
                {
                    //读一行
                    string line = reader2.ReadLine();
                    while (line != null)
                    {
                        //如果这一行里面有weekDay或者grade等等这几个字符，就不加入到text中，如果没有就加入
                        if (line.IndexOf("weekDay") >= 0 || line.IndexOf("grade") >= 0 || line.IndexOf("detail") >= 0
                            || line.IndexOf("note") >= 0 || line.IndexOf(" academicTeach") >= 0
                            || line.IndexOf("classNote") >= 0 || line.IndexOf("//隔几周") >= 0)
                        { }
                        else
                        {
                            text += line + "\r\n";
                        }
                        //一行一行读
                        line = reader2.ReadLine();
                    }

                }
                classtable[j] = text;  //改变字符串中的信息，即删除不需要的行
                                       // Console.WriteLine(classtable[j]);
                File.Delete("class.txt");
                j++;
            }
            return classtable;
        }
        //public void Match(string[] temp)//参数为课程表数组
        //{

        //    foreach (string c in temp)
        //    {
        //        Lesson newLesson = new Lesson();
        //        string[] t = new string[30];//临时存储单个课程的各项信息
        //        string pattern = " .*? ";
        //        Regex rx = new Regex(pattern);
        //        Match m = rx.Match(c);
        //        int i = 0;
        //        while (m.Success)
        //        {
        //            t[i] = m.Value;
        //            i++;
        //            m = rx.Match(c, m.Index + m.Length);
        //        }
        //        newLesson.LessonName = t[1];
        //        newLesson.Day = t[3];
        //        newLesson.BeginWeek = t[5];
        //        newLesson.EndWeek = t[7];
        //        newLesson.BeginTime = t[9];
        //        newLesson.EndTime = t[11];
        //        newLesson.ClassRoom = t[13];
        //        newLesson.TeacherName = t[15];
        //        newLesson.ProfessionName = t[17];
        //        newLesson.PlanType = t[19];
        //        newLesson.Credit = t[21];
        //        newLesson.AreaName = t[23];
        //        newLesson.WeekInterval = t[25];
        //        Lessons.Add(newLesson);
        //    }
        //}
        public void serializeMethod(List<Lesson> list)
        {
            using (FileStream fs = new FileStream("s.xml", FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, list);
            }
        }
        public List<Lesson> reserializeMethod(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                List<Lesson> list = (List<Lesson>)bf.Deserialize(fs);
                return list;
            }
        }
    }
}
