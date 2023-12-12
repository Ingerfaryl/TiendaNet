using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
using System.Data.SqlClient;
using ENTITIES;

namespace BOL
{
    public class producto
    {
        DBAccess conexion = new DBAccess();

        public DataTable listar()
        {
            return conexion.listarDatos("spu_productos_listar");
        }
        public int registrar(EProduto produto)
        {
            int registros = 0;
            //Llamamos a spu de sql
            SqlCommand comando = new SqlCommand("spu_productos_registrar", conexion.getConexion());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.abrirConexion();
            try
            {
                comando.Parameters.AddWithValue("@idmarca", produto.idmarca);
                comando.Parameters.AddWithValue("@idsubcategoria", produto.subcategoria);
                comando.Parameters.AddWithValue("@descripcion", produto.descripcion);
                comando.Parameters.AddWithValue("@garantia", produto.garantia);
                comando.Parameters.AddWithValue("@precio", produto.precio);
                comando.Parameters.AddWithValue("@stock", produto.stock);

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
    }
    
}
