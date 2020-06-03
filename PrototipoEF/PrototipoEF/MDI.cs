using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaDiseno;
using PrototipoEF.Mantenimientos;
using PrototipoEF.Procesos;
using PrototipoEF.Reportes;

namespace PrototipoEF
{
    public partial class MDI : Form
    {
        private int childFormNumber = 0;
        string usuario;
        public MDI()
        {
            InitializeComponent();
        }

        //funcion para mantenimientos
        private void mant(int tabla)
        {
            Frm_mantenimiento mantenimiento = new Frm_mantenimiento(usuario, tabla);
            mantenimiento.Show();
            mantenimiento.TopLevel = false;
            mantenimiento.TopMost = true;
            panel1.Controls.Add(mantenimiento);
        }
        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Ventana " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void seguridadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MDI_Seguridad seguridad = new MDI_Seguridad(Lbl_usuario.Text);
            seguridad.lbl_nombreUsuario.Text = Lbl_usuario.Text;
            seguridad.ShowDialog();
        }

        private void MDI_Load(object sender, EventArgs e)
        {
            frm_login login = new frm_login();
            login.ShowDialog();
            Lbl_usuario.Text = login.obtenerNombreUsuario();
            usuario = Lbl_usuario.Text;
        }

        private void cuentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mant(1);
        }

        private void pagoDeClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_PagoClientes pagoClientes = new Frm_PagoClientes();
            pagoClientes.Show();
            pagoClientes.TopLevel = false;
            pagoClientes.TopMost = true;
            panel1.Controls.Add(pagoClientes);
        }

        private void pagoAProveedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_PagoProveedor pagoProveedor = new Frm_PagoProveedor();
            pagoProveedor.Show();
            pagoProveedor.TopLevel = false;
            pagoProveedor.TopMost = true;
            panel1.Controls.Add(pagoProveedor);
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_reporteCliente reporteCliente = new Frm_reporteCliente();
            reporteCliente.Show();
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_reporteProveedor reporteProveedor = new Frm_reporteProveedor();
            reporteProveedor.Show();
        }
    }
}
