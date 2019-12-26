using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Almacen.Categoria
{
    public class ActualizarViewModel
    {
        [Required]
        public int idcategoria { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener maximo 50 caracteres y minimo 3")]
        public string nombre { get; set; }
        public string description { get; set; }
        public bool condicion { get; set; }
    }
}
