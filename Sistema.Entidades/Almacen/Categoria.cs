using System.ComponentModel.DataAnnotations;

namespace Sistema.Entidades.Almacen
{
    public class Categoria
    {
        public int Idcategoria { get; set; }

        [Required]
        [StringLength(50,MinimumLength =3,ErrorMessage ="El nombre debe tener maximo 50 caracteres y minimo 3")]
        public string Nombre { get; set; }

        [StringLength(256)]
        public string Descripcion { get; set; }
        public bool Condicion { get; set; }
    }
}
