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
    public class empresa
    {
        DBAccess conexion = new DBAccess();
        public int registrar(EEmpresa empresa)
        {
            int registros = 0;
            SqlCommand comando = new SqlCommand("spu_empresas_registrar", conexion.getConexion()); //Llamamos a spu de sql
            comando.CommandType = CommandType.StoredProcedure;
            conexion.abrirConexion();
            try
            {
                comando.Parameters.AddWithValue("@razonsocial", empresa.razonsocial);
                comando.Parameters.AddWithValue("@RUC", empresa.RUC);
                comando.Parameters.AddWithValue("@direccion", empresa.direccion);
                comando.Parameters.AddWithValue("@telefono", empresa.telefono);
                comando.Parameters.AddWithValue("@email", empresa.email);
                comando.Parameters.AddWithValue("@website", empresa.website);

                registros = comando.ExecuteNonQuery();
            }
            catch
            {
                registros = -1;
            }
            finally
            {
                conexion.cerrarConexion();
            }
            return registros;

        }
        public DataTable listrar()
        {
            DataTable dt = new DataTable();
            SqlCommand comando = new SqlCommand("spu_empresas_listar", conexion.getConexion());
            comando.CommandType = CommandType.StoredProcedure;

            conexion.abrirConexion();
            dt.Load(comando.ExecuteReader());
            conexion.cerrarConexion();

            return dt;

        }
    }
}
