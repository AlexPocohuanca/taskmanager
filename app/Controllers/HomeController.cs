using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TaskManager.Models;
using static Azure.Core.HttpHeader;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TaskmanagerContext _context;

        public HomeController(ILogger<HomeController> logger, TaskmanagerContext context)
        {
            _logger = logger;
            _context = context;
        }

       
        public async Task<IActionResult> Index()
        {
            var tareas = await _context.Tareas.Include(t => t.IdListaNavigation).Include(t => t.IdResponsableNavigation).ToListAsync();
            var listas = await _context.Lista.ToListAsync();
            var responsables = await _context.Responsables.ToListAsync();


            ViewBag.Tareas = tareas;
            ViewBag.Listas = listas;
            ViewBag.Responsables = responsables;


            ViewBag.Message = TempData["Message"];
                
            

            return View();
        }

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