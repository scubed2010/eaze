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

        [TestMethod]
        public void GetElementText_HasCorrectContent()
        {
            var content = "<header>some text</header><h1>Heading</h1><footer>some text</footer>";
            var result = _webScrapeService.GetFirstElementText("h1", content);

            Assert.IsTrue(result == "Heading");
        }
    }
}
