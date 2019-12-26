using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Almacen;
using Sistema.Web.Models.Almacen.Categoria;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public CategoriasController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Categorias/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<CategoriaViewModel>> Listar()
        {
          var categoria = await _context.Categorias.ToListAsync();

            return categoria.Select(c => new CategoriaViewModel 
            { 
                idcategoria = c.Idcategoria,
                nombre = c.Nombre,
                description = c.Descripcion,
                condicion = c.Condicion
            });
        }

        // GET: api/Categorias/Mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<CategoriaViewModel>> Mostrar(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return new CategoriaViewModel 
            {
                idcategoria = categoria.Idcategoria,
                nombre  = categoria.Nombre,
                description = categoria.Descripcion,
                condicion = categoria.Condicion
            };
        }

        // PUT: api/Categorias/Actualizar/
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar(ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (model.idcategoria <= 0)
            {
                return BadRequest();
            }
            Categoria categoria = new Categoria
            {
                Idcategoria = model.idcategoria,
                Nombre = model.nombre,
                Descripcion = model.description,
                Condicion = model.condicion
            };
            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(model.idcategoria))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Categorias
        [HttpPost("[action]")]
        public async Task<ActionResult<CrearViewModel>> Crear(CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Categoria categoria = new Categoria {
                Nombre = model.nombre,
                Descripcion = model.description,
                Condicion = true
            };
            _context.Categorias.Add(categoria);
            try 
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                return BadRequest();
            }

            return Ok();
            //return CreatedAtAction("GetCategoria", new { id = categoria.Idcategoria }, categoria);
        }

        // DELETE: api/Categorias/Eliminar/5
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoria);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                return BadRequest();
            }

          return Ok(categoria);
        }

        //PUT: api/Categorias/Desactivar/5
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
   
            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Idcategoria == id);
            if (categoria == null) 
            {
                return NotFound();
            }
            categoria.Condicion = false;
            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        //PUT: api/Categorias/Activar/5
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Idcategoria == id);
            if (categoria == null)
            {
                return NotFound();
            }
            categoria.Condicion = true;
            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.Idcategoria == id);
        }
    }
}
