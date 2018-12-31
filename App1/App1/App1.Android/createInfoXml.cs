using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Myxs.Application.port.XmlModels
{
    [XmlRoot(ElementName = "apas_info")]
    public class createInfoXml
    {
        [XmlElement(ElementName = "lessonName")]
        public string LessonName { get; set; }
        [XmlElement(ElementName = "day")]
        public string Day { get; set; }
        [XmlElement(ElementName = "beginWeek")]
        public string BeginWeek { get; set; }
        [XmlElement(ElementName = "endWeek")]
        public string EndWeek { get; set; }
        [XmlElement(ElementName = "beginTime")]
        public string BeginTime { get; set; }
        [XmlElement(ElementName = "endTime")]
        public string EndTime { get; set; }
        [XmlElement(ElementName = "place")]
        public string Place { get; set; }
        [XmlElement(ElementName = "classroom")]
        public string Classroom { get; set; }
        [XmlElement(ElementName = "weekInterVal")]//每几周上一次课
        public string WeekInterVal { get; set; }
        [XmlElement(ElementName = "teacherName")]
        public string TeacherName { get; set; }
        [XmlElement(ElementName = "professionName")]//教师职称
        public string ProfessionName { get; set; }
        [XmlElement(ElementName = "credit")]
        public string Credit { get; set; }
        
    }
}