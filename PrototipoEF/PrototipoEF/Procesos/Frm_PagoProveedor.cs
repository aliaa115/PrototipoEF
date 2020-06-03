using PrototipoEF.SQL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PrototipoEF.Procesos
{
    public partial class Frm_PagoProveedor : Form
    {
        Cls_SQL sql = new Cls_SQL();
        public Frm_PagoProveedor()
        {
            InitializeComponent();
            lenarDGV();
            //se define el formato del dateTimePicker
            Dtp_fecha.Format = DateTimePickerFormat.Custom;
            Dtp_fecha.CustomFormat = "dd MM yyyy";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string signo;
            if (Cbo_TipoTransaccion.Text == "Abono")
            {
                signo = "-";
            }
            else if (Cbo_TipoTransaccion.Text == "Cargo")
            {
                signo = "+";
            }
            else
            {
                MessageBox.Show("Por favor seleccione un tipo de accion");
                return;
            }

            string fecha = Dtp_fecha.Value.Date.ToString("yyyy-MM-dd");

            string monto;
            if (Nud_monto.Value != 0)
            {
                monto = Nud_monto.Value.ToString();
            }
            else
            {
                MessageBox.Show("Por favor seleccione el monto");
                return;
            }

            string cuenta = "1";

            string[] valores = {
                fecha,
                cuenta,
                monto,
                signo
            };

            string[] valores2 =
            {
                cuenta,
                signo,
                monto
            };

            sql.insertarMovimiento(valores);
            sql.alterarCuenta(valores2);
            lenarDGV();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Dgv_mov.RowCount != 0)
            {
                string signo = "";
                if (Dgv_mov.CurrentRow.Cells[2].Value.ToString() == "+") { signo = "-"; }
                else if (Dgv_mov.CurrentRow.Cells[2].Value.ToString() == "-") { signo = "+"; }
                string cuenta = Dgv_mov.CurrentRow.Cells[1].Value.ToString();
                string monto = Dgv_mov.CurrentRow.Cells[3].Value.ToString();

                string codigo = Dgv_mov.CurrentRow.Cells[0].Value.ToString();
                sql.EliminarMovimiento(codigo);
                string[] valores =
                {
                    cuenta,
                    signo,
                    monto
                };
                sql.alterarCuenta(valores);
                lenarDGV();
            }

        }

        private void lenarDGV()
        {
            Dgv_mov.Rows.Clear();
            List<MOVIMIENTO> listaMovmientos = sql.obtenerMovimietnoProveedor();

            int fila = 0;

            foreach (MOVIMIENTO mov in listaMovmientos)
            {
                Dgv_mov.Rows.Add();
                Dgv_mov.Rows[fila].Cells[0].Value = mov.id_mov;
                Dgv_mov.Rows[fila].Cells[1].Value = mov.cuenta.id_cuenta;
                Dgv_mov.Rows[fila].Cells[2].Value = mov.signo;
                Dgv_mov.Rows[fila].Cells[3].Value = mov.monto;
                Dgv_mov.Rows[fila].Cells[4].Value = mov.fecha_mov;
                fila++;
            }
        }

        private void Dgv_mov_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
