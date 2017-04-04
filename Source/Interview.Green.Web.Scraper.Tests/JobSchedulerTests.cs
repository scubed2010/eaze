using Interview.Green.Web.Scraper.Interfaces;
using Interview.Green.Web.Scraper.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace Interview.Green.Web.Scrapper.Tests
{
    [TestClass]
    public class JobSchedulerTests
    {
        IJobSchedulerService _jobSchedulerService;

        [TestInitialize]
        public void Initialize()
        {
            _jobSchedulerService = new JobSchedulerService();
        }

        [TestMethod]
        public void ScheduleJob_ConfirmCompletion()
        {
            var result = _jobSchedulerService.ScheduleScrapeJob("http://www.google.com", string.Empty);

            //sleep for 30 seconds
            Thread.Sleep(30000);

            Assert.IsTrue(_jobSchedulerService.IsJobCompete(result));
        }
    }
}
