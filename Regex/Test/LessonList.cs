using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
namespace Test
{
    public class LessonList
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
               int j = 3;
               if(t[j]== "  ")
                {
                    j = 4;
                }            
                newLesson.LessonName = t[1];
                newLesson.Day = t[j];
                newLesson.BeginWeek = t[j+2];
                newLesson.EndWeek = t[j+4];
                newLesson.BeginTime = t[j+6];
                newLesson.EndTime = t[j+8];
                newLesson.ClassRoom = t[j+10];
                newLesson.TeacherName = t[j+12];
                newLesson.ProfessionName = t[j+14];
                newLesson.PlanType = t[j+16];
                newLesson.Credit = t[j+18];
                newLesson.AreaName = t[j+20];
                newLesson.WeekInterval = t[j+22];
                lessonList.Add(newLesson);
            }
        }
    }
}
