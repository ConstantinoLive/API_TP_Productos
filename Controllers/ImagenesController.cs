using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Negocio;
using TP_API_Producto.Models;

namespace TP_API_Producto.Controllers
{
    public class ImagenesController : ApiController
    {
        // GET: api/Imagenes
        public IEnumerable<Imagenes> Get()
        {
            ImagenesDatos negocio = new ImagenesDatos();

            return negocio.listarImagenes();
        }

        // GET: api/Imagenes/5
        public Imagenes Get(int id)
        {
            ImagenesDatos negocio = new ImagenesDatos();
            List<Imagenes> listaI = negocio.listarImagenes();
            
            return listaI.FirstOrDefault(i => i.id == id);
        }

        // POST: api/Imagenes
        public HttpResponseMessage Post([FromBody]ImagenesDTO img)
        {
            try
            {
                ImagenesDatos negocio = new ImagenesDatos();
                ArticuloDatos artNegocio = new ArticuloDatos();

                Articulo articulo = artNegocio.listar().FirstOrDefault(a => a.IdProductos == img.IdArticulo);

                if (articulo == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El artículo no existe.");
                }

                negocio.ingresarUrl(img.ImagenUrl, img.IdArticulo);

                return Request.CreateResponse(HttpStatusCode.Created, "Imagen agregada correctamente.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error al agregar la imagen: " + ex.Message);
            }
            
        }

        // PUT: api/Imagenes/5
        public HttpResponseMessage Put(int id, [FromBody]ImagenesDTO img)
        {
            try
            {
                ImagenesDatos negocio = new ImagenesDatos();
                ArticuloDatos ArtNegocio = new ArticuloDatos();

                Imagenes imagenExistente = new Imagenes();
                Articulo articulo = ArtNegocio.listar().FirstOrDefault(a => a.IdProductos == img.IdArticulo);

                if (articulo == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El artículo no existe.");
                }
                imagenExistente.id = id;
                imagenExistente.IdArticulo = img.IdArticulo;
                imagenExistente.ImagenUrl = img.ImagenUrl;

                
                negocio.ModificarImagen(imagenExistente.id, imagenExistente.ImagenUrl);
                return Request.CreateResponse(HttpStatusCode.OK, "Imagen modificada correctamente.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error al modificar la imagen: " + ex.Message);
            }
        }

        // DELETE: api/Imagenes/5
        public void Delete(int id)
        {
            ImagenesDatos negocio = new ImagenesDatos();
            
            negocio.eliminarFisico(id);
        }
    }
}
