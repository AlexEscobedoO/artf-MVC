using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace artf_MVC.Models;

public partial class Equiuni
{
    public int Idequi { get; set; }

    public int? Idempreequi { get; set; }

    public int? Idfabequi { get; set; }

    public int? Idmodeequi { get; set; }

    public int? Idsolequi { get; set; }

    public int? Idinsequi { get; set; }

    public int? Idrectequi { get; set; }

    public int? Idmodequi { get; set; }

    public int? Idcanequi { get; set; }

    [Display(Name = "Modalidad equipo")]
    public string Modaequi { get; set; } = null!;

    [Display(Name = "Tipo equipo")]
    public string? Tipequi { get; set; }

    [Display(Name = "Combustible")]
    public string? Combuequi { get; set; }

    [Display(Name = "Potencia HP")]
    public int? Pequi { get; set; }

    [Display(Name = "Num de serie")]
    public string? Nserie { get; set; }

    [Display(Name = "Regimen")]
    public string? Regiequi { get; set; }

    [Display(Name = "Gravamen")]
    public string? Graequi { get; set; }

    [Display(Name = "Uso")]
    public string? Usoequi { get; set; }

    [Display(Name = "Fecha construcción")]
    public int? Fcons { get; set; }

    [Display(Name = "Num Factura")]
    public string? Nofact { get; set; }

    [Display(Name = "Tipo de contrato")]
    public string? Tcontra { get; set; }

    [Display(Name = "Fecha de contrato")]
    public DateTime? Fcontra { get; set; }

    [Display(Name = "Vigencia del contrato")]
    public string? Vcontra { get; set; }

    [Display(Name = "Monto de renta")]
    public int? Mrent { get; set; }

    public string? Monrent { get; set; }

    public string? Obsarre { get; set; }

    public string? Obsgra { get; set; }

    public string? Obsequi { get; set; }

    public byte[]? Fichaequi { get; set; }

    public DateTime? Fecharequi { get; set; }

    public virtual Canrf? IdcanequiNavigation { get; set; }

    public virtual Empresa? IdempreequiNavigation { get; set; }

    public virtual Fabricante? IdfabequiNavigation { get; set; }

    public virtual Insrf? IdinsequiNavigation { get; set; }

    public virtual Modelo? IdmodeequiNavigation { get; set; }

    public virtual Modrf? IdmodequiNavigation { get; set; }

    public virtual Rectrf? IdrectequiNavigation { get; set; }

    public virtual Solrf? IdsolequiNavigation { get; set; }
}
