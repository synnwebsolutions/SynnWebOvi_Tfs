using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CalendarUtilities;
using SynnWebOvi;

namespace WebSimplify
{
    public static class CalendarJobsHelper
    {
        private static IDatabaseProvider db;
        private static int uId;
        private static UserGoogleApiData googleApiData;

        internal static void CheckPendingJobs(IDatabaseProvider DBController, int userId)
        {
            db = DBController;
            uId = userId;

            var userPendingJobs = DBController.DbGenericData.GetGenericData<CalendarJob>(new CalendarJobSearchParameters
                                                    { UserId = userId, CalendarJobStatus = CalendarJobStatusEnum.Pending });

            foreach (var userPendingJob in userPendingJobs)
            {
                if (userPendingJob.JobMethod == CalendarJobMethodEnum.Google)
                {
                    SendViaGoogleApi(userPendingJob);
                }
                if (userPendingJob.JobMethod == CalendarJobMethodEnum.EMail)
                {
                    SendICSFileViaEmail(userPendingJob);
                }
            }
        }

        private static void LoadApiSettings()
        {
            googleApiData = db.DbGenericData.GetGenericData<UserGoogleApiData>(new GoogleApDataSearchParameters { UserId = uId }).FirstOrDefault() ??
                                new UserGoogleApiData();
        }

        private static void SendICSFileViaEmail(CalendarJob userPendingJob)
        {
            MemoItem memo = db.DbCalendar.Get(new CalendarSearchParameters { ID = userPendingJob.MemoItemId }).FirstOrDefault();
            LoggedUser user = db.DbAuth.GetUser(uId);
            var mailingSettings = db.DbGenericData.GetGenericData<SystemMailingSettings>(new GenericDataSearchParameters { GenericDataEnum = GenericDataEnum.SystemMailingSettings }).First();
            CalendarEventManager.SendCalendarByMail(new CalendarRequest
            {
                FromEmail = mailingSettings.SystemEmailAddress,
                FromName = mailingSettings.SystemName,
                NetworkCredentialPassword = mailingSettings.NetworkCredentialPassword,
                NetworkCredentialUserName = mailingSettings.NetworkCredentialUserName,
                Subject = mailingSettings.EmailsGenericSubject ,
                To = new List<string> { user.EmailAdress },
                CalendarEvents = new List<MyCalendarEvent>
                {
                    new MyCalendarEvent
                    {
                        BeginDate = memo.Date,
                        EndDate = memo.Date.AddHours(1),
                        Details = memo.Description,
                        LocationText = memo.Display,
                        SummaryText = memo.title,
                    }
                }
            });
        }

        private static void SendViaGoogleApi(CalendarJob userPendingJob)
        {
            try
            {
                LoadApiSettings();
                MemoItem memo = db.DbCalendar.Get(new CalendarSearchParameters { ID = userPendingJob.MemoItemId }).FirstOrDefault();
                var gar = new GoogleAccountRequest
                {
                    CredentialsJsonString = googleApiData.GenerateJsonString(),
                    CalendarEvent = new MyCalendarEvent
                    {
                        BeginDate = memo.Date,
                        EndDate = memo.Date.AddHours(1),
                        Details = memo.Description,
                        LocationText = memo.Display,
                        SummaryText = memo.title,
                    }
                };
                if (memo.RepeatEvery.HasValue && memo.RepeatEvery.Value != RepeatEvery.None)
                {
                    gar.CalendarEvent.Frequency = memo.RepeatEvery.GetDescription();
                    gar.CalendarEvent.FrequencyCount = 10;
                }
                GoogleCalendarExecuter.InsertGoogleAPIEvent(gar);
                userPendingJob.JobStatus = CalendarJobStatusEnum.Closed;
                db.DbGenericData.Update(userPendingJob);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
                userPendingJob.JobMethod = CalendarJobMethodEnum.EMail;
                SendICSFileViaEmail(userPendingJob);
            }
        }

        private static void ListUserCalendarItemsViaGoogleAPI()
        {
            try
            {
                LoadApiSettings();
                db.DbGoogle.AppUserId = uId;

                var irs = GoogleCalendarExecuter.ListEvents(new GoogleAccountRequest
                {
                    CredentialsJsonString = googleApiData.GenerateJsonString(),
                    GoogleDataStore = (IGoogleDataStore)db.DbGoogle
                });
                var jstr = JSonUtills.ToJSonString(irs);

            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
            }
        }
    }
}