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
        public void Post([FromBody]ImagenesDTO img)
        {
            ImagenesDatos negocio = new ImagenesDatos();
            
            negocio.ingresarUrl (img.ImagenUrl, img.IdArticulo);
        }

        // PUT: api/Imagenes/5
        public void Put(int id, [FromBody]ImagenesDTO img)
        {
            ImagenesDatos negocio = new ImagenesDatos();
            Imagenes imagenExistente = new Imagenes();
            imagenExistente.id = id;
            imagenExistente.IdArticulo = img.IdArticulo;
            imagenExistente.ImagenUrl = img.ImagenUrl;
                       

            negocio.ModificarImagen(imagenExistente.id, imagenExistente.ImagenUrl);
        }

        // DELETE: api/Imagenes/5
        public void Delete(int id)
        {
            ImagenesDatos negocio = new ImagenesDatos();
            
            negocio.eliminarFisico(id);
        }
    }
}
