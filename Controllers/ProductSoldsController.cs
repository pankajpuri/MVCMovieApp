using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCMovieApp.Data;
using MVCMovieApp.Models;

namespace MVCMovieApp.Controllers
{
    public class ProductSoldsController : Controller
    {
        private readonly MVCMovieAppDBContext _context;

        public ProductSoldsController(MVCMovieAppDBContext context)
        {
            _context = context;
        }

        // GET: ProductSolds
        public async Task<IActionResult> Index()
        {
            var mVCMovieAppDBContext = _context.ProductSold.Include(p => p.Customers).Include(p => p.Products).Include(p => p.Stores);
            return View(await mVCMovieAppDBContext.ToListAsync());
        }

        // GET: ProductSolds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSold = await _context.ProductSold
                .Include(p => p.Customers)
                .Include(p => p.Products)
                .Include(p => p.Stores)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productSold == null)
            {
                return NotFound();
            }

            return View(productSold);
        }

        // GET: ProductSolds/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.cutomers, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id");
            ViewData["StoreId"] = new SelectList(_context.Store, "Id", "Id");
            return View();
        }

        // POST: ProductSolds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,CustomerId,StoreId,DateSold")] ProductSold productSold)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productSold);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.cutomers, "Id", "Id", productSold.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", productSold.ProductId);
            ViewData["StoreId"] = new SelectList(_context.Store, "Id", "Id", productSold.StoreId);
            return View(productSold);
        }




        // GET: ProductSolds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSold = await _context.ProductSold.FindAsync(id);
            if (productSold == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.cutomers, "Id", "Id", productSold.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", productSold.ProductId);
            ViewData["StoreId"] = new SelectList(_context.Store, "Id", "Id", productSold.StoreId);
            return View(productSold);
        }

        // POST: ProductSolds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,CustomerId,StoreId,DateSold")] ProductSold productSold)
        {
            if (id != productSold.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productSold);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductSoldExists(productSold.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.cutomers, "Id", "Id", productSold.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", productSold.ProductId);
            ViewData["StoreId"] = new SelectList(_context.Store, "Id", "Id", productSold.StoreId);
            return View(productSold);
        }

        // GET: ProductSolds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSold = await _context.ProductSold
                .Include(p => p.Customers)
                .Include(p => p.Products)
                .Include(p => p.Stores)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productSold == null)
            {
                return NotFound();
            }

            return View(productSold);
        }

        // POST: ProductSolds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productSold = await _context.ProductSold.FindAsync(id);
            if (productSold != null)
            {
                _context.ProductSold.Remove(productSold);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductSoldExists(int id)
        {
            return _context.ProductSold.Any(e => e.Id == id);
        }
    }
}
