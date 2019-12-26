using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Almacen.Categoria
{
    public class CategoriaViewModel
    {
        public int idcategoria { get; set; }
        public string nombre { get; set; }
        public string description { get; set; }
        public bool condicion { get; set; }

    }
}
