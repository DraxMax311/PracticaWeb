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
    public class ProductosController : Controller
    {
        private readonly PracticaWebContext _context;
        public ProductosController(PracticaWebContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index(string orden, string columna, string busqueda, int? pagina)
        {
            ViewData["OrdenActual"] = orden;
            ViewData["ParamOrdenClave"] = string.IsNullOrEmpty(columna) ? "Clave_Desc" : "";
            ViewData["ParamOrdenActivo"] = columna == "Activo" ? "Activo_Desc" : "Activo";
            ViewData["ParamOrdenPrecio"] = columna == "Precio" ? "Precio_Desc" : "Precio";
            ViewData["ParamOrdenClaveProv"] = columna == "ClaveProveedor" ? "ClaveProveedor_Desc" : "ClaveProveedor";
            ViewData["ParamOrdenPrecioProv"] = columna == "PrecioProovedor" ? "PrecioProovedor_Desc" : "PrecioProovedor";
            ViewData["ParamOrdenTipo"] = columna == "Tipo" ? "Tipo_Desc" : "Tipo";
            ViewData["Filtro"] = busqueda;

            if (busqueda != null)
                pagina = 1;
            else
                busqueda = columna;

            var productos = from prod in _context.Productos select prod;
            if (!string.IsNullOrEmpty(busqueda))
            {
                productos = productos.Where(x => x.Clave == busqueda || x.ClaveProveedor== busqueda);
            }
            switch (columna)
            {
                case "Clave_Desc":
                    productos = productos.OrderByDescending(x=>x.Clave);
                    break;
                case "Activo":
                    productos = productos.OrderBy(x=>x.Activo);
                    break;
                case "Activo_Desc":
                    productos = productos.OrderByDescending(x=>x.Activo);
                    break;
                case "Precio":
                    productos = productos.OrderBy(x=>x.Precio);
                    break;
                case "Precio_Desc":
                    productos = productos.OrderByDescending(x=>x.Precio);
                    break;
                case "ClaveProveedor":
                    productos = productos.OrderBy(x=>x.ClaveProveedor);
                    break;
                case "ClaveProveedor_Desc":
                    productos = productos.OrderByDescending(x=>x.ClaveProveedor);
                    break;
                case "PrecioProovedor":
                    productos = productos.OrderBy(x=>x.PrecioProveedor);
                    break;
                case "PrecioProovedor_Desc":
                    productos = productos.OrderByDescending(x=>x.PrecioProveedor);
                    break;
                case "Tipo":
                    productos = productos.OrderBy(x=>x.TipoProducto);
                    break;
                case "Tipo_Desc":
                    productos = productos.OrderByDescending(x=>x.TipoProducto);
                    break;
                default:
                    productos = productos.OrderBy(x=>x.Clave);
                    break;

            }
            int tamañoPagina = 3;
            return View(await PaginatedList<Productos>.CreateAsync(productos.AsNoTracking(),pagina ?? 1,tamañoPagina));
        }

        // GET: Productos/Crear
        public IActionResult Crear()
        {
            return View();
        }

        // POST: Productos/Crear
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("ID_Producto,Clave,Activo,Precio,ClaveProveedor,PrecioProveedor,TipoProducto")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productos);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos.FindAsync(id);
            if (productos == null)
            {
                return NotFound();
            }
            return View(productos);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Producto,Clave,Activo,Precio,ClaveProveedor,PrecioProveedor,TipoProducto")] Productos productos)
        {
            if (id != productos.ID_Producto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductosExists(productos.ID_Producto))
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
            return View(productos);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos
                .FirstOrDefaultAsync(m => m.ID_Producto == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productos = await _context.Productos.FindAsync(id);
            if (productos != null)
            {
                _context.Productos.Remove(productos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductosExists(int id)
        {
            return _context.Productos.Any(e => e.ID_Producto == id);
        }
    }
}
