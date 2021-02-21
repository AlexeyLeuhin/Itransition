using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using task5.Context;
using task5.Models;


namespace task5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ElementsContext _db;
        public HomeController(ILogger<HomeController> logger, ElementsContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
           
        [HttpGet]
        public IEnumerable<TextArea> GetTextAreas(){
            return _db.TextAreas.ToList();
        }

        [HttpGet]
        public IEnumerable<Text> GetTextes()
        {
            return _db.Textes.ToList();
        }

        [HttpGet]
        public IEnumerable<Line> GetLines()
        {
            return _db.Lines.ToList();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
