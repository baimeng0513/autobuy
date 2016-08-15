using autobuy.service.Jobs;
using Common.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace autobuy.service
{
    public partial class TaskService : ServiceBase
    {
        private ILog log = LogManager.GetLogger(typeof(TaskService));
        ISchedulerFactory schedFact = new StdSchedulerFactory();
        IScheduler sched = null;
        public TaskService()
        {

        }

        public void Start()
        {
            sched = schedFact.GetScheduler();
            sched.Start();
            createMasterJob();

        }
         
        private void createMasterJob()
        {
            if (sched != null)
            {
                IJobDetail job = JobBuilder.Create<TaskJob>()
                .WithIdentity("MasterJob")
                .Build();
                ITrigger trigger = null;
                trigger = TriggerBuilder.Create()
                            .WithIdentity("MasterJob")
                            .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(9, 30))
                            .ForJob(job)
                            .Build();
                sched.ScheduleJob(job, trigger);
            }
        }

        protected override void OnStop()
        {
            if (sched != null)
            {
                sched.Shutdown();
            }
        }
    }
}
