using System;
using System.Collections.Generic;

namespace BotV42.Models.DB
{
    public partial class DayOfSweek
    {
        /// <summary>
        /// Class from directory /Models/DB was generated using Entity framework. Method of this was database first.
        /// </summary>
        public DayOfSweek()
        {
            Scheduler = new HashSet<Scheduler>();
        }

        public int IdDay { get; set; }
        public string Name { get; set; }

        public ICollection<Scheduler> Scheduler { get; set; }
    }
}
