using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaWeb.Models
{
    [Table("Productos")]
    public class Productos
    {
        [Key]
        [Display(Name = "ID")]
        public int ID_Producto { get; set; }
        public String? Nombre { get; set; }
        public String? Clave { get; set; }
        public bool Activo { get; set; }
        public Decimal Precio { get; set; }
        [Display(Name = "Tipo de Producto")]
        public int TipoProducto { get; set; }
    }
}
