using FridgeManagementSystemV2.Data;
using FridgeManagementSystemV2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FridgeManagementSystemV2.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Create Technician
        [HttpGet]
        public IActionResult CreateTechnician()
        {
            return View();
        }

        // POST: Create Technician
        [HttpPost]
        public async Task<IActionResult> CreateTechnician(Technician model)
        {
            if (ModelState.IsValid)
            {
                _context.Technicians.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
