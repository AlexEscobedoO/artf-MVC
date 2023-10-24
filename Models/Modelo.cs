using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace artf_MVC.Models;

public partial class Modelo
{
    public int Idmod { get; set; }

    [Display(Name = "Modelo del equipo")]
    public string Modequi { get; set; } = null!;

    public virtual ICollection<Equiuni> Equiunis { get; set; } = new List<Equiuni>();
}
