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
        public HttpResponseMessage Post([FromBody] ArticuloDTO art)
        {
            try
            {
                ArticuloDatos negocio = new ArticuloDatos();
                MarcaDatos mrk = new MarcaDatos();
                CategoriaDatos cat = new CategoriaDatos();

                Articulo nuevoArticulo = new Articulo();

                Marca marca = mrk.listarMarca().Find(m => m.id == art.IdMarca);
                Categoria categoria = cat.listarCategorias().Find(c => c.Id == art.IdCategoria);

                if(marca==null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "La marca ingresada no existe.");
                }

                if(categoria==null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "La categoría ingresada no existe.");
                }

                nuevoArticulo.CodArticulo = art.CodArticulo;
                nuevoArticulo.Nombre = art.Nombre;
                nuevoArticulo.Descripcion = art.Descripcion;
                nuevoArticulo.Marca = marca;
                nuevoArticulo.Categoria = categoria;
                nuevoArticulo.Precio = art.Precio;


                negocio.ingresar(nuevoArticulo);
                return Request.CreateResponse(HttpStatusCode.OK, "Artículo agregado correctamente.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Ocurrió un error inesperado: " + ex);
            }
           
        }

        // PUT: api/Articulo/5
        public HttpResponseMessage Put(int id, [FromBody]ArticuloDTO art)
        {
            try
            {

                ArticuloDatos negocio = new ArticuloDatos();
                MarcaDatos mrk = new MarcaDatos();
                CategoriaDatos cat = new CategoriaDatos();

                Articulo ArtExistente = new Articulo();

                Marca marca = mrk.listarMarca().Find(m => m.id == art.IdMarca);
                Categoria categoria = cat.listarCategorias().Find(c => c.Id == art.IdCategoria);

                if (marca == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "La marca ingresada no existe.");
                }

                if (categoria == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "La categoría ingresada no existe.");
                }

                ArtExistente.IdProductos = id;
                ArtExistente.CodArticulo = art.CodArticulo;
                ArtExistente.Nombre = art.Nombre;
                ArtExistente.Descripcion = art.Descripcion;
                ArtExistente.Marca = marca;
                ArtExistente.Categoria = categoria;
                ArtExistente.Precio = art.Precio;


                negocio.modificar(ArtExistente);
                
                return Request.CreateResponse(HttpStatusCode.OK, "Artículo modificado correctamente.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Ocurrió un error inesperado: " + ex);
            }
            
        }

        // DELETE: api/Articulo/5
        public void Delete(int id)
        {
            ArticuloDatos negocio = new ArticuloDatos();
            negocio.eliminarFisico(id);
        }
    }
}
