﻿using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WebApp.Models;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class SuppliersController : ControllerBase
    {
        private readonly DataContext _context;

        public SuppliersController(DataContext dataContext)
        {
            _context = dataContext;
        }

        [HttpGet("{id}")]
        public async Task<Supplier?> GetSupplier(long id)
        {
            Supplier supplier = await _context.Suppliers
                .Include(supplier => supplier.Products)
                .FirstAsync(supplier => supplier.SupplierId == id);
            if (supplier.Products != null)
            {
                foreach (Product product in supplier.Products)
                    product.Supplier = null;
            }

            return supplier;
        }

        [HttpPatch("{id}")]
        public async Task<Supplier?> PatchSupplier(long id, JsonPatchDocument<Supplier> patch)
        {
            Supplier? supplier = await _context.Suppliers.FindAsync(id);
            if (supplier != null)
            {
                patch.ApplyTo(supplier);
                await _context.SaveChangesAsync();
            }

            return supplier;
        }
    }
}
