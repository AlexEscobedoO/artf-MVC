using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace artf_MVC.Models;

public partial class Canrf
{
    [Display(Name = "Folio cancelación")]
    public int Idcan { get; set; }

    [Display(Name = "Folio Modificación")]
    public int? Idmodcan { get; set; }

    [Display(Name = "Id Usuario")]
    public int? Idusercan { get; set; }

    [Display(Name = "Acuerdo de cancelación")]
    public string Numacuofcan { get; set; } = null!;

    [Display(Name = "Fecha de acuerdo de cancelación")]
    public DateTime Fechaofcan { get; set; }

    [Display(Name = "Justificación")]
    public string Juscan { get; set; } = null!;

    [Display(Name = "Observaciones")]
    public string? Obscan { get; set; }

    [Display(Name = "Ficha de cancelación")]
    public byte[] Fichacan { get; set; } = ObtenerValorPredeterminado();//null!;

    [Display(Name = "Clave de cancelación")]
    public string Clavecan { get; set; } = null!;

    [Display(Name = "Fecha de cancelación")]
    public DateTime Fechacan { get; set; }

    public virtual ICollection<Equiuni> Equiunis { get; set; } = new List<Equiuni>();

    [Display(Name = "Folio modificación")]
    public virtual Modrf? IdmodcanNavigation { get; set; }

    [Display(Name = "Id Usuario")]
    public virtual User? IdusercanNavigation { get; set; }
    private static byte[] ObtenerValorPredeterminado()
    {
        // Lógica para obtener el valor predeterminado
        return Encoding.UTF8.GetBytes("Default");
    }
}
