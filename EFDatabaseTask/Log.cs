using System;
using System.IO;

namespace EFDatabaseTask.Logger
{
    public static class Log
    {
        public static bool LogEvent(string filename, string message)
        {
            try
            {
                string formatString = $"\n======================================\n {DateTime.Now}: {message} \n======================================\n";
                File.AppendAllText(filename, formatString);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
