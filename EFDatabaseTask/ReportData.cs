using Model;
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
        public List<appointment> GetUserAppointments(Model.user user)
        {
            return dbcontext.appointments.Where(appointment => appointment.user.userName == user.userName).OrderBy(appointment => appointment.start).ToList();
        }

        public int GetUniqueAppointmentTypes()
        {
            return dbcontext.appointments.Select(appointment => appointment.type).Distinct().Count();
        }
    }
}
