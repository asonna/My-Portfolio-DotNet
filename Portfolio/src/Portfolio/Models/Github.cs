using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp.Authenticators;
using System.Diagnostics;

namespace Portfolio.Models
{
    public class Github
    {
        public static List<Repo> GetTopRepos()
        {
            RestClient client = new RestClient("https://api.github.com");
            client.Authenticator = new HttpBasicAuthenticator("token", EnvironmentVariables.GithubToken);

            RestRequest request = new RestRequest("/users/asonna/starred?", Method.GET);
            request.AddHeader("User-Agent", "asonna");
            request.AddParameter("sort", "stars");
            request.AddParameter("order", "desc");

            RestResponse response = new RestResponse();

            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            return JsonConvert.DeserializeObject<List<Repo>>(response.Content);
        }

        public static string GetReadme(string projectName)
        {
            RestClient client = new RestClient("https://api.github.com");
            client.Authenticator = new HttpBasicAuthenticator("token", EnvironmentVariables.GithubToken);

            RestRequest request = new RestRequest($"/repos/asonna/{projectName}/readme", Method.GET);
            request.AddHeader("User-Agent", "asonna");
            request.AddHeader("Accept", "application/vnd.github.VERSION.html");

            RestResponse response = new RestResponse();

            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            return response.Content;
        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            TaskCompletionSource<IRestResponse> tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}