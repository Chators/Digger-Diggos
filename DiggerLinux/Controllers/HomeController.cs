using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DiggerLinux.Models;
using DiggerLinux.Helpers;

namespace DiggerLinux.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index( [FromBody] SearchViewModel model )
        {
            string result = ShellHelper.Bash("execDatasploit.sh " + model.Domain);
            return Ok(result);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
