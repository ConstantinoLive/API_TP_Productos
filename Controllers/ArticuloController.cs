using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dominio;
using Negocio;
using TP_API_Producto.Models;

namespace TP_API_Producto.Controllers
{
    public class ArticuloController : ApiController
    {
        // GET: api/Articulo
        public IEnumerable<Articulo> Get()
        {
            ArticuloDatos negocio = new ArticuloDatos();

            return negocio.listar();
        }

        // GET: api/Articulo/5
        public Articulo Get(int id)
        {
            ArticuloDatos negocio = new ArticuloDatos();
            List<Articulo> lista = negocio.listar();
            
            return lista.FirstOrDefault(a => a.IdProductos == id); 
        }

        // POST: api/Articulo
        public void Post([FromBody] ArticuloDTO art)
        {
                ArticuloDatos negocio = new ArticuloDatos();
                Articulo nuevoArticulo = new Articulo();

                nuevoArticulo.CodArticulo = art.CodArticulo;
                nuevoArticulo.Nombre = art.Nombre;
                nuevoArticulo.Descripcion = art.Descripcion;
                nuevoArticulo.Marca = new Marca { id = art.IdMarca };
                nuevoArticulo.Categoria = new Categoria { Id = art.IdCategoria };
                nuevoArticulo.Precio = art.Precio;
                

                negocio.ingresar(nuevoArticulo); 
           
        }

        // PUT: api/Articulo/5
        public void Put(int id, [FromBody]ArticuloDTO art)
        {
                ArticuloDatos negocio = new ArticuloDatos();
                Articulo ArtExistente = new Articulo();

                ArtExistente.IdProductos = id;
                ArtExistente.CodArticulo = art.CodArticulo;
                ArtExistente.Nombre = art.Nombre;
                ArtExistente.Descripcion = art.Descripcion;
                ArtExistente.Marca = new Marca { id = art.IdMarca };
                ArtExistente.Categoria = new Categoria { Id = art.IdCategoria };
                ArtExistente.Precio = art.Precio;


                negocio.modificar(ArtExistente);
            
        }

        // DELETE: api/Articulo/5
        public void Delete(int id)
        {
            ArticuloDatos negocio = new ArticuloDatos();
            negocio.eliminarFisico(id);
        }
    }
}
