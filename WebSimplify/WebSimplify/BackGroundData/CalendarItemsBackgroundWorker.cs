using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SynnCore.DataAccess;
using SynnWebOvi;
using WebSimplify.Data;

namespace WebSimplify.BackGroundData
{
    public class CalendarItemsBackgroundWorker : BackgroundWorkerBase
    {
        public override int RepeatEvery //=> 60000 * 15 ; // in milliseconds - 1 minute * X
        {
            get
            {
                var minutes = 15;
#if DEBUG
                minutes = 1;
#endif
                return 60000 * minutes;
            }
        }
        private CalendarBackgroundWorkerLog workerLog;
        public IDatabaseProvider DBController { get; set; }
        private List<MemoItem> memoItems;

        public override void DoWork()
        {
            try
            {
                LoadLog();
                LoadItems();
                GenerateNewJobs();
                HandleExistingJobs();
                CloseLog();
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
            }
        }

        private void HandleExistingJobs()
        {
            var memoJobs = DBController.DbGenericData.GetGenericData<CalendarJob>(new CalendarJobSearchParameters {});
            foreach (var job in memoJobs)
            {
                if (job.JobStatus == CalendarJobStatusEnum.Failed)
                {
                    if (job.JobMethod == CalendarJobMethodEnum.GoogleAPI)
                    {
                        job.JobMethod = CalendarJobMethodEnum.EMail;
                    }
                    if (job.JobMethod == CalendarJobMethodEnum.EMail)
                    {
                        job.JobMethod = CalendarJobMethodEnum.DownloadICS;
                    }
                    job.JobStatus = CalendarJobStatusEnum.Pending;
                    job.UpdateDate = DateTime.Now;
                    DBController.DbGenericData.Update(job);
                }
            }
        }

        private void GenerateNewJobs()
        {
            foreach (var memo in memoItems)
            {
                var memoJobs = DBController.DbGenericData.GetGenericData<CalendarJob>(new CalendarJobSearchParameters { MemoId = memo.Id });
                if (memoJobs.IsEmpty())
                {
                    CreateNewJob(memo);
                }
            }
        }

        private void CreateNewJob(MemoItem memo)
        {
            CalendarJob job = new CalendarJob();
            job.UserId = memo.UserId;
            job.MemoItemId = memo.Id;
            job.JobMethod = CalendarJobMethodEnum.GoogleAPI;
            job.JobStatus = CalendarJobStatusEnum.Pending;
            job.Active = true;
            DBController.DbGenericData.Add(job);
            ShareMemo(memo, job);
        }

        private void ShareMemo(MemoItem memo, CalendarJob job)
        {
            if (memo.Shared)
            {
                UserMemoSharingSettings sharingSettings = DBController.DbGenericData.GetGenericData<UserMemoSharingSettings>
                    (new UserMemoSharingSettingsSearchParameters { OwnerUserId = memo.UserId }).FirstOrDefault();
                if (sharingSettings != null)
                {
                    foreach (var sharedUser in sharingSettings.UsersToShare)
                    {
                        job.UserId = sharedUser;
                        DBController.DbGenericData.Add(job);
                    }
                }
            }
        }

        private void LoadItems()
        {
            memoItems = DBController.DbCalendar.Get(new CalendarSearchParameters { FromCreationDate = workerLog.LastRunTime });
        }

        private void LoadLog()
        {
            workerLog = DBController.DbGenericData.GetGenericData<CalendarBackgroundWorkerLog>(new GenericDataSearchParameters { GenericDataEnum = GenericDataEnum.CalendarBackgroundWorkerLog }).FirstOrDefault();
            if (workerLog == null)
            {
                DBController.DbGenericData.Add(new CalendarBackgroundWorkerLog { LastRunTime = DateTime.Now.AddYears(-3) });
                workerLog = DBController.DbGenericData.GetGenericData<CalendarBackgroundWorkerLog>(new GenericDataSearchParameters()).FirstOrDefault();
            }
        }

        private void CloseLog()
        {
            workerLog.LastRunTime = DateTime.Now;
            DBController.DbGenericData.Update(workerLog);
        }
    }

}