using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
namespace Test
{
    class LessonList
    {
        public List<Lesson> lessonList= new List<Lesson>();
        public void Match(string []temp)//参数为课程表数组
        {
           
            foreach(string c in temp)
            {
                Lesson newLesson = new Lesson();
                string[] t = new string[30];//临时存储单个课程的各项信息
                string pattern = " .*? ";
                Regex rx = new Regex(pattern);
                Match m = rx.Match(c);
                int i = 0;
                while (m.Success)
                {
                    t[i] = m.Value;
                    i++;
                    m = rx.Match(c, m.Index + m.Length);
                }
                newLesson.LessonName = t[1];
                newLesson.Day = t[3];
                newLesson.BeginWeek = t[5];
                newLesson.EndWeek = t[7];
                newLesson.BeginTime = t[9];
                newLesson.EndTime = t[11];
                newLesson.ClassRoom = t[13];
                newLesson.TeacherName = t[15];
                newLesson.ProfessionName = t[17];
                newLesson.PlanType = t[19];
                newLesson.Credit = t[21];
                newLesson.AreaName = t[23];
                newLesson.WeekInterval = t[25];
                lessonList.Add(newLesson);
            }
        }
    }
}
