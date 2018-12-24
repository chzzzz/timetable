using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule
{
    [Serializable]
    public class Week
    {
        public string Name { set; get; }
        public int No { set; get; }
        public void setName()
        {
            Name = "第" + No + "周";
        }
    }
}
