using System.Net.Http;

namespace ChildScheduler.Interfaces
{
    public interface IHttpService
    {
        HttpClient GetHttpClient();
        void SetAuthToken(string token);
    }
}