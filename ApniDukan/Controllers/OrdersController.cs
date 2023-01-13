using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ApniDukan.DatabaseIntegration;
using ApniDukan.Models;
using Newtonsoft.Json;
using ApniDukan.Common;

namespace ApniDukan.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApniDukanContext _context;

        public OrdersController(ApniDukanContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddToCart(Product product)
        {
            if (product == null)
                return Json(new { code = "0001", message = "Product cannot be empty.", productCount = 0 });

            if (SessionStorage.CartProducts is not null or { Count: 0 } && SessionStorage.CartProducts[User.GetEmailAddress()] is not null or { Count: 0 })
            {
                Product cartProduct = SessionStorage.CartProducts[User.GetEmailAddress()].FirstOrDefault(p => p.ProductID == product.ProductID);

                if (cartProduct != null)
                    cartProduct.Quantity++;
                else
                    SessionStorage.CartProducts[User.GetEmailAddress()].Add(product);
            }
            else
            {
                SessionStorage.CartProducts![User.GetEmailAddress()] = new List<Product> { product };
            }

            return Json(new { code = "0000", message = "Item added to cart.", productCount = SessionStorage.CartProducts[User.GetEmailAddress()].Count });
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var apniDukanContext = _context.Order.Include(o => o.Customer);
            return View(await apniDukanContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Address");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,CustomerID,CouponID,OrderDate,TotalAmount,NetAmount,DiscountAmount,CreatedOn,IsActive")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Address", order.CustomerID);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Address", order.CustomerID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("OrderID,CustomerID,CouponID,OrderDate,TotalAmount,NetAmount,DiscountAmount,CreatedOn,IsActive")] Order order)
        {
            if (id != order.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Address", order.CustomerID);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Order == null)
            {
                return Problem("Entity set 'ApniDukanContext.Order'  is null.");
            }
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(long id)
        {
            return _context.Order.Any(e => e.OrderID == id);
        }
    }
}
