using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader reader = new StreamReader("D:\\classtable.txt", Encoding.Default);
            string a = reader.ReadToEnd();
            GetInformation b = new GetInformation();//获取课程信息的对象
            string[] temp = b.Get(a);//保存一个学生所有课程的数组
            LessonList c = new LessonList();
            c.Match(temp);
               
        }      

    }
}
