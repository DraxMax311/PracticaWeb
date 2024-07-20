using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaWeb.Models
{
    [Table("TipoProductos")]
    public class TipoProductos
    {
        [Key]
        [Display(Name = "ID")]
        public int ID_TipoProducto { get; set; }
        public String? Nombre { get; set; }
        public String? Descripcion { get; set; }
    }
}
