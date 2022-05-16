using ChildScheduler.Interfaces;
using ChildScheduler.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(HttpService))]
namespace ChildScheduler.Services
{
    public class HttpService : IHttpService
    {
        private HttpClient _httpClient;

        public HttpClient HttpClient
        {
            get { return _httpClient; }
            set { _httpClient = value; }
        }
        public string BaseUrl = "http://192.168.10.73:5001";
        //public string BaseUrl = "http://192.168.0.188:5001";
        public HttpService()
        {

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            HttpClient = new HttpClient(httpClientHandler);
            HttpClient.BaseAddress = new Uri($"{BaseUrl}/");
        }
        public HttpClient GetHttpClient()
        {
            return HttpClient;
        }
        public void SetAuthToken(string token)
        {
            HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

    }
}
