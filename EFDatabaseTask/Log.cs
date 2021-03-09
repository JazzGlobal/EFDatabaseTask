using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDatabaseTask.Logger
{
    public static class Log
    {
        public static bool LogEvent(string filename,string message)
        {
            try
            {
                string formatString = $"\n======================================\n {DateTime.Now}: {message} \n======================================\n";
                File.AppendAllText(filename, formatString);
                return true;
            } catch (Exception e)
            {
                return false;
            }
        }
    }
}
