using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
namespace Test
{
    class ImoptAndExport
    {
        public List<Lesson> temp = new List<Lesson>();//用来存储xml文件中的课程信息
        public void ImportToXml(List<Lesson> a)
        //将课程表list序列化为一个xml文件用以在本地保存课程表信息
        {
            XmlDocument xd = new XmlDocument();
            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer xz = new XmlSerializer(a.GetType());
                xz.Serialize(sw, a);               
                xd.LoadXml(sw.ToString());
                xd.Save("D:\\classtable.xml");
            }
        }
        public void ExportFromXml(ImoptAndExport a)
         //从本地的xml文件中导入课程表信息到list中
        {
            using (XmlReader reader = XmlReader.Create("D:\\classtable.xml"))
            {
                XmlSerializer xz = new XmlSerializer(a.temp.GetType());
                a.temp = (List<Lesson>)xz.Deserialize(reader);          
            }
        }
    }
}
