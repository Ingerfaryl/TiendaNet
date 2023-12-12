using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BOL;              //lógica
using ENTITIES;         //Estructura
using DESIGNER.Tools;   //Herramienta

namespace DESIGNER
{
    public partial class frmEmpresas : Form
    {
        empresa empresa = new empresa();
        EEmpresa entEmpresa = new EEmpresa();
        DataView Dv;
        int go = 0;
        public frmEmpresas()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnEnviar_Click(object sender, EventArgs e) // cuando presiones enviar, va a validar las siguientes condiciones
        {
            if (txtRazon.Text.Trim() == String.Empty | txtRUC.Text.Trim() ==String.Empty) // si el txt razon social o el ruc está en blanco mostrará el siguiente mensaje 
            {
                errorEmpresa.SetError(txtRazon, "Por favor no deje el campo de Razon social en blanco");
                errorEmpresa.SetError(txtRUC, "Por favor no deje el campo de RUC en blanco");
                go = 1;
            }
            else // de lo contrario pasará lo siguiente
            {
                if (go == 0) // si go es igual a 0
                    {
                    if (Aviso.Preguntar("¿Estás seguro de guardar?") == DialogResult.Yes)
                    {
                        entEmpresa.razonsocial = txtRazon.Text;
                        entEmpresa.RUC = txtRUC.Text;
                        entEmpresa.direccion = txtDireccion.Text.ToString();
                        entEmpresa.telefono = txtTelefono.Text.ToString();
                        entEmpresa.email = txtEmail.Text.ToString();
                        entEmpresa.website = txtWebSite.Text.ToString();

                        if (empresa.registrar(entEmpresa) > 0)
                        {
                            Aviso.informar("Nueva Empresa registrado");
                            reiniciarinterfaz();
                            actualizar();
                        }
                        else
                        {
                            Aviso.advertir("No hemos podido terminar el registro");
                        }
                    }
                }
            }
            
            

        }
        private void reiniciarinterfaz()
        {
            txtRazon.Clear();
            txtRUC.Clear();
            txtDireccion.Clear();
            txtEmail.Clear();
            txtTelefono.Clear();
            txtWebSite.Clear();
        }
        private void actualizar()
        {
            Dv = new DataView(empresa.listrar());
            gridEmpresa.DataSource = Dv;
            gridEmpresa.Refresh();

            //ocultar filas
            gridEmpresa.Columns[0].Visible = false; //ID

            //Tamaño de las filas
            gridEmpresa.Columns[1].Width = 180;//Apellidos
            gridEmpresa.Columns[2].Width = 150;//nombres
            gridEmpresa.Columns[3].Width = 150;//email
            gridEmpresa.Columns[4].Width = 150;//nivel acceso
            gridEmpresa.Columns[5].Width = 150;//nivel acceso
        }

        private void gridEmpresa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmEmpresas_Load(object sender, EventArgs e)
        {
            actualizar();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            Dv.RowFilter = "razonsocial LIKE '%" + buscador.Text + "%' or RUC LIKE '%" + buscador.Text + "%'";
        }

        private void txtRazon_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            reiniciarinterfaz();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
        }
    }
}
