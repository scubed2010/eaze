using Interview.Green.Web.Scraper.Interfaces;
using Interview.Green.Web.Scraper.Service;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Green.Web.Scrapper.Service
{
    public class WebScrapeJob : IJob
    {
        IWebScrapeService _webScrapeService;

        public WebScrapeJob()
        {
            _webScrapeService = new WebScrapeService();
        }
        public void Execute(IJobExecutionContext context)
        {
            var jobUrl = context.JobDetail.JobDataMap.First(t => t.Key == "url").Value.ToString();
            var jobRequestedOn = context.JobDetail.JobDataMap.First(t => t.Key == "requestedOn").Value.ToString();

            var content = _webScrapeService.GetUrlContent(jobUrl);
            _webScrapeService.StoreScrapeContent(content, jobRequestedOn, new Guid(context.JobDetail.Key.Name));
        }
    }
}
