using Microsoft.AspNetCore.Mvc;
using FridgeManagementSystemV2.Data;
using FridgeManagementSystemV2.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FridgeManagementSystemV2.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customer Dashboard
        public async Task<IActionResult> Dashboard()
        {
            var customerId = GetCurrentCustomerId(); // Implement this method based on your authentication logic
            var fridges = await _context.Fridges
                .Include(f => f.FaultReports)
                .Where(f => f.CustomerId == customerId)
                .ToListAsync();
            return View(fridges);
        }
        // GET: Add Fridge
        public IActionResult AddFridge()
        {
            return View();
        }

        // POST: Add Fridge
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFridge(Fridge fridge)
        {
            if (ModelState.IsValid)
            {
                // Assuming the customer is already logged in and we can get their ID
                fridge.CustomerId = GetCurrentCustomerId();  // Set customer ID

                _context.Fridges.Add(fridge);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Dashboard)); // Redirect back to Customer Dashboard
            }
            return View(fridge);
        }


        private int GetCurrentCustomerId()
        {
            // Your logic to get the logged-in customer's ID
            // Placeholder: Replace this with actual logic
            return 1;
        }

        // View upcoming maintenance visits
        public async Task<IActionResult> UpcomingVisits()
        {
            var customerId = GetCurrentCustomerId();
            var visits = await _context.MaintenanceVisits
                .Include(v => v.Fridge)
                .Where(v => v.Fridge.CustomerId == customerId && v.MainDate > DateTime.Now)
                .ToListAsync();
            return View(visits);
        }

        // View fridge history
        public async Task<IActionResult> FridgeHistory(int fridgeId)
        {
            var history = await _context.FaultReports
                .Where(fr => fr.FridgeId == fridgeId)
                .ToListAsync();
            return View(history);
        }

        // Customer management actions
        public IActionResult Details(int id)
        {
            var customer = _context.Customers
                .Include(c => c.Fridges)
                .ThenInclude(f => f.FaultReports) // Include fault reports for each fridge
                .FirstOrDefault(c => c.CustomerId == id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Dashboard));
            }

            return View(customer);
        }

        public IActionResult Edit(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Update(customer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Dashboard));
            }
            return View(customer);
        }

        public IActionResult Delete(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Dashboard));
        }
    }
}
