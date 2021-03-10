using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFDatabaseTask
{
    public partial class Calendar : Form
    {
        public List<CalendarEvent> calendarEvents;
        public Calendar()
        {
            InitializeComponent();
        }
        private U07lyXEntities dbcontext = new U07lyXEntities();

        private void Calendar_Load(object sender, EventArgs e)
        {
            // Query the database for all appointments for the currently logged in user. For each of these appointments whose date hasn't occurred ... 
            // ... Shade the days accordingly. 
            calendarEvents = LoadEvents();
            foreach(CalendarEvent c_event in calendarEvents)
            {
                monthCalendar1.AddBoldedDate(c_event.startDate);    
            }
            monthCalendar1.UpdateBoldedDates();
        }
        public List<CalendarEvent> LoadEvents ()
        {
            List<CalendarEvent> events = new List<CalendarEvent>();
            var appointmentList = dbcontext.appointments.Where(appointment => appointment.user.userName == Login.CurrentLoggedInUser.userName).ToList();
            foreach (Model.appointment app in appointmentList)
            {
                events.Add(new CalendarEvent(app));
            }
            return events;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            var selectedDateEvents = from c_event in calendarEvents
                                     where c_event.startDate.ToString().Substring(0, 9) == e.Start.ToString().Substring(0, 9)
                                     select c_event;
            foreach(CalendarEvent c_event in selectedDateEvents)
            {
                Console.WriteLine(c_event.title);
            }

           //  Console.WriteLine(DateTime.Now.Date.ToString().Substring(0, 9));
        }
    }
    public class CalendarEvent 
    {
        public string customerName { get; }
        public string title { get; }
        public string description { get; }
        public DateTime startDate { get; }
        public DateTime endDate { get; }
        
        public CalendarEvent(Model.appointment appointment)
        {
            customerName = appointment.customer.customerName;
            title = appointment.title;
            description = appointment.description;
            startDate = appointment.start;
            endDate = appointment.end;
        }
    }
}
