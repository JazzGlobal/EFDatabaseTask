using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFDatabaseTask
{
    public class ReportData
    {
        private U07lyXEntities dbcontext
        {
            get { return new U07lyXEntities(); }
        }
        /// <summary>
        /// Queries the appointment table for all appointments from current day onward, order by appointment start.
        /// </summary>
        /// <param name="user">User whose appointments are desired.</param>
        /// <returns></returns>
        public List<appointment> GetUserAppointments(Model.user user)
        {
            // Return all appointments from current day onward, ordered by appointment start. This makes iterating in the reports easier.
            return dbcontext.appointments
                .Where(appointment => 
            appointment.user.userName == user.userName 
            && appointment.start.Month == DateTime.Now.Month
            && appointment.start.Day >= DateTime.Now.Day)
                .OrderBy(appointment => appointment.start)
                .ToList();
        }

        /// <summary>
        /// Returns integer value denoting the total unique types of appointments in the current database.
        /// </summary>
        /// <returns></returns>
        public int GetUniqueAppointmentTypes()
        {
            return dbcontext.appointments.Select(appointment => appointment.type).Distinct().Count();
        }
    }
}
