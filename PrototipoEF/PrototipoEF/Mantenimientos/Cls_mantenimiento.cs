using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrototipoEF.Mantenimientos
{
    public class Cls_mantenimiento
    {
        /*
         ID DE TABLAS:
            1 = cuenta_corriente

        ORDEN DE LOS DATOS EN RETURN PARA datos:
            1 = alias
            2 = ayuda
            3 = tabla
            4 = form
            5 = nombre
            6 = noForaneas
            
        ORDEN DE LOS DATOS EN RETURN PARA foraneas:
            1 = tabla
            2 = campo
            3 = modo
             */


        public (string[], string, string, string, string, int) datos(int tabla)
        {
            switch (tabla)
            {
                case 1:
                    string[] alias1 = { "Codigo", "Nombre", "Monto Restante", "Estado" };
                    return (alias1, "1", "cuenta_corriente", "de Cuentas Corrientes", "CUNETA CORRIENTE", 0);

                default:
                    MessageBox.Show("Error al identificar el mantenimiento a trabajar.");

                    break;
            }
            return (null, null, null, null, null, 0);
        }
        
        public (string, string, int) foraneas(int tabla, int no)
        {
            switch (tabla)
            {
                //proveedores
                case 2:
                    switch (no)
                    {
                        case 1:
                            return ("contactos", "nombre_contacto", 1);
                        default:
                            MessageBox.Show("Error al identificar el mantenimiento a trabajar.");

                            break;
                    }
                    break;

                default:
                    MessageBox.Show("Error al tomar los datos relacionados al mantenimiento a trabajar.");

                    break;
            }
            return ("", "", 0);
        }
    
    }
}
