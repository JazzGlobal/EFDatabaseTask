using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDatabaseTask.DataFormExceptions
{
    [Serializable]
    class InvalidDatabaseItemsException : Exception
    {
        public InvalidDatabaseItemsException()
        {

        }
        public InvalidDatabaseItemsException(string message) : base($"The following items were invalid: {message}")
        {

        }
    }
    [Serializable]
    class ScheduledAppointmentOutsideOfBusinessHoursException : Exception
    {
        public ScheduledAppointmentOutsideOfBusinessHoursException()
        {

        }
        public ScheduledAppointmentOutsideOfBusinessHoursException(string message) : base(message)
        {

        }
    }
    [Serializable]
    class MismatchingCredentialsException : Exception
    {
        public MismatchingCredentialsException()
        {

        }
        public MismatchingCredentialsException(string message) : base(message)
        {

        }
    }
    [Serializable]
    class UserNotFoundException : Exception
    {
        public UserNotFoundException(string userName) : base($" {Properties.Resources.UserNotFoundExceptionMessage} {userName}")
        {

        }
    }
}

