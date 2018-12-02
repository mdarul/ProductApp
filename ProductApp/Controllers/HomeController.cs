using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProductApp.Controllers
{
    public class HomeController: Controller
    {
        [HttpGet()]
        public ViewResult Home()
        {
            return View();
        }
    }
}
