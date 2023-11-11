using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace artf_MVC.Models;

public partial class Equiuni
{
    [Display(Name = "Folio registro equipo")]
    public int Idequi { get; set; }
    [Display(Name = "Nombre de la empresa")]
    public int? Idempreequi { get; set; }
    [Display(Name = "Nombre del fabricante")]
    public int? Idfabequi { get; set; }
    [Display(Name = "Modelo del equipo")]
    public int? Idmodeequi { get; set; }
    [Display(Name = "Folio solicitud")]
    public int? Idsolequi { get; set; }
    [Display(Name = "Folio inscripción")]
    public int? Idinsequi { get; set; }
    [Display(Name = "Folio rectificación")]
    public int? Idrectequi { get; set; }
    [Display(Name = "Folio modificación")]
    public int? Idmodequi { get; set; }
    [Display(Name = "Folio cancelación")]
    public int? Idcanequi { get; set; }

    [Display(Name = "Modalidad equipo")]
    public string Modaequi { get; set; } = null!;

    [Display(Name = "Tipo equipo")]
    public string? Tipequi { get; set; }

    [Display(Name = "Combustible")]
    public string? Combuequi { get; set; }

    [Display(Name = "Potencia HP")]
    public int? Pequi { get; set; }

    [Display(Name = "Número de serie")]
    public string? Nserie { get; set; }

    [Display(Name = "Regimen")]
    public string? Regiequi { get; set; }

    [Display(Name = "Gravamen")]
    public string? Graequi { get; set; }

    [Display(Name = "Uso")]
    public string? Usoequi { get; set; }

    [Display(Name = "Fecha construcción")]
    public int? Fcons { get; set; }

    [Display(Name = "Número de factura")]
    public string? Nofact { get; set; }

    [Display(Name = "Tipo de contrato")]
    public string? Tcontra { get; set; }

    [Display(Name = "Fecha de contrato")]
    public DateTime? Fcontra { get; set; }

    [Display(Name = "Vigencia del contrato")]
    public string? Vcontra { get; set; }

    [Display(Name = "Monto de renta")]
    public int? Mrent { get; set; }
    [Display(Name = "Tipo de moneda")]
    public string? Monrent { get; set; }
    [Display(Name = "Observaciones del arrendamiento")]
    public string? Obsarre { get; set; }
    [Display(Name = "Observaciones del gravamen")]
    public string? Obsgra { get; set; }
    [Display(Name = "Observaciones del equipo")]
    public string? Obsequi { get; set; }
    [Display(Name = "Ficha técnica del equipo")]
    public byte[]? Fichaequi { get; set; } = ObtenerValorPredeterminado();
    [Display(Name = "Fecha de registro del equipo")]
    public DateTime? Fecharequi { get; set; }
    [Display(Name = "Folio cancelación")]
    public virtual Canrf? IdcanequiNavigation { get; set; }
    [Display(Name = "Nombre de la empresa")]
    public virtual Empresa? IdempreequiNavigation { get; set; }
    [Display(Name = "Nombre del fabricante")]
    public virtual Fabricante? IdfabequiNavigation { get; set; }
    [Display(Name = "Folio inscripción")]
    public virtual Insrf? IdinsequiNavigation { get; set; }
    [Display(Name = "Modelo del equipo")]
    public virtual Modelo? IdmodeequiNavigation { get; set; }
    [Display(Name = "Folio modificación")]
    public virtual Modrf? IdmodequiNavigation { get; set; }
    [Display(Name = "Folio rectificación")]
    public virtual Rectrf? IdrectequiNavigation { get; set; }
    [Display(Name = "Folio solicitud")]
    public virtual Solrf? IdsolequiNavigation { get; set; }
    private static byte[] ObtenerValorPredeterminado()
    {
        // Lógica para obtener el valor predeterminado
        return Encoding.UTF8.GetBytes("Default");
    }
}
