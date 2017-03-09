using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using System.Diagnostics;


namespace Portfolio.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetProjectList()
        {
            return View("ProjectList", Github.GetTopRepos());
        }

        //public IActionResult GetReadme(string id)
        //{
        //    Debug.WriteLine("first check");
        //    return Content(Github.GetReadme(id));
        //}
    }
}