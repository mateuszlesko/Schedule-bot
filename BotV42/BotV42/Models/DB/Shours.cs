using System;
using System.Collections.Generic;

namespace BotV42.Models.DB
{
    public partial class Shours
    {
        /// <summary>
        /// Class from directory /Models/DB was generated using Entity framework. Method of this was database first.
        /// </summary>
        public Shours()
        {
            Scheduler = new HashSet<Scheduler>();
        }

        public int IdHour { get; set; }
        public int? MinutesStart { get; set; } //minutes of specified hour when lessons start until 00:00
        public int? MinutesEnd { get; set; } //minutes of specified hour when lessons end until 00:00
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }

        public ICollection<Scheduler> Scheduler { get; set; }
    }
}
