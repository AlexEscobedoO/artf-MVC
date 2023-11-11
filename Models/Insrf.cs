using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace artf_MVC.Models;

public partial class Insrf
{
    [Display(Name = "Folio inscripción")]
    public int Idins { get; set; }
    [Display(Name = "Nombre de la empresa")]
    public int? Idempins { get; set; }
    [Display(Name = "Folio solicitud")]
    public int? Idsolins { get; set; }

    public int? Iduserins { get; set; }

    [Display(Name = "Número de acuerdo")]
    public string Numacuofins { get; set; } = null!;

    [Display(Name = "Observaciones")]
    public string? Obsins { get; set; }

    [Display(Name = "Fecha de captura")]
    public DateTime? Fecapins { get; set; }

    [Display(Name = "Documentos")]
    public byte[]? Docins { get; set; }

    public virtual ICollection<Equiuni> Equiunis { get; set; } = new List<Equiuni>();

    [Display(Name = "Empresa")]
    public virtual Empresa? IdempinsNavigation { get; set; }

    [Display(Name = "Folio solicitud")]
    public virtual Solrf? IdsolinsNavigation { get; set; }

    [Display(Name = "Usuario")]
    public virtual User? IduserinsNavigation { get; set; }

    public virtual ICollection<Rectrf> Rectrves { get; set; } = new List<Rectrf>();
}
