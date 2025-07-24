using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Negocio
{
    internal class AccesoDatos
    {
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;

        public AccesoDatos()
        {
            connection = new SqlConnection("server = .\\SQLEXPRESS; database = CATALOGO_P3_DB; integrated security = true");
            command = new SqlCommand();
        }

        public void Consulta(string consulta)
        {
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = consulta;
        }

        public void EjecutarConsulta()
        {
            command.Connection = connection;

            try
            {
                connection.Open();
                reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EjecutarAccion()
        {
            command.Connection = connection;

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void SetearParametros(string nombre, object valor)
        {
            command.Parameters.AddWithValue(nombre, valor);
        }

        public SqlDataReader Reader
        {
            get { return reader; }
        }

        public void CerrarConexion()
        {
            if (reader != null)
                reader.Close();
            connection.Close();
        }
    }
}
