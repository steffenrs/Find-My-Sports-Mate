using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace PresentationLayer
{
    public class Weekdays
    {
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }

        public override string ToString()
        {
            StringBuilder weeklyActivity = new StringBuilder();

            if (Monday == true)
                weeklyActivity.Append("Mo,");
            if (Tuesday == true)
                weeklyActivity.Append("Tu,");
            if (Wednesday == true)
                weeklyActivity.Append("We,");
            if (Thursday == true)
                weeklyActivity.Append("Th,");
            if (Friday == true)
                weeklyActivity.Append("Fr,");
            if (Saturday == true)
                weeklyActivity.Append("Sa,");
            if (Sunday == true)
                weeklyActivity.Append("Su,");

            // Remove last comma character
            if (weeklyActivity.Length > 0)
                weeklyActivity.Remove(weeklyActivity.Length - 1, 1);

            return weeklyActivity.ToString();
        }
    }
}