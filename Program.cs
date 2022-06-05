using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace SheetsQuickstart
{
    // Class to demonstrate the use of Sheets list values API
    class Program
    {
        /* Global instance of the scopes required by this quickstart.
         If modifying these scopes, delete your previously saved token.json/ folder. */
        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static string ApplicationName = "Google Sheets API .NET Quickstart";

        static void Main(string[] args)
        {
            GoogleCredential credential;
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(Scopes);
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });

            // Define request parameters.
            String spreadsheetId = "1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms";
            String range = "Class Data!A2:E";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                service.Spreadsheets.Values.Get(spreadsheetId, range);

            // Prints the names and majors of students in a sample spreadsheet:
            // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;
            if (values == null || values.Count == 0)
            {
                Console.WriteLine("No data found.");
                return;
            }
            Console.WriteLine("Name, Major");
            foreach (var row in values)
            {
                // Print columns A and E, which correspond to indices 0 and 4.
                Console.WriteLine("{0}, {1}", row[0], row[4]);
            }

            //    static void CreateEntry()
            //{
            //    String range = "Class Data!A:F";
            //    var valuerange = new ValueRange();

            //Definir valores em uma varíavel indexável 
            //    var objList = new List<object>() { "Hello", "This", "was", "iserted", "via", "C#" };

            // Setar os valores para o Google Apis 
            //    valuerange.Values = new List<IList<object>> { objList };

            //    var appendRequest = service.Spreadsheets.Values.Append(valuerange, spreadsheetId, range);
            //    appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            //    var appResponse = appendRequest.Execute();
            //}
        }
    }
}