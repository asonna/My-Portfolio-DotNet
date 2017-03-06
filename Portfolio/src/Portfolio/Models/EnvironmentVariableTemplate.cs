using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class EnvironmentVariableTemplate
    {
        public static string GithubKey = "{{GITHUB-API-KEY}}";
        public static string GithubKeyName = "{{KEY-NAME}}";
    }
}
