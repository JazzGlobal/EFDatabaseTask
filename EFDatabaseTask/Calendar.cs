using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EFDatabaseTask
{
    public partial class Calendar : Form
    {
        public Calendar()
        {
            InitializeComponent();
        }
        private U07lyXEntities dbcontext = new U07lyXEntities();

        private void Calendar_Load(object sender, EventArgs e)
        {
            timePeriodSelection.Items.Add(AppointmentLookupType.Day);
            timePeriodSelection.Items.Add(AppointmentLookupType.Week);
            timePeriodSelection.Items.Add(AppointmentLookupType.Month);
            timePeriodSelection.SelectedIndex = 0;
        }
        enum AppointmentLookupType
        {
            Day = 0,
            Week = 1, 
            Month = 2
        }
        private List<appointment> GetAppointments (DateTime dt_obj, AppointmentLookupType a_type)
        {
            var startDayOfWeek = DayOfWeek.Monday; 
            List<appointment> appointments = new List<appointment>();
            switch(a_type)
            {
                case AppointmentLookupType.Day:
                    appointments = (from app in dbcontext.appointments
                                    where app.start.Month == dt_obj.Month && app.start.Day >= dt_obj.Day
                                     && app.end.Month == dt_obj.Month && app.end.Day <= dt_obj.Day 
                                     && app.start.Year == dt_obj.Year
                                    select app).ToList();
                    break;
                case AppointmentLookupType.Week:
                    int diff = (7 + (dt_obj.DayOfWeek - startDayOfWeek)) % 7;
                    DateTime startOfWeekDtObj = dt_obj.AddDays(-1 * diff);
                    DateTime endOfWeekDtObj = dt_obj.AddDays(7);
                    appointments = (from app in dbcontext.appointments
                                    where app.start.Month == startOfWeekDtObj.Month && app.start.Day >= startOfWeekDtObj.Day  
                                    && app.end.Month == endOfWeekDtObj.Month && app.end.Day <= endOfWeekDtObj.Day
                                    && app.start.Year == startOfWeekDtObj.Year && app.end.Year == endOfWeekDtObj.Year
                                    select app).ToList();
                    break;
                case AppointmentLookupType.Month:
                     appointments = (from app in dbcontext.appointments
                                        where app.start.Month == dt_obj.Month  && app.start.Year == dt_obj.Year
                                        select app).ToList();
                    break; 
            }
            appointments = appointments.OrderBy(appointment => appointment.start).ToList();
            return appointments;
        }
        private void viewButton_Click(object sender, EventArgs e)
        {
            List<appointment> apps = GetAppointments(dateTimePicker.Value, (AppointmentLookupType) timePeriodSelection.SelectedItem);
            eventResultBox.Text = "";
            foreach (var app in apps)
            {
                eventResultBox.Text += $"{app.start} - {app.end}\n";
            }
        }
    }
}
