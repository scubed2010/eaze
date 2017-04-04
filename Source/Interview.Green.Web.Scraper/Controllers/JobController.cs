using Interview.Green.Web.Scraper.Interfaces;
using Interview.Green.Web.Scraper.Models;
using Interview.Green.Web.Scraper.Service;
using Interview.Green.Web.Scrapper.Models;
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

        [HttpPost]
        public async Task<IHttpActionResult> ScrapeUrl(JobRequest request)
        {
            var result = await Task.Run(() => _jobSchedulerService.ScheduleScrapeJob(request.Url, request.RequestedElement));

            return Ok(result);
        }

        [HttpGet]
        public async Task<IHttpActionResult> ScrapeUrlStatus(Guid requestId)
        {
            var isComplete = await Task.Run(() => _jobSchedulerService.IsJobCompete(requestId));
            var content = _webScrapeService.RetrieveScrapeContent(requestId);

            if (isComplete && !string.IsNullOrEmpty(content))
            {
                return Ok(content);
            }
            
            return Ok("Processing...");
        }
    }
}