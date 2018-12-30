using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule
{
    [Serializable]
    public class Score
    {
        public string No { set; get; }
        public string Name { set; get; }
        public string TeacherName { set; get; }
        public string Type { set; get; }
        public string Year { set; get; }
        public string Term { set; get; }
        public string Grade { set; get; }
        public string Credit { set; get; }
    }
}
