using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CalendarUtilities
{
    public static class GoogleCalendarExecuter
    {
        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "Google Calendar API .NET Quickstart";

        public static IList ListEvents(GoogleAccountInfo info)
        {
            UserCredential credential;
            var AccountCredentialsJsonPath = @"D:\GOOGLE-PHOTOS-DATA\ACCOUNTCREDENTIALS\Accounts\Smach\credentials.json";
            using (var stream =  new FileStream(AccountCredentialsJsonPath, FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = @"D:\GOOGLE-PHOTOS-DATA\ACCOUNTCREDENTIALS\Accounts\Smach\token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });


            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            //request.TimeMin = DateTime.Now;
            //request.TimeMax = DateTime.Now.AddYears(-1);
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            var lst = new List<Event>();
            do
            {
                Events events = request.Execute();

                if (events.Items != null && events.Items.Count > 0)
                {
                    //foreach (var eventItem in events.Items)
                    //{
                    //    string when = eventItem.Start.DateTime.ToString();
                    //    if (String.IsNullOrEmpty(when))
                    //    {
                    //        when = eventItem.Start.Date;
                    //    }
                    //    Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                    //}
                    lst.AddRange(events.Items.ToList());
                }
                request.PageToken = events.NextPageToken;
            }
            while (!string.IsNullOrEmpty(request.PageToken));

            return lst;
        }
    }

    public class GoogleAccountInfo
    {

    }
}
