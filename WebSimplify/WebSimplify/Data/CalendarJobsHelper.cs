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
                try
                {
                    if (userPendingJob.JobMethod == CalendarJobMethodEnum.GoogleAPI)
                    {
                        SendViaGoogleApi(userPendingJob);
                    }
                    if (userPendingJob.JobMethod == CalendarJobMethodEnum.EMail)
                    {
                        SendICSFileViaEmail(userPendingJob);
                    }
                    if (userPendingJob.JobMethod == CalendarJobMethodEnum.DownloadICS)
                    {
                        DownloadICS(userPendingJob);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Instance.Error(ex);
                    userPendingJob.JobStatus = CalendarJobStatusEnum.Failed;
                    userPendingJob.UpdateDate = DateTime.Now;
                    db.DbGenericData.Update(userPendingJob);
                }
            }
        }

        private static void DownloadICS(CalendarJob userPendingJob)
        {
            var request = GenerateCalendarRequest(userPendingJob.MemoItemId);
            CalendarEventManager.DownloadCalendarFile(HttpContext.Current, request);
            CloseJob(userPendingJob);
        }

        private static void LoadApiSettings()
        {
            googleApiData = db.DbGenericData.GetGenericData<UserGoogleApiData>(new GoogleApDataSearchParameters { UserId = uId }).FirstOrDefault() ??
                                new UserGoogleApiData();
        }

        private static void SendICSFileViaEmail(CalendarJob userPendingJob)
        {
            var request = GenerateCalendarRequest(userPendingJob.MemoItemId);
            CalendarEventManager.SendCalendarByMail(request);
            CloseJob(userPendingJob);
        }

        private static CalendarRequest GenerateCalendarRequest(int memoId)
        {
            MemoItem memo = db.DbCalendar.Get(new CalendarSearchParameters { ID = memoId }).FirstOrDefault();
            LoggedUser user = db.DbAuth.GetUser(uId);
            var mailingSettings = db.DbGenericData.GetGenericData<SystemMailingSettings>(new GenericDataSearchParameters { }).First();
            return new CalendarRequest
            {
                FromEmail = mailingSettings.SystemEmailAddress,
                FromName = mailingSettings.SystemName,
                NetworkCredentialPassword = mailingSettings.NetworkCredentialPassword,
                NetworkCredentialUserName = mailingSettings.NetworkCredentialUserName,
                Subject = mailingSettings.EmailsGenericSubject,
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
            };
        }

        private static void SendViaGoogleApi(CalendarJob userPendingJob)
        {
            LoadApiSettings();

            if (googleApiData.HasData)
            {
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
                    },
                    GoogleDataStore = (IGoogleDataStore)db.DbGoogle
                };
                if (memo.RepeatEvery.HasValue && memo.RepeatEvery.Value != RepeatEvery.None)
                {
                    gar.CalendarEvent.Frequency = memo.RepeatEvery.GetDescription();
                    gar.CalendarEvent.FrequencyCount = 10;
                }
                GoogleCalendarExecuter.InsertGoogleAPIEvent(gar);
                CloseJob(userPendingJob);
            }
        }

        private static void CloseJob(CalendarJob userPendingJob)
        {
            userPendingJob.JobStatus = CalendarJobStatusEnum.Closed;
            userPendingJob.UpdateDate = DateTime.Now;
            db.DbGenericData.Update(userPendingJob);
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