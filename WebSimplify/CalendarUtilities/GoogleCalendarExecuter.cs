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
        static string[] Scopes = { CalendarService.Scope.Calendar};
        static string ApplicationName = "Google Calendar Executer";
        public const string IsraelDefaultTimeZone = "Asia/Jerusalem";
        static UserCredential credential;
        public static IList ListEvents(GoogleAccountRequest info)
        {
            Authenticate(info);
            CalendarService service = InitService();
            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            //request.TimeMin = DateTime.Now;
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
                    lst.AddRange(events.Items.ToList());
                }
                request.PageToken = events.NextPageToken;
            }
            while (!string.IsNullOrEmpty(request.PageToken));

            return lst;
        }

        private static CalendarService InitService()
        {
            // Create Google Calendar API service.
            return new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        public static void Insert(GoogleAccountRequest googleRequest)
        {
            Authenticate(googleRequest);
            CalendarService service = InitService();

            MyCalendarEvent myEvent = googleRequest.CalendarEvent;
            Event newEvent = new Event()
            {
                Summary = myEvent.SummaryText,
                Location = myEvent.LocationText,
                Description = myEvent.Description,
                Start = new EventDateTime()
                {
                    DateTime = myEvent.BeginDate,
                    TimeZone = IsraelDefaultTimeZone,
                },
                End = new EventDateTime()
                {
                    DateTime = myEvent.EndDate,
                    TimeZone = IsraelDefaultTimeZone,
                },
                //Recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=2" },
             
                Reminders = new Event.RemindersData()
                {
                    UseDefault = false,
                    Overrides = new EventReminder[] 
                    {
                        new EventReminder() { Method = "popup", Minutes = 24 * 60 },
                        new EventReminder() { Method = "popup", Minutes = 15 },
                        new EventReminder() { Method = "popup", Minutes = 3 * 60 },
                    }
                }
            };

            String calendarId = "primary";
            EventsResource.InsertRequest request = service.Events.Insert(newEvent, calendarId);
            Event createdEvent = request.Execute();
        }

        private static void Authenticate(GoogleAccountRequest info)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(info.CredentialsJsonString)))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None, new SqlDatabaseDatastore(info.DbConnectionString, info.DbTableName)).Result;
            }
        }
    }
}
