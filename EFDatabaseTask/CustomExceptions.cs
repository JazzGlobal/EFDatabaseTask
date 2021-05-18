using System;

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
    class StartTimeMustBeBeforeEndTimeException : Exception
    {
        public StartTimeMustBeBeforeEndTimeException()
        {

        }
        public StartTimeMustBeBeforeEndTimeException(string message) : base(message)
        {

        }
    }
    [Serializable]
    class ScheduledAppointmentDuringAnotherAppointment : Exception
    {
        public ScheduledAppointmentDuringAnotherAppointment()
        {

        }
        public ScheduledAppointmentDuringAnotherAppointment(string message) : base(message)
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
    [Serializable]
    class CustomerIDNotFoundException : Exception
    {
        public CustomerIDNotFoundException(int customerId) : base($"Could not find CustomerID: {customerId}")
        {

        }
    }
}

