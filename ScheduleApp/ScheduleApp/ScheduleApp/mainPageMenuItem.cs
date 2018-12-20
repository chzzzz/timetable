using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleApp
{

    public class mainPageMenuItem
    {
        public mainPageMenuItem(Type t)
        {
            TargetType = t;
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}