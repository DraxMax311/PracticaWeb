using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaWeb.Models
{
    [Table("PrecioProveedores")]
    public class PrecioProveedores
    {
        [Key]
        public int ID_Precio { get; set; }
        public int ID_Producto { get; set; }
        public int ID_Proveedor { get; set; }
        public decimal Precio { get; set; }
        public string? ClaveProveedor { get; set; }
    }
}
