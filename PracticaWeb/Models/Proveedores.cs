using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaWeb.Models
{
    [Table("Proveedores")]
    public class Proveedores
    {
        [Key]
        public int ID_Proveedor { get; set; }
        public String? Nombre { get; set; }
        public String? Descripcion { get; set; }
    }
}
