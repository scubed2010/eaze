using System;

namespace Interview.Green.Web.Scraper.Interfaces
{
    public interface IWebScrapeService
    {
        string GetUrlContent(string url);
        void StoreScrapeContent(string content, string requestedOn, Guid jobRequestId);
        string RetrieveScrapeContent(Guid jobRequestId);
    }
}