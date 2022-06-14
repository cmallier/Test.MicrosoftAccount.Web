using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Test.MicrosoftAccount.Web.Models;

namespace Test.MicrosoftAccount.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            AuthenticationProperties properties = new AuthenticationProperties();
            //properties.RedirectUri = "/SignInSuccess";
            properties.RedirectUri = "/Home/SignInSuccess";
            //properties.UpdateTokenValue("", "");

            return Challenge(properties);
        }

        //[HttpGet("/signin-microsoft")]
        public IActionResult SignInSuccess()
        {
            return RedirectToAction("Index");
        }

        public IActionResult ReChallenge()
        {
            AuthenticationProperties properties = new AuthenticationProperties();
            //properties.RedirectUri = "/SignInSuccess";
            properties.RedirectUri = "/Home/SignInSuccess";

            return Challenge(properties);
        }


        public IActionResult SignOut(string signOutType)
        {
            if (signOutType == "app")
            {
                HttpContext.SignOutAsync().Wait();
            }
            if (signOutType == "all")
            {
                return Redirect("https://login.microsoftonline.com/common/oauth2/v2.0/logout");
            }

            return RedirectToAction("Index");
        }

        //[HttpGet("signout-callback-oidc")]
        //public IActionResult SignoutCallbackOidc
        //{

        //}

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
