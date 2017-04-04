using System;
using Interview.Green.Web.Scraper.Interfaces;
using Quartz;
using Quartz.Impl;
using Interview.Green.Web.Scrapper.Service;

namespace Interview.Green.Web.Scraper.Service
{
    public class JobSchedulerService : IJobSchedulerService
    {
        public Guid ScheduleScrapeJob(string url)
        {
            var guid = Guid.NewGuid();
            
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();
            
            IJobDetail job = JobBuilder.Create<WebScrapeJob>()
                .WithIdentity(guid.ToString(), "g")
                .UsingJobData("url", url)
                .UsingJobData("requestedOn", DateTime.Now.ToLongDateString())
                .Build();
            
            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity("t", "g")
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInSeconds(40))
              .Build();

            sched.ScheduleJob(job, trigger);

            return guid;
        }

        public bool IsJobCompete(Guid guid)
        {
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            
            IScheduler sched = schedFact.GetScheduler();

            var executingJobs = sched.GetCurrentlyExecutingJobs();
            foreach (var job in executingJobs)
            {
                if (job.JobDetail.Key.Name == guid.ToString())
                {
                    return false;
                }
            }

            return true;
        }
    }
}