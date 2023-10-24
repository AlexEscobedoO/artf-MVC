using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace artf_MVC.Models;

public partial class Empresa
{
    public int Idempre { get; set; }

    [Display(Name = "Nombre de la Empresa")]
    public string Rsempre { get; set; } = null!;

    [Display(Name = "Dirección de la Empresa")]
    public string Dirempre { get; set; } = null!;

    [Display(Name = "Tipo de Empresa")]
    public string Tipoempre { get; set; } = null!;

    public virtual ICollection<Equiuni> Equiunis { get; set; } = new List<Equiuni>();

    public virtual ICollection<Insrf> Insrves { get; set; } = new List<Insrf>();

    public virtual ICollection<Solrf> Solrves { get; set; } = new List<Solrf>();
}
