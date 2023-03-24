using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BasicEducationDepartmentWeb.Helpers
{
    public class BEDLogger
    {

        private void Write(string folderName, string Message)
        {
            try
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\" + folderName + "\\" + DateTime.Now.ToString("yyyyMM")))
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\" + folderName + "\\" + DateTime.Now.ToString("yyyyMM"));

                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\" + folderName + "\\" + DateTime.Now.ToString("yyyyMM") + "\\log" + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                sw.WriteLine(DateTime.Now.ToString() + " : " + Message);
                sw.Close();
            }
            catch
            {
                throw new IOException("Please give permission to write on the disk.");
            }
        }

        /// <summary>
        /// Logs the exception with an option to add a message (object in json format, etc..).
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <param name="message">Useful message for debugging (object in json, user details, etc..).</param>
        /// <exception cref="IOException">No permission to read/write on the disk.</exception>
        public void LogException(Exception ex, string message = "") =>
            Write("_errorLogs", message + Environment.NewLine + "Exception: " + ex.ToString() +
                        Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);

        /// <summary>
        /// Logs an event (tracking of objects passed on the method, etc..).
        /// </summary>
        /// <param name="_event">Message to log.</param>
        /// <exception cref="IOException">No permission to read/write on the disk.</exception>
        public void LogEvents(string _event) =>
            Write("_eventLogs", _event + Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);

    }
}