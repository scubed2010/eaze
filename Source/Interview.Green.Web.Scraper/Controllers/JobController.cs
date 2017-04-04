using Interview.Green.Web.Scraper.Interfaces;
using Interview.Green.Web.Scraper.Service;
using Interview.Green.Web.Scrapper.Models;
using System;
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
        /// <summary>
        /// Schedule url and optional selected first element for scraping
        /// </summary>
        /// <param name="request.Url">http://www.seattlesoftwaresolutions.com/About</param>
        /// <param name="request.RequestedElement">h1</param>
        /// <returns>Job ID as GUID</returns>
        [HttpPost]
        public async Task<IHttpActionResult> ScrapeUrl(JobRequest request)
        {
            var result = await Task.Run(() => _jobSchedulerService.ScheduleScrapeJob(request.Url, request.RequestedElement));

            return Ok(result);
        }

        /// <summary>
        /// Returns processing until content is available where it will return JobResult object at JSON
        /// </summary>
        /// <param name="requestId">GUID value returned from Quartz scheduling application</param>
        /// <returns>Status string or JobResult object as JSON </returns>
        [HttpGet]
        public async Task<IHttpActionResult> ScrapeUrlStatus(Guid requestId)
        {
            var isComplete = await Task.Run(() => _jobSchedulerService.IsJobCompete(requestId));
            var content = await Task.Run(() => _webScrapeService.RetrieveScrapeContent(requestId));

            if (isComplete && !string.IsNullOrEmpty(content))
            {
                return Ok(content);
            }
            
            return Ok("Processing...");
        }
    }
}