using Microsoft.AspNetCore.Mvc;
using FridgeManagementSystemV2.Data;
using FridgeManagementSystemV2.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FridgeManagementSystemV2.Controllers
{
    public class TechnicianController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TechnicianController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Technician Dashboard
        public async Task<IActionResult> Dashboard()
        {
            var faultReports = await _context.FaultReports
                .Include(fr => fr.Fridge)
                .ThenInclude(f => f.Customer)
                .ToListAsync();
            return View(faultReports);
        }

        // View all scheduled maintenance visits
        public async Task<IActionResult> ScheduledVisits()
        {
            var visits = await _context.MaintenanceVisits
                .Include(v => v.Fridge)
                .ThenInclude(f => f.Customer)
                .ToListAsync();
            return View(visits);
        }

        // View all fault reports
        public IActionResult ViewFaultReports()
        {
            var faultReports = _context.FaultReports
                .Include(fr => fr.Fridge)
                .Include(fr => fr.Fridge.Customer) // Include customer details
                .ToList();
            return View(faultReports);
        }

        // Create a new fault report (GET)
        public IActionResult CreateFaultReport()
        {
            ViewBag.Fridges = _context.Fridges.Include(f => f.Customer).ToList();
            ViewBag.Technicians = _context.Technicians.ToList();
            return View();
        }

        // Create a new fault report (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFaultReport(FaultReport faultReport)
        {
            if (ModelState.IsValid)
            {
                _context.FaultReports.Add(faultReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ViewFaultReports));
            }

            ViewBag.Fridges = _context.Fridges.Include(f => f.Customer).ToList();
            ViewBag.Technicians = _context.Technicians.ToList();
            return View(faultReport);
        }

        // Edit a fault report (GET)
        public IActionResult EditFaultReport(int id)
        {
            var faultReport = _context.FaultReports
                .Include(fr => fr.Fridge)
                .ThenInclude(f => f.Customer)
                .FirstOrDefault(fr => fr.ReportId == id);

            if (faultReport == null)
            {
                return NotFound();
            }

            ViewBag.Fridges = _context.Fridges.Include(f => f.Customer).ToList();
            ViewBag.Technicians = _context.Technicians.ToList();
            return View(faultReport);
        }

        // Edit a fault report (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFaultReport(FaultReport faultReport)
        {
            if (ModelState.IsValid)
            {
                _context.FaultReports.Update(faultReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ViewFaultReports));
            }

            ViewBag.Fridges = _context.Fridges.Include(f => f.Customer).ToList();
            ViewBag.Technicians = _context.Technicians.ToList();
            return View(faultReport);
        }

        // Delete a fault report (GET)
        public IActionResult DeleteFaultReport(int id)
        {
            var faultReport = _context.FaultReports.Find(id);
            if (faultReport == null)
            {
                return NotFound();
            }
            return View(faultReport);
        }

        // Delete a fault report (POST)
        [HttpPost, ActionName("DeleteFaultReport")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFaultReportConfirmed(int id)
        {
            var faultReport = await _context.FaultReports.FindAsync(id);
            if (faultReport != null)
            {
                _context.FaultReports.Remove(faultReport);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ViewFaultReports));
        }
    }
}
