using ahorra_marcket.DTO;
using ahorra_marcket.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace ahorra_marcket.Controllers
{
    [Route("api/market")] //Expone al ruta de API con la que se debe usar
    [ApiController] //Indica que es un controlador de API
    [Produces("text/json")] // Indica que tipo de formato se va a retornar
    public class ValuesController : ControllerBase
    {

        /// <summary>
        /// Método para consultar los precios de las tiendas por producto
        /// Se usa Entity Framework Core para consultar las tablas de la base datos haciendo un inner join desde producto tienda
        /// hacia productos y tiendas, finalizando con un where para filtrar el nombre del producto que se desea consultar
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("obtener/{nombre}")]
        public IActionResult ObtenerPrecios([FromRoute] string nombre)
        {
            //Using se usa para usar un objeto y dejarlo disponible al finaliar la transaccion
            using (ahorra_marketContext _context = new ahorra_marketContext())
            {
                var resultado =
                    _context.ProductoTienda
                    .Join(_context.Productos, a => a.IdProducto, b => b.IdProducto, (a, b) => new { a, b })
                    .Join(_context.Tienda, c => c.a.IdTienda, d => d.IdTienda, (c, d) => new { c, d })
                    .Where(wh => wh.c.b.Nombre.Contains(nombre))
                    .Select(datos => new
                    {
                        id = datos.c.a.IdProductoTienda,
                        producto = datos.c.b.Nombre,
                        tienda = datos.d.Nombre,
                        precio = datos.c.a.Precio
                    }).ToList();

                return Ok(resultado);
            }
        }

        /// <summary>
        /// Método para obtener el productotienda con el id, se usa entiry framework core para consultar la tabla en la base de datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private ProductoTiendum ObtenerTiendaProducto(int id)
        {
            ProductoTiendum producto = new ProductoTiendum();

            try
            {
                using (ahorra_marketContext _context = new ahorra_marketContext())
                {
                    producto = _context.ProductoTienda.Where(wh => wh.IdProductoTienda == id).First();
                }

                return producto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Método de API para actualizar un registro de la tabla producto tienda, , se usa entiry framework core para actualizar
        /// </summary>
        /// <param name="dato"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("actualizar")]
        public IActionResult ActualizarPrecios([FromBody] PrecioDTO dato)
        {
            try
            {
                ProductoTiendum producto = ObtenerTiendaProducto(dato.Id);

                if (producto is null)
                    return BadRequest("Error en el id");

                using (ahorra_marketContext _context = new ahorra_marketContext())
                {
                    producto.Precio = dato.Precio;
                    _context.Update(producto);
                    _context.SaveChanges();

                    return Ok("Se actualizó el registro " + dato.Id.ToString());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Método de API para borrar un registro de la tabla producto tienda, se usa entiry framework core para borrar
        /// </summary>
        /// <param name="dato"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("borrar")]
        public IActionResult Borrar([FromBody] PrecioDTO dato)
        {
            try
            {
                ProductoTiendum producto = ObtenerTiendaProducto(dato.Id);

                if (producto is null)
                    return BadRequest("Error en el id");

                using (ahorra_marketContext _context = new ahorra_marketContext())
                {
                    _context.Remove(producto);
                    _context.SaveChanges();

                    return Ok("Se borró el registro " + dato.Id.ToString());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Método para buscar productos con el nombre, se usa entity framework core para poder ralizar la consulta
        /// </summary>
        /// <param name="nombreProducto"></param>
        /// <returns></returns>

        private Producto ObtenerProducto(string nombreProducto)
        {
            Producto producto = new Producto();

            try
            {
                using (ahorra_marketContext _context = new ahorra_marketContext())
                {
                    producto = _context.Productos.Where(wh => wh.Nombre.ToLower() == nombreProducto.ToLower()).First();
                }

                return producto;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// Método para buscar tiendad por el nombre, se usa entity framework core para poder ralizar la consulta
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>

        private Tiendum ObtenerTienda(string nombre)
        {
            Tiendum tienda = new Tiendum();

            try
            {
                using (ahorra_marketContext _context = new ahorra_marketContext())
                {
                    tienda = _context.Tienda.Where(wh => wh.Nombre.ToLower() == nombre.ToLower()).First();
                }

                return tienda;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// Método de API para mostrar tiendas, se usa entity framework core para poder ralizar la consulta
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("tiendas")]
        public IActionResult ObtenerTiendas()
        {

            try
            {
                using (ahorra_marketContext _context = new ahorra_marketContext())
                {
                    return Ok(_context.Tienda.Select(s =>  s.Nombre ).ToList());
                }

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Método de API para mostrar productos, se usa entity framework core para poder ralizar la consulta
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("productos")]
        public IActionResult ObtenerProductos()
        {

            try
            {
                using (ahorra_marketContext _context = new ahorra_marketContext())
                {
                    return Ok(_context.Productos.Select(s =>  s.Nombre ).ToList());
                }

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Método de API para poder crear registros en la tabla prodcutotienda, se usa entity framework core para poder ralizar la creación
        /// </summary>
        /// <param name="dato"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("crear")]
        public IActionResult Crear([FromBody] PrecioDTO dato)
        {
            try
            {
                Producto producto = ObtenerProducto(dato.Producto);

                if (producto is null)
                    return BadRequest("Error al validar producto");

                Tiendum tienda = ObtenerTienda(dato.Tienda);

                if (tienda is null)
                    return BadRequest("Error al validar tienda");

                using (ahorra_marketContext _context = new ahorra_marketContext())
                {
                    ProductoTiendum item = new ProductoTiendum()
                    {
                        IdProducto = producto.IdProducto,
                        IdTienda = tienda.IdTienda,
                        Precio = dato.Precio
                    };

                    _context.Add(item);
                    _context.SaveChanges();

                    return Ok("Se ha creado con éxito el registro " + item.IdProductoTienda);

                }


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}

