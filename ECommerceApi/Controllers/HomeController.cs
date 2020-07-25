using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ECommerceApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ECommerceApi.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            var name = User.Claims.Where(c => c.Type == ClaimTypes.Name)
               .Select(c => c.Value).SingleOrDefault();
            return View();
        }
    }
}
