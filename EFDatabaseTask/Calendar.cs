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
            calendarEvents = LoadEvents(); // Query the database for all appointments for the currently logged in user.

            foreach (CalendarEvent c_event in calendarEvents)
            {
                monthCalendar1.AddBoldedDate(c_event.startDate);    
            }
            monthCalendar1.UpdateBoldedDates(); // Shade the days accordingly. 

        }
        public List<CalendarEvent> LoadEvents ()
        {
            List<CalendarEvent> events = new List<CalendarEvent>();
            var appointmentList = dbcontext.appointments.Where(appointment => appointment.user.userName == Login.CurrentLoggedInUser.userName).ToList();
            foreach (appointment app in appointmentList)
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
            eventResultBox.Text = "";
            if(selectedDateEvents.Count() > 0)
            {
                foreach (CalendarEvent c_event in selectedDateEvents)
                {
                    // Populate RichEditText box with event data.
                    eventResultBox.Text += $"Title: {c_event.title}\nCustomer Name: {c_event.customerName}\nStart: {c_event.startDate}\nEnd: {c_event.endDate}\nDescription: {c_event.description}\n\n";
                }
            } else
            {
                eventResultBox.Text = $"No scheduled events for {e.Start.ToString().Substring(0,9)}.";
            }      
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
