using System;

namespace Interview.Green.Web.Scraper.Interfaces
{
    public interface IJobSchedulerService
    {
        Guid ScheduleScrapeJob(string url);
        bool IsJobCompete(Guid guid);
    }
}