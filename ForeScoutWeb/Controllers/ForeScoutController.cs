using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ForeScoutWeb.Models;
using ForeScoutWrapper;
using ForeScoutWrapper.Data;

namespace ForeScoutWeb.Controllers
{
    public class ForeScoutController : Controller
    {
        private readonly ILogger<ForeScoutController> _logger;
        private readonly IForeScoutWrapper _wrapperClient;

        public ForeScoutController(ILogger<ForeScoutController> logger,IForeScoutWrapper wrapperClient)
        {
            _logger = logger;
            _wrapperClient=wrapperClient;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ForeScoutObject(){

            ForeScoutDataObject fsdo = _wrapperClient.GetForeScoutDataObjectFromOITSystem();
            
            ViewData["Property1"]=fsdo.Property1;
            ViewData["Property2"]=fsdo.Property2;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
