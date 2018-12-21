using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
namespace Test
{
    class GetInformation
    {
        public string[] Get(string a)//返回一个课程表数组
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
                  classtable[j] = classtable[j].Replace("=", "").Replace("\"", "").Replace(";"," ").Replace("var","");//去掉等于号和引号
                  classtable[j] = classtable[j].Replace("lessonName", "").Replace("day", "").Replace("beginWeek", "");
                  classtable[j] = classtable[j].Replace("endWeek", "").Replace("beginTime", "").Replace("endTime", "");
                  classtable[j] = classtable[j].Replace("classRoom", "").Replace("teacherName", "").Replace("professionName", "");
                  classtable[j] = classtable[j].Replace("planType", "").Replace("credit", "").Replace("areaName", "").Replace("weekInterVal","");

                }
                catch (Exception e)
                {

                }
                StreamWriter sw = new StreamWriter("D:/class.txt", true, Encoding.UTF8);
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
                            || line.IndexOf("classNote") >= 0||line.IndexOf("//隔几周")>=0)
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
                File.Delete("D:/class.txt");
                j++;
            }
            return classtable;
        }
    }
}
