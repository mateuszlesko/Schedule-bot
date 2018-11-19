using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotV42
{
    public class CommandsHelper
    {
        /// <summary>
        /// Class show  commands for a bot.
        /// </summary>
        string name;
        string function;
        string[] AllCommandsArray = new string[] { "help = lists all of commands",
            "today = return today's day of week",
            "DailyPlan = return a list of daily school subjects",
            "WeeklyPlan = return a list of weekly school schedule",
            " GetWhenStartSchool = return a start lesson of current day",
            "GetWhenEndSchool = return an end lesson of current day"
        }; 
        
        public string SeeAllCommands() {
            string allOfCommandText = "";
            foreach (string s in AllCommandsArray) {
                allOfCommandText += s;
                allOfCommandText += "\n";
            }
            return allOfCommandText;
        }
    }
}
