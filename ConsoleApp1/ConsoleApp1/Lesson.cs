using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace App1
{
    [Serializable]
    public class Lesson
    {
        public string LessonName { get; set; }
        public string Day { get; set; }
        public string BeginWeek { get; set; }
        public string EndWeek { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public string Place { get; set; }
        public string Classroom { get; set; }
        public string WeekInterVal { get; set; }
        public string TeacherName { get; set; }
        public string ProfessionName { get; set; }
        public string Credit { get; set; }
    }
    class xml
    {
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
