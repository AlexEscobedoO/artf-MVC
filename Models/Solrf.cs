using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace artf_MVC.Models;

public partial class Solrf
{
    [Display(Name = "Folio solicitud")]
    public int Idsol { get; set; }
    [Display(Name = "Nombre de la Empresa")]
    public int? Idempsol { get; set; }

    public int? Idusersol { get; set; }

    [Display(Name = "Número de acuerdo")]
    public string Numacuofsol { get; set; } = null!;

    [Display(Name = "Observaciones")]
    public string? Obssol { get; set; }

    [Display(Name = "Fecha de solicitud")]
    public DateTime? Fecapsol { get; set; }

    [Display(Name = "Documento")]
    public byte[]? Docsol { get; set; }

    public virtual ICollection<Equiuni> Equiunis { get; set; } = new List<Equiuni>();

    public virtual Empresa? IdempsolNavigation { get; set; }

    public virtual User? IdusersolNavigation { get; set; }
    public virtual ICollection<Insrf> Insrves { get; set; } = new List<Insrf>();
}
