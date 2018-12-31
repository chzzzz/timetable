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
        private void CreateXML()
        {
            List<Lesson> crosslist = new List<Lesson>();
            XmlSerializer xmlser = new XmlSerializer(typeof(List<Lesson>));
            String xmlFileName = "s.xml";
            XmlSerialize(xmlser, xmlFileName, crosslist);
        }
        public static void XmlSerialize(XmlSerializer ser, string fileName, object obj)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            ser.Serialize(fs, obj);
            fs.Close();
        }
        public static List<Lesson> ReserializeMethod(string fileName)
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
