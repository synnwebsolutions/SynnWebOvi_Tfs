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
        public override int RepeatEvery => 60000 * 15 ; // in milliseconds - 1 minute * X
        private CalendarBackgroundWorkerLog workerLog;
        public IDatabaseProvider DBController { get; set; }
        private List<MemoItem> memoItems;

        public override void DoWork()
        {
            try
            {
                LoadLog();
                LoadItems();
                GenerateJobs();
                CloseLog();
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
            }
        }

        private void GenerateJobs()
        {
            foreach (var memo in memoItems)
            {
                var memoJobs = DBController.DbGenericData.GetGenericData<CalendarJob>(new CalendarJobSearchParameters { UserId = memo.UserId });
                if (memoJobs.IsEmpty())
                {
                    CreateNewJob(memo);
                }
                else
                {
                    foreach (var job in memoJobs)
                    {
                        if (job.JobStatus == CalendarJobStatusEnum.Failed)
                        {
                            if (job.JobMethod == CalendarJobMethodEnum.Google)
                            {
                                job.JobMethod = CalendarJobMethodEnum.EMail;
                                job.JobStatus = CalendarJobStatusEnum.Pending;
                                DBController.DbGenericData.Update(job);
                            }
                        }
                    }
                }
            }
        }

        private void CreateNewJob(MemoItem memo)
        {
            CalendarJob job = new CalendarJob();
            job.UserId = memo.UserId;
            job.MemoItemId = memo.Id;
            job.JobMethod = CalendarJobMethodEnum.Google;
            job.JobStatus = CalendarJobStatusEnum.Pending;
            job.Active = true;
            DBController.DbGenericData.Add(job);
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
            memoItems = DBController.DbCalendar.Get(new CalendarSearchParameters { FromDate = workerLog.LastRunTime });
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