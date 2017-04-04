using System;

namespace Interview.Green.Web.Scraper.Interfaces
{
    public interface IJobSchedulerService
    {
        Guid ScheduleScrapeJob(string url, string requestedElement);
        bool IsJobCompete(Guid guid);
    }
}