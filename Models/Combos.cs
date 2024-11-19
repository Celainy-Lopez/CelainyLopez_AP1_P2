using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CelainyLopez_AP1_P2.Models;

public class Combos
{
    [Key]
    public int ComboId { get; set; }
    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "Por favor ingrese una descripción valida")]
    public string Descripcion { get; set; }


    [Required(ErrorMessage = "Ingrese un precio valido")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0")]
    public double Precio { get; set; }

    public bool Vendido {  get; set; }

    [ForeignKey("ComboId")]
    public ICollection<CombosDetalle> CombosDetalles { get; set;} = new List<CombosDetalle>();
}
