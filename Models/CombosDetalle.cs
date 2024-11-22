using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CelainyLopez_AP1_P2.Models;

public class CombosDetalle
{
    [Key] 
    public int DetalleId { get; set; }

    [ForeignKey("Combos")]
    public int ComboId { get; set; }
    public Combos? Combos { get; set; }

    [ForeignKey("Articulos")]
    public int ArticuloId { get; set; }
    public Productos? Articulos { get; set; }

    [Range(0.01, int.MaxValue, ErrorMessage = "El precio debe ser mayor que 0")]
    public int Cantidad {  get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "El costo debe ser mayor que 0")]
    public double Costo { get; set; }
}
