using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace artf_MVC.Models;

public partial class Modrf
{
    public int Idmod { get; set; }

    public int? Idrectmod { get; set; }

    public int? Idusermod { get; set; }

    [Display(Name = "Nº de modificación")]
    public string Numacuofmod { get; set; } = null!;

    [Display(Name = "Acuerdo de modificación")]
    public byte[]? Acumod { get; set; }

    [Display(Name = "Fecha de modificación")]
    public DateTime Fechamod { get; set; }

    [Display(Name = "Descripción de modificación")]
    public string Desmod { get; set; } = null!;

    [Display(Name = "Observaciones de modificación")]
    public string Obsmod { get; set; } = null!;

    [Display(Name = "Ficha técnica de modificación")]
    public byte[] Fichatecmod { get; set; } = null!;

    [Display(Name = "Clave de modificación")]
    public string Clavemod { get; set; } = null!;

    public virtual ICollection<Canrf> Canrves { get; set; } = new List<Canrf>();

    public virtual ICollection<Equiuni> Equiunis { get; set; } = new List<Equiuni>();

    [Display(Name = "Id-Rectificación")]
    public virtual Rectrf? IdrectmodNavigation { get; set; }

    [Display(Name = "Id-Usuario")]
    public virtual User? IdusermodNavigation { get; set; }
}
