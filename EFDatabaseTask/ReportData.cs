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
        public List<customer> GetAllActiveCustomers()
        {
            List<customer> customerList = new List<customer>();
            customerList = (from customer in dbcontext.customers
                            where customer.active == true
                            select customer).ToList();
            return customerList;
        }
        /// <summary>
        /// Returns list of (appointment types, month, and year).
        /// </summary>
        /// <returns></returns>
        public List<(int, int, int)> GetUniqueAppointmentTypes()
        {
            List<(int, int, int)> list = new List<(int, int, int)>();
            var results = from apps in dbcontext.appointments
                          select new
                          {
                              apps.type,
                              apps.start.Month,
                              apps.start.Year
                          };
            // Lambda Usage: Used to properly group the query results into a readable format for the report.
            var grouped = results.GroupBy(a => new { a.Year, a.Month }); // Get distinct types grouped by year -> month
            foreach (var item in grouped)
            {
                list.Add((item.Distinct().Count(), item.First().Month, item.First().Year));
                
            }
            return list;
        }
    }
}
