using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiggerLinux.Authentification;
using DiggerLinux.Helpers;
using DiggerLinux.Models;
using DiggerLinux.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiggerLinux.Controllers
{
    [Authorize( AuthenticationSchemes = JwtBearerAuthentication.AuthenticationType )]
    [Route("[controller]")]
    public class SoftwareController : Controller
    {
        readonly DiggerService _diggerService;
        readonly ShellHelper _shellHelper;

        public SoftwareController(DiggerService diggerService, ShellHelper shellHelper)
        {
            _diggerService = diggerService;
            _shellHelper = shellHelper;
        }

        [Authorize]
        [HttpGet("GetListNameFileInDocker/{imageDockerName}")]
        public async Task<IActionResult> GetListNameFileInDocker(string imageDockerName)
        {
            string result = _shellHelper.Bash("getListNameFileInDocker.sh " + imageDockerName);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("Search")]
        public IActionResult SearchSoftware([FromBody] SearchViewModel model)
        {
            _diggerService.SearchData(model);

            return Ok("Request in progress");
        }

        [Authorize]
        [HttpPost("Install")]
        public async Task<IActionResult> InstallSoftware([FromBody] SoftwareViewModel model)
        {
            string result = _shellHelper.Bash("installOsintSoft.sh " + model.LinkProject + " " + model.Id);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("Uninstall/{softwareId}")]
        public async Task<IActionResult> UninstallSoftware(int softwareId)
        {
            string result = _shellHelper.Bash("uninstallOsintSoft.sh " + softwareId);
            return Ok(result);
        }
    }
}