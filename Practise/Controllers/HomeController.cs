using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Practise.DataAccessLayer;
using Practise.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace P320Practise.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _dbContext.Categories.Where(x => x.IsMain && x.IsDeleted == false).ToListAsync();

            //Serilog, NLog
            _logger.LogInformation("Test logger");
            return View(new HomeViewModel
            {
                Categories = categories
            });
        }
    }
}
