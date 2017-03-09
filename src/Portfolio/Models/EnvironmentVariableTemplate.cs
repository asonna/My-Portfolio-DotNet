using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class EnvironmentVariableTemplate
    {
        public static string GithubToken = "{{GITHUB-API-KEY}}";
        public static string GithubTokenName = "{{KEY-NAME}}";
    }
}
