using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using System.Diagnostics;
using static Portfolio.Models.GithubRepo;

namespace Portfolio.Models

{
    public static class Project
    {
        //Get Request
        public static List<GithubRepo> GetFirstThreeStar()
        {
            RestClient client = new RestClient("https://api.github.com/");
            RestRequest request = new RestRequest("users/asonna/repos?per_page=100&access_token=" + EnvironmentVariables.GithubToken);
            request.AddHeader("User-Agent", EnvironmentVariables.GithubTokenName);

            RestResponse response = new RestResponse();

            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();

            List<GithubRepo> repos = JsonConvert.DeserializeObject<List<GithubRepo>>(response.Content);
            List<GithubRepo> firstThree = FirstThree(repos);

            return firstThree;
        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            TaskCompletionSource<IRestResponse> tcs = new TaskCompletionSource<IRestResponse>();

            theClient.ExecuteAsync(theRequest, response =>
            {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }

        // Find First three starred repos
        private static List<GithubRepo> FirstThree(List<GithubRepo> repos)
        {

            GithubRepo first = null;
            GithubRepo second = null;
            GithubRepo third = null;

            foreach (GithubRepo repo in repos)
            {
                if (first == null)
                {
                    first = repo;
                }
                else if (repo.stargazers_count > first.stargazers_count)
                {
                    third = second;
                    second = first;
                    first = repo;
                }
                else if (second == null)
                {
                    second = repo;
                }
                else if (repo.stargazers_count == first.stargazers_count || repo.stargazers_count > second.stargazers_count)
                {
                    third = second;
                    second = repo;
                }
                else if (third == null)
                {
                    third = repo;
                }
                else if (repo.stargazers_count > third.stargazers_count)
                {
                    third = repo;
                }
            }

            List<GithubRepo> firstThree = new List<GithubRepo>()
            {
                first, second, third
            };

            return firstThree;
        }
    }
}