using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaWeb.Models
{
    public class Productos
    {
        [Key]
        [Display(Name = "ID")]
        public int ID_Producto { get; set; }
        public String? Clave { get; set; }
        public bool Activo { get; set; }
        public Decimal Precio { get; set; }
        [Display(Name = "Clave Proveedor")]
        public String? ClaveProveedor { get; set; }
        [Display(Name = "Precio Proveedor")]
        public Decimal PrecioProveedor { get; set; }
        [Display(Name = "Tipo de Producto")]
        public int TipoProducto { get; set; }
    }
}
