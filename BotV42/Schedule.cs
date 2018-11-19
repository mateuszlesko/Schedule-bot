using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotV42.Models.DB;

namespace BotV42
{
    public class Schedule
    {
        /// <summary>
        /// Method  getAllSubjects gets all of School Subjects from class SchoolScheduleContext, 
        /// which was generated from Database using EnityFramework
        /// </summary>
        /// <returns>allSubjectString return a result of LINQ query. All elements save as string </returns>
        public string  getAllSubjects() {

            string allSubjectString="";

            using (var context = new SchoolScheduleContext()) {

                var AllSubjectGroup = context.Lessons.GroupBy(subjects => subjects.Name);

                foreach (var group in AllSubjectGroup) {

                    foreach (var subject in group) {
                        allSubjectString += subject.Name.ToString();
                        allSubjectString += "\n";
                    }
                }
            }
                return allSubjectString;
        }

        /// <summary>
        /// Method GetWhenStartsSchool gets when school start in current day. Data come form datbase and is stored by using LINQ in variable startHour. 
        /// IdDay stored a day of the week as a number.
        /// </summary>
        /// <returns>startHour return value of string which contain a result of LINQ query</returns>
        public string GetWhenStartSchool()
        {
            string startHour = "";
            int IdDay = getNumberDayOfWeek();

            //The numbers of days when children have to go to school is from 1 to 5. Number 0 is for the weekend.
            if (IdDay != 0)
            {
                using (var context = new SchoolScheduleContext())
                {

                    var query = (from s in context.Scheduler join h in context.Shours on s.IdFH equals h.IdHour
                                 where s.IdFDow == IdDay select new { hour = h.TimeStart }).First().hour.ToString();

                    startHour = $"Today school starts at {query} am";
                }

            }
            else {
                startHour = "Today you mustn't go to school";
            }

            return startHour;
        }
        /// <summary>
        /// Method GetWhenEndsSchool is constructed in a similar way as GetWhenStartSchool.
        /// Only diffrent is that the result of LINQ Query is a hour of end of school in current day
        /// </summary>
        /// <returns></returns>
        public string GetWhenEndSchool()
        {
            string startHour = "";
            int IdDay = getNumberDayOfWeek();

            if (IdDay != 0)
            {
                using (var context = new SchoolScheduleContext())
                {

                    var query = (from s in context.Scheduler
                                 join h in context.Shours on s.IdFH equals h.IdHour
                                 where s.IdFDow == IdDay
                                 select new { hour = h.TimeEnd }).Last().hour.ToString();

                    startHour = $"Today school ends at {query} pm";
                }

            }
            else
            {
                startHour = "Today you mustn't go to school";
            }

            return startHour;
        }
        /// <summary>
        /// Method getDailyPlan get a school schedule for current day of the week. In result of LINQ query is a list of subjects schedule data.  
        /// </summary>
        /// <returns>Variable dailyPlan contains a value of LINQ query</returns>
        public string getDailyPlan() {
            string dailyPlan = "";
            int numberDayOfWeek = getNumberDayOfWeek();
            if (numberDayOfWeek != 0)
            {
                using (var context = new SchoolScheduleContext())
                {

                    var query = (from l in context.Lessons
                                 join d in context.Scheduler
                                 on l.IdLesson equals d.IdFLssn
                                 join h in context.Shours
                             on d.IdFH equals h.IdHour
                                 join c in context.Classrooms on d.IdFClss equals c.IdClassroom
                                 where d.IdFDow == numberDayOfWeek
                                 select new { name = l.Name, startHour = h.TimeStart, endHour = h.TimeEnd, classroom = c.Nr }).ToList();


                    dailyPlan += DateTime.Now.DayOfWeek.ToString();
                    dailyPlan += ": \n";

                    foreach (var q in query)
                    {
                        dailyPlan += $"{q.startHour.ToString()} - {q.endHour.ToString()}  {q.name.ToString()}  (Cl:{q.classroom.ToString()})\n";
                    }
                }

            }
            else {
                dailyPlan = "Today you mustn't go to school";
            }
                return dailyPlan;
        }
       
        /// <summary>
        /// Function getWeeklyPlan contains a weekly schedule. Data comes from database and is querid using LINQ.
        /// </summary>
        /// <returns>return weeklyPlan as a string </returns>
        public string getWeeklyPlan()
        {
            string weeklyPlan = "";

            using (var context = new SchoolScheduleContext()) {

                var query = (from l in context.Lessons
                             join s in context.Scheduler
                             on l.IdLesson equals s.IdFLssn
                             join h in context.Shours
                              on s.IdFH equals h.IdHour
                             join c in context.Classrooms on s.IdFClss equals c.IdClassroom
                             
                             
                                 where s.IdFDow >= 1 && s.IdFDow <= 5
                                 
                                 select new { name = l.Name, NrDay = s.IdFDow, lessonStart = h.TimeStart, lessonEnd = h.TimeEnd, classroom = c.Nr }).ToList();
                 
                foreach (var q in query) {

                    switch (q.NrDay)
                    {
                        case 1:
                            weeklyPlan += "Monday|  ";
                            break;
                        case 2:
                            weeklyPlan += "Tuesday|  ";
                            break;
                        case 3:
                            weeklyPlan += "Wednesday| ";
                            break;
                        case 4:
                            weeklyPlan += "Thursday|  ";
                            break;
                        case 5:
                            weeklyPlan += "Friday|  ";
                            break;
                    }
                        

                    weeklyPlan += $"{q.lessonStart} - {q.lessonEnd}  {q.name}  (Cl:{q.classroom.ToString()})";
                    
                    weeklyPlan += "\n";
                }
                return weeklyPlan;
            }
            
        }

        /// <summary>
        /// Method getNumberDayOfWeek return a number of current day of the week as a number. 0 is value for the weekend
        /// </summary>
        /// <returns>return a number of current day</returns>
        private int getNumberDayOfWeek()
        {
            int number = 0;
            string dayName = DateTime.Now.DayOfWeek.ToString();
            switch (dayName) {
                case "Monday":
                    number = 1;
                    break;
                case "Tuesday":
                    number = 2;
                    break;
                case "Wednesday":
                    number = 3;
                    break;
                case "Thursday":
                    number = 4;
                    break;
                case "Friday":
                    number = 5;
                    break;
                default:
                    break;
            }
            return number;
        }
    }
}
