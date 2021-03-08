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
}

