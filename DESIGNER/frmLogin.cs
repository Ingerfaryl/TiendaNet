using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//incluir librerías
using BOL;
using CryptSharp; //Manejar claves encriptadas 
using DESIGNER.Tools;


namespace DESIGNER
{
    public partial class frmLogin : Form
    {
        //Instancia de la clase
        Usuario usuario = new Usuario();
        DataTable dtRpta = new DataTable();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void login()
        {
            if (txtEmail.Text.Trim() == String.Empty) //El trim quita espacios en blancos
            {
                errorLogin.SetError(txtEmail, "Por favor ingrese un email"); //para evitar un error y colocas un anuncio
            }
            else
            {
                errorLogin.Clear();
                if (txtClaveAcceso.Text.Trim() == String.Empty)
                {
                    errorLogin.SetError(txtClaveAcceso, "Escriba su contraseña");
                    txtClaveAcceso.Focus();
                }
                else
                {
                    errorLogin.Clear();
                    //Los datos ingresados fueron validades en la base de datos
                    dtRpta = usuario.iniciarSesion(txtEmail.Text);
                    if (dtRpta.Rows.Count > 0)
                    {
                        string claveEncriptada = dtRpta.Rows[0][4].ToString();
                        string apellidos = dtRpta.Rows[0][1].ToString();
                        string nombres = dtRpta.Rows[0][2].ToString();
                        if (Crypter.CheckPassword(txtClaveAcceso.Text, claveEncriptada))
                        { 
                            Aviso.informar($"Bienvenido {apellidos}, {nombres}");
                            frmMain formMain = new frmMain();
                            formMain.Show();
                            this.Hide();
                        }
                        else
                        {
                            Aviso.advertir("Error en la contraseña");
                        }
                    }
                    else
                    {
                        MessageBox.Show("El usuario ingresado no existe");
                    }

                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //txtClaveAcceso.Text = Crypter.Blowfish.Crypt("SENATI123");
            //return;
            login();

        }

        private void txtClaveAcceso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                login();
            }
        }
    }
}
