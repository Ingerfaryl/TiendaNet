using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES
{
    public class EProduto
    {
        public int idproducto { get; set; }
        public int idmarca { get; set; }
        public string descripcion { get; set; }
        public int subcategoria { get; set; }
        public int garantia { get; set; }
        public double precio { get; set; }
        public int stock { get; set; }
    }
}
