using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        public async Task<IActionResult> Index(string orden, string tipo, string busqueda, int? pagina)
        {
            ViewData["OrdenActual"] = orden;
            ViewData["Filtro"] = busqueda;

            if (busqueda != null)
                pagina = 1;

            var productos = from prod in _context.ProductosDB select prod;
            if (!string.IsNullOrEmpty(busqueda))
            {
                productos = productos.Where(x => x.Clave == busqueda || x.Nombre == busqueda);
            }
            if (!tipo.IsNullOrEmpty())
                productos = productos.Where(x => x.TipoProducto == int.Parse(tipo));
            int tamañoPagina = 5;
            List<SelectListItem> tiposProducto = (from tipoProducto in _context.TipoProductosDB
                                                  select new SelectListItem()
                                                  {
                                                      Text = tipoProducto.Nombre + " | " + tipoProducto.Descripcion,
                                                      Value = tipoProducto.ID_TipoProducto.ToString()
                                                  }).ToList();
            ViewBag.Tipos = tiposProducto;
            return View(await PaginatedList<Productos>.CreateAsync(productos.AsNoTracking(), pagina ?? 1, tamañoPagina));
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
        public async Task<IActionResult> Crear([Bind("ID_Producto,Nombre,Clave,Activo,Precio,TipoProducto")] Productos productos)
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

            var productos = await _context.ProductosDB.FindAsync(id);
            if (productos == null)
            {
                return NotFound();
            }
            List<Proveedores> proveedores = await _context.ProveedoresDB.ToListAsync();
            List<PrecioProveedores> precios = await _context.PreciosProveedoresDB.Where(x => x.ID_Producto == id).ToListAsync();
            ViewBag.Precios = from precio in precios
                                  join proveedor in proveedores
                                  on precio.ID_Proveedor equals proveedor?.ID_Proveedor
                                  select new
                                  {
                                      ID_Precio = precio.ID_Precio,
                                      Proveedor = proveedor.Nombre,
                                      Clave = precio.ClaveProveedor,
                                      Costo = precio.Precio
                                  };
            List<SelectListItem> tipos = new List<SelectListItem>();
            List<TipoProductos> tiposProductos = _context.TipoProductosDB.ToList();
            tipos.AddRange((from tipoProducto in tiposProductos
                            select new SelectListItem()
                            {
                                Text = tipoProducto.Nombre + " | " + tipoProducto.Descripcion,
                                Value = tipoProducto.ID_TipoProducto.ToString()
                            }).ToList());
            SelectListItem? selectListItem = (from tipoProducto in tiposProductos
                                              where tipoProducto.ID_TipoProducto == productos.TipoProducto
                                             select new SelectListItem() { Text = tipoProducto.Nombre + " | " + tipoProducto.Descripcion, Value = tipoProducto.ID_TipoProducto.ToString() }).FirstOrDefault();
            if (selectListItem != null)
                tipos.Insert(0, selectListItem);
            ViewBag.Tipos = tipos;
            ViewBag.Proveedores = (from proveedor in proveedores
                                   select new SelectListItem() { 
                                       Text = proveedor.Nombre,
                                       Value = proveedor.Descripcion
                                   });
            return View(productos);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Producto,Nombre,Clave,Activo,Precio,TipoProducto")] Productos productos)
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

            var productos = await _context.ProductosDB
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
            var productos = await _context.ProductosDB.FindAsync(id);
            if (productos != null)
            {
                _context.ProductosDB.Remove(productos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductosExists(int id)
        {
            return _context.ProductosDB.Any(e => e.ID_Producto == id);
        }
    }
}
