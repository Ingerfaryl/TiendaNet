using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptSharp;
using BOL;              //lógica
using ENTITIES;         //Estructura
using DESIGNER.Tools;   //Herramientas

namespace DESIGNER.modulos
{
    public partial class frmUsuario : Form
    {
        //el objeto usuario contiene la lógica 
        Usuario usuario = new Usuario();
        //El objeto entTusuario contiene los datos 
        EUsuario entUsuario = new EUsuario();
        string nivelAcceso = "INV";
        //Reservado un espacio de memoria para el objeto
        DataView dv;
        public frmUsuario()
        {
            InitializeComponent();
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            actualizarDatos();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if

            if (Aviso.Preguntar("¿Estás seguro de guardar?") == DialogResult.Yes)
            {
                string claveEncriptada = Crypter.Blowfish.Crypt(txtClave.Text.Trim());
                entUsuario.apellidos = txApellidos.Text;
                entUsuario.nombres = txtNombres.Text;
                entUsuario.email = txtEmail.Text;
                entUsuario.claveacceso = claveEncriptada;
                entUsuario.nivelacceso = nivelAcceso;

                if (usuario.registrar(entUsuario) >0)
                {
                    Aviso.informar("Nuevo usuario registrado");
                    reiniciarinterfaz();
                    actualizarDatos(); // actualiza los datos del datagridview
                }
                else
                {
                    Aviso.advertir("No hemos podido terminar el registro");
                }
                
                
            }
        }
        private void actualizarDatos()
        {
            dv = new DataView(usuario.listar());
            gridUsuarios.DataSource = dv;
            gridUsuarios.Refresh();

            //ocultar filas
            gridUsuarios.Columns[0].Visible = false; //ID
            gridUsuarios.Columns[4].Visible = false; //Clave

            //Tamaño de las filas
            gridUsuarios.Columns[1].Width = 180;//Apellidos
            gridUsuarios.Columns[2].Width = 150;//nombres
            gridUsuarios.Columns[3].Width = 150;//email
            gridUsuarios.Columns[5].Width = 150;//nivel acceso

            //filas alternadas
            gridUsuarios.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;

        }
        private void reiniciarinterfaz()
        {
            txtNombres.Clear();
            txApellidos.Clear();
            txtClave.Clear();
            txtEmail.Clear();
            optInvitado.Checked = true;
            nivelAcceso = "INV";
            txApellidos.Focus();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void optAdministrador_CheckedChanged(object sender, EventArgs e)
        {
            nivelAcceso = "ADM";
        }

        private void optInvitado_CheckedChanged(object sender, EventArgs e)
        {
            nivelAcceso = "INV";
        }

        private void gridUsuarios_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            gridUsuarios.ClearSelection();
        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            dv.RowFilter = "nombres LIKE '%" + txtBuscar.Text + "%' or apellidos LIKE '%" + txtBuscar.Text + "%'"; //buscador de nombres
        }

        private void gridUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
