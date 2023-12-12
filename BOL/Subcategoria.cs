using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BOL
{
    public class Subcategoria
    {
        DBAccess conexion = new DBAccess();
        public DataTable listar(int idcategoria)
        {
            return conexion.listardatosVariable("spu_subcategoria_listar","@idcategoria",idcategoria);
        }
    }
}
