using Interview.Green.Web.Scraper.Interfaces;
using Interview.Green.Web.Scraper.Models;
using Interview.Green.Web.Scraper.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Interview.Green.Web.Scraper.Controllers
{
    public class JobController : ApiController
    {
        IJobSchedulerService _jobSchedulerService;
        IWebScrapeService _webScrapeService;

        public JobController()
        {
            _jobSchedulerService = new JobSchedulerService();
            _webScrapeService = new WebScrapeService();
        }

        [HttpGet]
        public async Task<IHttpActionResult> ScrapeUrl(string url)
        {
            var result = _jobSchedulerService.ScheduleScrapeJob(url);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IHttpActionResult> ScrapeUrlStatus(Guid guid)
        {
            var isComplete = _jobSchedulerService.IsJobCompete(guid);

            if (isComplete)
            {
                var content = _webScrapeService.RetrieveScrapeContent(guid);
                return Ok(content);
            }
            
            return Ok("Is not yet complete!");
        }
    }
}