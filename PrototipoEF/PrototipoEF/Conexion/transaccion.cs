using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrototipoEF.Conexion
{
    public class transaccion
    {
        public void insertarDatos(params string[] sSentencia)
        {
            Conexion conexion = new Conexion();
            var resultado = conexion.conexion();
            OdbcCommand comando = resultado.Item1.CreateCommand();
            OdbcTransaction transaccion = resultado.Item2;
            comando.Transaction = transaccion;

            try
            {
                foreach (string sentencia in sSentencia)
                {
                    comando.CommandText = sentencia;
                    comando.ExecuteNonQuery();
                }

                transaccion.Commit();
            }
            catch (OdbcException ex)
            {
                transaccion.Rollback();
                MessageBox.Show("Error en la operacion con la Base de Datos: \n" + ex.Message);

            }
            finally
            {
                conexion.desconexion();
            }
        }

        public void eliminarDatos(params string[] sSentencia)
        {
            Conexion conexion = new Conexion();
            var resultado = conexion.conexion();
            OdbcCommand comando = resultado.Item1.CreateCommand();
            OdbcTransaction transaccion = resultado.Item2;
            comando.Transaction = transaccion;

            try
            {
                foreach (string sentencia in sSentencia)
                {
                    comando.CommandText = sentencia;
                    comando.ExecuteNonQuery();
                }

                transaccion.Commit();
            }
            catch (OdbcException ex)
            {
                transaccion.Rollback();
                MessageBox.Show("> Error en la eliminacion a nivel de la Base de Datos: \n" + ex.Message);
            }
            finally
            {
                conexion.desconexion();
            }
        }

        public OdbcDataReader ConsultarDatos(string sParametro)
        {
            Conexion conexion = new Conexion();
            var resultado = conexion.conexion();
            OdbcCommand comando = resultado.Item1.CreateCommand();
            OdbcTransaction transaction = resultado.Item2;
            OdbcDataReader reader;
            try
            {
                comando.Transaction = transaction;
                comando.CommandText = sParametro;
                reader = comando.ExecuteReader();
            }
            catch (OdbcException ex)
            {
                MessageBox.Show("Error en la operacion con la Base de Datos: \n" + ex.Message);
                return null;
            }
            return reader;
        }
    }
}
