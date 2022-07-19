using SendGrid;
using SendGrid.Helpers.Mail;

namespace Interactive
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());
            Interact.AcceptTerms();

        }

    }
    public class Interact
    {
        public static void AcceptTerms()
        {
            DialogResult dialogResult = MessageBox.Show("By clicking below, you have accepted the charges attached to using this Wowbudd", "Accept Terms of Use", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    //SendEmail().Wait();
                    SaveToDB("Yes wow");
                }
                catch (Exception)
                {

                }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }


        static async Task SendEmail()
        {
            //read data from a file stored from C drive/windows
            var info = GetModelAndClient();
            string clientName = info[0];
            var model = info[1];
            int minutes = 234;
            var apiKey = "";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("aliyu.abdullahi@equipmenthall.com", "Aliyu Abdullahi");
            var subject = $"WowBudd model {model} has been used recently";
            var to = new EmailAddress("harryalex9821@gmail.com", "Harry Alex");
            var plainTextContent = $"Wowbudd with model number: {model} was used for {minutes} minutes by {clientName}. This is an automated mail to alert the company of wowbudd usage, Please do not reply to this email ";
            var htmlContent = $"<strong>Wowbudd with model number: <b>{model}</b> was used for <b>{minutes}</b> minutes by <b>{clientName}</b>. This is an automated mail to alert the company of wowbudd usage, Please do not reply to this email  </strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            //Setup database next
        }
        static string[] GetModelAndClient()
        {
            try
            {
                int counter = 0;
                string[] info;
                info = new string[2];
                foreach (string line in System.IO.File.ReadLines(@"c:\Windows\Web\info.txt"))
                {
                    info[counter] = line;
                    counter++;
                }

                return info;
            }
            catch (IOException)
            {
                string[] info;
                info = new string[2];
                info[0] = "Can not find client name";
                info[1] = "Cannot find model number, please check if a file was deleted from the web folder of the wowbudd the wowbudd ";
                return info;
            }
        }
        static async void SaveToDB(string pen)
        {

        }

    }
}
