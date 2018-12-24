using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
   public class Lesson
    {
        public string LessonName { get; set; }
        public string Day { get; set; }
        public string BeginWeek { get; set; }
        public string EndWeek { get; set; }        
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public string ClassRoom { get; set; }
        public string TeacherName { get; set; }
        public string ProfessionName { get; set; }//教师职称
        public string PlanType { get; set; }//课程类型
        public string Credit { get; set; }//课程学分
        public string AreaName { get; set; }//上课地点名称
        public string WeekInterval { get; set; }//每隔几周上一次课

    }
}
