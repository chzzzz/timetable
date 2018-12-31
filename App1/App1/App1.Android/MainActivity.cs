using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Xml;
using System.IO;

namespace App1.Droid
{
    [Activity(Label = "App1", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        void xml()
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "GB2312", null);
            doc.AppendChild(dec);
            //创建一个根节点（一级）
            XmlElement cla = doc.CreateElement("Class");
            doc.AppendChild(cla);
            cla.SetAttribute("name", "");
            XmlElement teacher = doc.CreateElement("Teacher");
            cla.AppendChild(teacher);
            teacher.SetAttribute("tname", "");
            teacher.SetAttribute("ttitle", "");
            XmlElement time = doc.CreateElement("Time");
            cla.AppendChild(time);
            time.SetAttribute("dayofweek", "");
            time.SetAttribute("week", "");
            time.SetAttribute("lesson", "");
            XmlElement place = doc.CreateElement("Place");
            place.SetAttribute("classroom", "");
            doc.Save(@"d:\bb.xml");
            Console.Write(doc.OuterXml);
        }
    }
    public class Test
    {
        public static void Main()
        {
            // 创建一个新的测试对象
            TestSimpleObject obj = new TestSimpleObject();

            Console.WriteLine("Before serialization the object contains: ");
            obj.Print();

            // 创建一个文件"data.xml"并将对象序列化后存储在其中
            Stream stream = File.Open("data.xml", FileMode.Create);
            SoapFormatter formatter = new SoapFormatter();
            //BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(stream, obj);
            stream.Close();

            // 将对象置空
            obj = null;

            // 打开文件"data.xml"并进行反序列化得到对象
            stream = File.Open("data.xml", FileMode.Open);
            formatter = new SoapFormatter();
            //formatter = new BinaryFormatter();

            obj = (TestSimpleObject)formatter.Deserialize(stream);
            stream.Close();

            Console.WriteLine("");
            Console.WriteLine("After deserialization the object contains: ");
            obj.Print();
        }
    }

    // 一个要被序列化的测试对象的类
    [Serializable()]
    public class TestSimpleObject
    {
        public int member1;
        public string member2;
        public string member3;
        public double member4;

        // 标记该字段为不可被序列化的
        [NonSerialized()] public string member5;

        public TestSimpleObject()
        {
            member1 = 11;
            member2 = "hello";
            member3 = "hello";
            member4 = 3.14159265;
            member5 = "hello world!";
        }

        public void Print()
        {
            Console.WriteLine("member1 = '{0}'", member1);
            Console.WriteLine("member2 = '{0}'", member2);
            Console.WriteLine("member3 = '{0}'", member3);
            Console.WriteLine("member4 = '{0}'", member4);
            Console.WriteLine("member5 = '{0}'", member5);
        }
    }
}