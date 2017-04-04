using System;
using Interview.Green.Web.Scraper.Interfaces;
using Quartz;
using Quartz.Impl;
using Interview.Green.Web.Scrapper.Service;
using System.Configuration;

namespace Interview.Green.Web.Scraper.Service
{
    public class JobSchedulerService : IJobSchedulerService
    {
        ISchedulerFactory _schedFact;

        public JobSchedulerService()
        {
            _schedFact = new StdSchedulerFactory();

        }
        public Guid ScheduleScrapeJob(string url, string requestedElement)
        {
            var guid = Guid.NewGuid();

            IScheduler sched = _schedFact.GetScheduler();
            sched.Start();

            IJobDetail job = JobBuilder.Create<WebScrapeJob>()
                .WithIdentity(guid.ToString(), "g")
                .UsingJobData("url", url)
                .UsingJobData("requestedOn", DateTime.Now.ToLongDateString())
                .UsingJobData("requestedElement", requestedElement)
                .Build();
            
            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity($"trigger{guid.ToString()}", "g")
              .StartAt(DateBuilder.FutureDate(Convert.ToInt32(ConfigurationManager.AppSettings["ScheduleDelay"]), IntervalUnit.Second))
              .ForJob(job.Key)
              .Build();

            sched.ScheduleJob(job, trigger);

            return guid;
        }

        public bool IsJobCompete(Guid guid)
        {
            IScheduler sched = _schedFact.GetScheduler();

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