using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1;
namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Lesson> lessons;
            xml xml = new xml();
            lessons = xml.reserializeMethod("s.xml");
        }
    }
}
