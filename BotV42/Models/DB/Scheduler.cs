using System;
using System.Collections.Generic;

namespace BotV42.Models.DB
{
    /// <summary>
    /// Class from directory /Models/DB was generated using Entity framework. Method of this was database first.
    /// </summary>
    public partial class Scheduler
    {
        public int IdScheduler { get; set; }
        public int? IdFDow { get; set; } // ID day of the week
        public int? IdFLssn { get; set; } //ID Lesson
        public int? IdFH { get; set; } //Id hour
        public int? IdFClss { get; set; } //Id classroom

        public Classrooms IdFClssNavigation { get; set; }
        public DayOfSweek IdFDowNavigation { get; set; }
        public Shours IdFHNavigation { get; set; }
        public Lessons IdFLssnNavigation { get; set; }
    }
}
