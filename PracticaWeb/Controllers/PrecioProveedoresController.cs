using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracticaWeb.Data;
using PracticaWeb.Models;

namespace PracticaWeb.Controllers
{
    public class PrecioProveedoresController : Controller
    {
        private readonly PracticaWebContext _context;

        public PrecioProveedoresController(PracticaWebContext context)
        {
            _context = context;
        }

        // GET: PrecioProveedores
        public async Task<IActionResult> Index()
        {
            return View(await _context.PreciosProveedoresDB.ToListAsync());
        }

        // GET: PrecioProveedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var precioProveedores = await _context.PreciosProveedoresDB
                .FirstOrDefaultAsync(m => m.ID_Precio == id);
            if (precioProveedores == null)
            {
                return NotFound();
            }

            return View(precioProveedores);
        }

        // GET: PrecioProveedores/Crear
        public IActionResult Crear()
        {
            return View("Crear");
        }

        // POST: PrecioProveedores/Crear
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Crear(int ID_Producto, int ID_Proveedor, decimal Precio, string ClaveProveedor)
        {
            try
            {
                PrecioProveedores precioProveedores = new PrecioProveedores();
                PrecioProveedores? modeloExistente = _context.PreciosProveedoresDB.Where(x => x.ID_Proveedor == ID_Proveedor && x.ID_Producto == ID_Producto).FirstOrDefault();
                if (ModelState.IsValid && modeloExistente == null)
                {
                    int ID_Precio = _context.PreciosProveedoresDB.Max(x => x.ID_Precio) + 1;
                    precioProveedores.ID_Producto = ID_Producto;
                    precioProveedores.ID_Proveedor = ID_Proveedor;
                    precioProveedores.Precio = Precio;
                    precioProveedores.ClaveProveedor = ClaveProveedor;
                    await _context.PreciosProveedoresDB.AddAsync(precioProveedores);
                    await _context.SaveChangesAsync();
                    precioProveedores.ID_Precio = ID_Precio;
                    ViewBag.NuevoPrecio = precioProveedores;
                    return PartialView("NewTableLine");
                }
                return PartialView("NewTableLine");
            }
            catch (Exception ex)
            {
                return PartialView("NewTableLine");
            }
        }

        // GET: PrecioProveedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var precioProveedores = await _context.PreciosProveedoresDB.FindAsync(id);
            if (precioProveedores == null)
            {
                return NotFound();
            }
            return View(precioProveedores);
        }

        // POST: PrecioProveedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Precio,ID_Producto,ID_Proveedor,Precio,ClaveProveedor")] PrecioProveedores precioProveedores)
        {
            if (id != precioProveedores.ID_Precio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(precioProveedores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrecioProveedoresExists(precioProveedores.ID_Precio))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), "Productos");
            }
            return View(precioProveedores);
        }

        // GET: PrecioProveedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var precioProveedores = await _context.PreciosProveedoresDB
                .FirstOrDefaultAsync(m => m.ID_Precio == id);
            if (precioProveedores == null)
            {
                return NotFound();
            }

            return View(precioProveedores);
        }

        // POST: PrecioProveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var precioProveedores = await _context.PreciosProveedoresDB.FindAsync(id);
            if (precioProveedores != null)
            {
                _context.PreciosProveedoresDB.Remove(precioProveedores);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), "Productos");
        }

        private bool PrecioProveedoresExists(int id)
        {
            return _context.PreciosProveedoresDB.Any(e => e.ID_Precio == id);
        }
    }
}
