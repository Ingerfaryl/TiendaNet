using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DESIGNER.Tools
{
    public static class Aviso //El static hace que puedas llamar a la clase sin problemas, así como un messageBox
    {
        public static void informar(string mensaje)
        {
            MessageBox.Show(mensaje,"App TiendaNET", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        public static void advertir(string mensaje)
        {
            MessageBox.Show(mensaje, "App TiendaNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static DialogResult Preguntar(string mensaje)
        {
            return MessageBox.Show(mensaje, "App TiendaNET", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}
