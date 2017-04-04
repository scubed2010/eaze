using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interview.Green.Web.Scraper.Service;
using Interview.Green.Web.Scraper.Interfaces;

namespace Interview.Green.Web.Scrapper.Tests
{
    [TestClass]
    public class WebScraperTests
    {
        IWebScrapeService _webScrapeService;

        [TestInitialize]
        public void Initialize()
        {
            _webScrapeService = new WebScrapeService();
        }

        [TestMethod]
        public void GetUrlContent_HasContent_IfUrlIsValid()
        {
            var result = _webScrapeService.GetUrlContent("http://www.google.com");

            Assert.IsFalse(string.IsNullOrEmpty(result));
        }
    }
}
