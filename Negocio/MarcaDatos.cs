using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class MarcaDatos
    {
        public List<Marca> listarMarca()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.Consulta("select Id, Descripcion from MARCAS");
                datos.EjecutarConsulta();

                while (datos.Reader.Read())
                {
                    Marca aux = new Marca();

                    aux.id = (int)datos.Reader["Id"];
                    aux.marca = (string)datos.Reader["Descripcion"];
                    lista.Add(aux);
                }


                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }

        }

        public void eliminarFisico(int idMarca)
        {
            try
            {
                AccesoDatos accesoDatos = new AccesoDatos();
                accesoDatos.Consulta("DELETE FROM MARCAS WHERE Id = @IdMarca");
                accesoDatos.SetearParametros("@IdMarca", idMarca);
                accesoDatos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void agregarMarca(string nuevaMarca)
        {
            try
            {
                AccesoDatos accesoDatos = new AccesoDatos();
                accesoDatos.Consulta("INSERT INTO MARCAS (Descripcion) VALUES (@Marca)");
                accesoDatos.SetearParametros("@Marca", nuevaMarca);
                accesoDatos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarMarca(int Id, string nuevaMarca)
        {
            try
            {
                AccesoDatos accesoDatos = new AccesoDatos();
                accesoDatos.Consulta("UPDATE MARCAS SET Descripcion = @Marca WHERE Id = @IdMarca");
                accesoDatos.SetearParametros("@Marca", nuevaMarca);
                accesoDatos.SetearParametros("@IdMarca", Id);
                accesoDatos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
