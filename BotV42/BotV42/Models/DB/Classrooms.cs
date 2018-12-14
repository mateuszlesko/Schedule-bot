using System;
using System.Collections.Generic;

namespace BotV42.Models.DB
{
    public partial class Classrooms
    {
        /// <summary>
        /// Class from directory /Models/DB was generated using Entity framework. Method of this was database first.
        /// </summary>
        public Classrooms()
        {
            Scheduler = new HashSet<Scheduler>();
        }

        public int IdClassroom { get; set; }
        public string Nr { get; set; }
        public int? Floor { get; set; }

        public ICollection<Scheduler> Scheduler { get; set; }
    }
}
