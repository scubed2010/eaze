using System;
using Interview.Green.Web.Scraper.Interfaces;
using System.Net;
using System.IO;
using Amazon.S3;
using Amazon.S3.Model;
using System.Configuration;
using Newtonsoft.Json;
using Interview.Green.Web.Scraper.Models;

namespace Interview.Green.Web.Scraper.Service
{
    public class WebScrapeService : IWebScrapeService
    {
        private IAmazonS3 _AWSClient;

        public WebScrapeService()
        {
            _AWSClient = new AmazonS3Client(
                ConfigurationManager.AppSettings["AWSAccessKeyId"], 
                ConfigurationManager.AppSettings["AWSSecretKey"],
                Amazon.RegionEndpoint.USWest2);
        }
        public string GetUrlContent(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    HttpStatusCode statusCode = ((HttpWebResponse)response).StatusCode;
                    return reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                //log error
                return string.Empty;
            }
        }

        public string GetFirstElementText(string element, string content)
        {
            var elementText = content.Split(new string[] { $"<{element}>" }, StringSplitOptions.RemoveEmptyEntries)[1];
            elementText = elementText.Split(new string[] { $"</{element}>" }, StringSplitOptions.RemoveEmptyEntries)[0];
            return elementText;
        }

        public void StoreScrapeContent(string content, string requestedOn, Guid jobRequestId)
        {
            var jsonString = JsonConvert.SerializeObject(
                new JobResult
                {
                    Id = jobRequestId.ToString(),
                    RequestedOn = requestedOn,
                    Content = content
                });

            PutObjectRequest putRequest = new PutObjectRequest
            {
                BucketName = ConfigurationManager.AppSettings["S3Bucket"],
                Key = $"{jobRequestId.ToString()}.json",
                ContentBody = jsonString,
                CannedACL = S3CannedACL.PublicRead
            };

            _AWSClient.PutObject(putRequest);
        }

        public string RetrieveScrapeContent(Guid jobRequestId)
        {
            try
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = ConfigurationManager.AppSettings["S3Bucket"],
                    Key = $"{jobRequestId.ToString()}.json"
                };

                GetObjectResponse response = _AWSClient.GetObject(request);

                StreamReader reader = new StreamReader(response.ResponseStream);

                return reader.ReadToEnd();
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }
    }
}