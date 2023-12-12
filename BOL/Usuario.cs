using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ENTITIES;
using DAL; //Acceso a conexion
using System.Data; // Objetos manejados datos
using System.Data.SqlClient; //Acceso MSSQL Server
namespace BOL
{
    //Clase publica
    public class Usuario
    {
        //instancia de la clase de conexion 
        DBAccess conexion = new DBAccess();
        /// <summary>
        /// Inicia sesión utilizando datos del servidor
        /// </summary>
        /// <param name="email">
        /// identificar o nombre de usuario
        /// </param>
        /// <returns>
        /// Objeto DataTable conteniendo toda la fila (varios campos)
        /// </returns>
        public DataTable iniciarSesion(string email)
        {
            //1) Crear un objeto que contendrá el resultado
            DataTable dt = new DataTable();
            //2) Abrir conexion
            conexion.abrirConexion();
            //3) Objeto para enviar consulta
            SqlCommand comando = new SqlCommand("spu_usuarios_login", conexion.getConexion());
            //4) Tipo de Comando (procedimiento almacenado)
            comando.CommandType = CommandType.StoredProcedure;
            //5) Pasar las variables
            comando.Parameters.AddWithValue("@email",email);
            //6) Ejecutar y obterner los datos
            dt.Load(comando.ExecuteReader());
            //7. Cerrar conoexion
            conexion.cerrarConexion();
            //8. Returnamos el objeto con la info
            return dt;
        }
        public DataTable login(string email) // hace lo mismo que la función iniciar sesion
        {
            return conexion.listardatosVariable("spu_usuarios_login", "@email", email);
        }
        public int registrar(EUsuario entidad)
        {
            int totalregistros = 0;
            SqlCommand comando = new SqlCommand("spu_usuarios_registrar", conexion.getConexion());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.abrirConexion();
            try
            {
                comando.Parameters.AddWithValue("@apellidos",entidad.apellidos);
                comando.Parameters.AddWithValue("@nombres",entidad.nombres);
                comando.Parameters.AddWithValue("@email",entidad.email);
                comando.Parameters.AddWithValue("@claveacceso",entidad.claveacceso);
                comando.Parameters.AddWithValue("@nivelacceso",entidad.nivelacceso);

                totalregistros = comando.ExecuteNonQuery();
            }
            catch
            {
                totalregistros = -1;
            }
            finally
            {
                conexion.cerrarConexion();
            }
            return totalregistros;
        }
        public DataTable listar()
        {
            DataTable dt = new DataTable();
            SqlCommand comando = new SqlCommand("spu_usuarios_listar", conexion.getConexion());
            comando.CommandType = CommandType.StoredProcedure;

            conexion.abrirConexion();
            dt.Load(comando.ExecuteReader());
            conexion.cerrarConexion();

            return dt;
        }
    }
}
