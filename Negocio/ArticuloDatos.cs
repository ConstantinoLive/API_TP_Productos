using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dominio;


namespace Negocio
{
    public class ArticuloDatos
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server = .\\SQLEXPRESS; database = CATALOGO_P3_DB; integrated security = true";
                comando.CommandType = System.Data.CommandType.Text;
                //comando.CommandText = "select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion Marca,C.Descripcion Categoria,Precio, I.ImagenUrl from ARTICULOS A, MARCAS M, CATEGORIAS C, IMAGENES I where M.Id=A.IdMarca and C.Id=A.IdCategoria and I.Id = A.Id";
                comando.CommandText = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion AS Marca, C.Descripcion AS Categoria, A.Precio, I.ImagenUrl FROM ARTICULOS A LEFT JOIN MARCAS M ON M.Id = A.IdMarca LEFT JOIN CATEGORIAS C ON C.Id = A.IdCategoria LEFT JOIN IMAGENES I ON I.IdArticulo = A.Id";
                //comando.CommandText = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion AS Marca, C.Descripcion AS Categoria, A.Precio, (SELECT TOP 1 ImagenUrl FROM IMAGENES WHERE IdArticulo = A.Id) AS ImagenUrl FROM ARTICULOS A LEFT JOIN MARCAS M ON M.Id = A.IdMarca LEFT JOIN CATEGORIAS C ON C.Id = A.IdCategoria GROUP BY A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion, C.Descripcion, A.Precio";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.IdProductos = lector["Id"] != DBNull.Value ? Convert.ToInt32(lector["Id"]) : 0;
                    aux.CodArticulo = lector["Codigo"] != DBNull.Value ? lector["Codigo"].ToString() : string.Empty;
                    aux.Nombre = lector["Nombre"] != DBNull.Value ? lector["Nombre"].ToString() : string.Empty;
                    aux.Descripcion = lector["Descripcion"] != DBNull.Value ? lector["Descripcion"].ToString() : string.Empty;
                    aux.Marca = new Marca();
                    aux.Marca.id = lector["Id"] != DBNull.Value ? Convert.ToInt32(lector["Id"]) : 0;
                    aux.Marca.marca = lector["Marca"] != DBNull.Value ? lector["Marca"].ToString() : string.Empty;
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = lector["Id"] != DBNull.Value ? Convert.ToInt32(lector["Id"]) : 0;
                    aux.Categoria.categoria = lector["Categoria"] != DBNull.Value ? lector["Categoria"].ToString() : string.Empty;
                    aux.Precio = lector["Precio"] != DBNull.Value ? Convert.ToDecimal(lector["Precio"]):0;
                    aux.UrlImagen = lector["ImagenUrl"] != DBNull.Value ? lector["ImagenUrl"].ToString() : string.Empty;

                    lista.Add(aux);
                }

                conexion.Close();

                return lista;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error al cargar la base de datos");
                throw ex;

            }

        }
        public void ingresar(Articulo nuevo)
        {
            AccesoDatos IngresarDatos = new AccesoDatos();
            ImagenesDatos CargarImagen = new ImagenesDatos();

            try
            {
                IngresarDatos.Consulta("INSERT INTO ARTICULOS (Codigo,Nombre,Descripcion,IdMarca,IdCategoria,Precio) VALUES (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio)");
                IngresarDatos.SetearParametros("@Codigo", nuevo.CodArticulo);
                IngresarDatos.SetearParametros("@Nombre", nuevo.Nombre);
                IngresarDatos.SetearParametros("@Descripcion", nuevo.Descripcion);
                IngresarDatos.SetearParametros("@IdMarca", nuevo.Marca.id);
                IngresarDatos.SetearParametros("@IdCategoria", nuevo.Categoria.Id);
                IngresarDatos.SetearParametros("@Precio", nuevo.Precio);

                IngresarDatos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                IngresarDatos.CerrarConexion();
            }

        }

        public void eliminarFisico(int idArticulo)
        {
            try
            {
                AccesoDatos accesoDatos = new AccesoDatos();
                accesoDatos.Consulta("DELETE FROM ARTICULOS WHERE Id = @Id");
                accesoDatos.SetearParametros("@Id", idArticulo);
                //accesoDatos.SetearParametro("@Id", idArticulo);
                accesoDatos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificar(Articulo Art)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.Consulta("UPDATE ARTICULOS SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, Precio = @Precio WHERE Id = @Id");
                datos.SetearParametros("@Id", Art.IdProductos);
                datos.SetearParametros("@Codigo", Art.CodArticulo);
                datos.SetearParametros("@Nombre", Art.Nombre);
                datos.SetearParametros("@Descripcion", Art.Descripcion);
                datos.SetearParametros("@IdMarca", Art.Marca.id);
                datos.SetearParametros("@IdCategoria", Art.Categoria.Id);
                datos.SetearParametros("@Precio", Art.Precio);

                datos.EjecutarAccion();

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
    }
}
