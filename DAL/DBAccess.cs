﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Configuration;

namespace DAL
{
    public class DBAccess
    {
        private SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["AccesoTiendaNET"].ConnectionString);
        public SqlConnection getConexion()
        {
            return this.conexion;
        }

        public void abrirConexion()
        {
            if (this.conexion.State == ConnectionState.Closed)
            {
                this.conexion.Open();
            }
        }
        public void cerrarConexion()
        {
            if (this.conexion.State == ConnectionState.Open)
            {
                this.conexion.Close();
            }
        }
        /// <summary>
        /// Es un método general que retorna una colección de datos de una consulta
        /// que no tiene variables de entrada
        /// </summary>
        /// <param name="spu"> Nombre del procedimiento almacenado</param>
        /// <returns>Colección de datos de tipo DataTAble</returns>
        public DataTable listarDatos(string spu)
        {
            DataTable dt = new DataTable();
            this.abrirConexion();
            SqlCommand comando = new SqlCommand(spu, this.getConexion());
            comando.CommandType = CommandType.StoredProcedure;
            dt.Load(comando.ExecuteReader());
            this.cerrarConexion();
            return dt;
        }
        public DataTable listardatosVariable(string spu, string nombreVariable, object ValorVariable)
        {
            DataTable dt = new DataTable();
            this.abrirConexion();
            SqlCommand comando = new SqlCommand(spu, this.getConexion());
            comando.CommandType = CommandType.StoredProcedure;
            //Agregar parámetro de entrada (variable)
            comando.Parameters.AddWithValue(nombreVariable,ValorVariable);
            dt.Load(comando.ExecuteReader());
            this.cerrarConexion();
            return dt;
        }
    }
}
