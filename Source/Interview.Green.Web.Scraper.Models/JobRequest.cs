using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Green.Web.Scrapper.Models
{
    public class JobRequest
    {
        /// <summary>
        /// Url for website to be scraped
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// First instance of HTML element to return
        /// </summary>
        public string RequestedElement { get; set; }
    }
}
