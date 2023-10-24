using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace artf_MVC.Models;

public partial class Rectrf
{
    public int Idrect { get; set; }

    [Display(Name = "Id inscripción")]
    public int? Idinsrect { get; set; }

    [Display(Name = "Id usuario")]
    public int? Iduserrect { get; set; }

    [Display(Name = "Número de acuerdo")]
    public string Numacuofrect { get; set; } = null!;

    [Display(Name = "Ficha técnica")]
    public byte[]? Fichatecrect { get; set; }

    [Display(Name = "Número de documento")]
    public string Numdocemp { get; set; } = null!;

    [Display(Name = "Fecha de solicitud")]
    public DateTime Fechadocsol { get; set; }

    [Display(Name = "Fecha de acuerdo")]
    public DateTime Fecharect { get; set; }

    [Display(Name = "Descripción")]
    public string? Desrect { get; set; }

    [Display(Name = "Observaciones")]
    public string? Obsrect { get; set; }

    [Display(Name = "Acuerdo")]
    public byte[]? Acurect { get; set; }

    [Display(Name = "Clave")]
    public string Claverect { get; set; } = null!;

    [Display(Name = "Fecha de rectificación")]
    public DateTime Fechamodrect { get; set; }

    public virtual ICollection<Equiuni> Equiunis { get; set; } = new List<Equiuni>();

    [Display(Name = "Id inscripción")]
    public virtual Insrf? IdinsrectNavigation { get; set; }

    [Display(Name = "Id usuario")]
    public virtual User? IduserrectNavigation { get; set; }

    public virtual ICollection<Modrf> Modrves { get; set; } = new List<Modrf>();
}
