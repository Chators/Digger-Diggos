using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DiggerLinux.Models;

namespace DiggerLinux.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    //FileName = "/bin/bash",
                    //Arguments = $"-c \"{escapedArgs}\"", 					 
                    FileName = "ping",
                    Arguments = $"localhost",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            Console.WriteLine(result);
            Console.WriteLine("zzz");
            process.WaitForExit();

            return View();
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
