using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrototipoEF.Conexion;

namespace PrototipoEF.SQL
{
    public class CUENTA
    {
        public int id_cuenta { get; set; }
        public string nombre_cuenta { get; set; }
        public float monto_cuenta { get; set; }
    }
    public class MOVIMIENTO
    {
        public int id_mov { get; set; }
        public DateTime fecha_mov { get; set; }
        public CUENTA cuenta { get; set; }
        public float monto { get; set; }
        public string signo { get; set; }
    }
    public class Cls_SQL
    {
        transaccion transaccion = new transaccion();

        public CUENTA obtenerCuenta(int id_cuenta)
        {
            CUENTA cuenta = new CUENTA();
            try
            {
                string sComando = string.Format("SELECT id_cuenta, nombre_cuenta " +
                    "FROM cuenta_corriente " +
                    "WHERE id_cuenta = {0} ;",
                    id_cuenta);

                OdbcDataReader reader = transaccion.ConsultarDatos(sComando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cuenta.id_cuenta = reader.GetInt32(0);
                        cuenta.nombre_cuenta = reader.GetString(1);
                    }
                }
                return cuenta;
            }
            catch (OdbcException ex)
            {
                MessageBox.Show("Error en la operacion con la Base de Datos: \n" + ex.Message);
                return null;
            }
        }
        public List<MOVIMIENTO> obtenerMovimietnoCliente()
        {
            List <MOVIMIENTO> MOV = new List<MOVIMIENTO>();

            try
            {
                string sComando = string.Format("" +
                    "SELECT " +
                        "id_movimiento, " +
                        "fecha, " +
                        "id_cuenta, " +
                        "monto, " +
                        "signo " +
                    "FROM movimiento_cuenta " +
                    "WHERE id_cuenta = 2;");

                OdbcDataReader reader = transaccion.ConsultarDatos(sComando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MOVIMIENTO movimiento = new MOVIMIENTO();
                        movimiento.id_mov = reader.GetInt32(0);
                        movimiento.fecha_mov = reader.GetDate(1);
                        movimiento.cuenta = obtenerCuenta(reader.GetInt32(2));
                        movimiento.monto = reader.GetFloat(3);
                        movimiento.signo = reader.GetString(4);
                        MOV.Add(movimiento);
                    }
                }
                return MOV;
            }
            catch (OdbcException ex)
            {
                MessageBox.Show("Error en la operacion con la Base de Datos: \n" + ex.Message);
                return null;
            }

        }
        public List<MOVIMIENTO> obtenerMovimietnoProveedor()
        {
            List<MOVIMIENTO> MOV = new List<MOVIMIENTO>();

            try
            {
                string sComando = string.Format("" +
                    "SELECT " +
                        "id_movimiento, " +
                        "fecha, " +
                        "id_cuenta, " +
                        "monto, " +
                        "signo " +
                    "FROM movimiento_cuenta " +
                    "WHERE id_cuenta = 1;");

                OdbcDataReader reader = transaccion.ConsultarDatos(sComando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MOVIMIENTO movimiento = new MOVIMIENTO();
                        movimiento.id_mov = reader.GetInt32(0);
                        movimiento.fecha_mov = reader.GetDate(1);
                        movimiento.cuenta = obtenerCuenta(reader.GetInt32(2));
                        movimiento.monto = reader.GetFloat(3);
                        movimiento.signo = reader.GetString(4);
                        MOV.Add(movimiento);
                    }
                }
                return MOV;
            }
            catch (OdbcException ex)
            {
                MessageBox.Show("Error en la operacion con la Base de Datos: \n" + ex.Message);
                return null;
            }

        }
        public void insertarMovimiento(string[] valores)
        {
            string sCommando = string.Format(
                "INSERT INTO movimiento_cuenta ( " +
                   "fecha, " +
                   "id_cuenta, " +
                   "monto, " +
                   "signo) VALUES " +
                   "('{0}', {1}, {2}, '{3}'); ",
                   valores[0], valores[1], valores[2], valores[3]);

            try
            {
                transaccion.insertarDatos(sCommando);
            }
            catch (OdbcException ex)
            {
                MessageBox.Show("Error en la operacion con la Base de Datos: \n" + ex.Message);
            }
        }
        public void EliminarMovimiento(string id_mov)
        {
            string sCommando = string.Format( "DELETE FROM movimiento_cuenta WHERE id_movimiento = {0}; ", id_mov);

            try
            {
                transaccion.insertarDatos(sCommando);
            }
            catch (OdbcException ex)
            {
                MessageBox.Show("Error en la operacion con la Base de Datos: \n" + ex.Message);
            }
        }
        public void alterarCuenta(string[] valores)
        {
            string sCommando = string.Format(
                "UPDATE cuenta_corriente SET " +
                    "monto_restante = monto_restante {1} {2} " +
                    "WHERE id_cuenta = {0}; ",
                    valores[0], valores[1], valores[2]);
            try
            {
                transaccion.insertarDatos(sCommando);
            }
            catch (OdbcException ex)
            {
                MessageBox.Show("Error en la operacion con la Base de Datos: \n" + ex.Message);
            }
        }
    }
}
