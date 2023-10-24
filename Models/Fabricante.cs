using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace artf_MVC.Models;

public partial class Fabricante
{
    public int Idfab { get; set; }

    [Display(Name = "Nombre del Fabricante")]
    public string Rsfab { get; set; } = null!;

    [Display(Name = "Marca del Fabricante")]
    public string Marfab { get; set; } = null!;

    public virtual ICollection<Equiuni> Equiunis { get; set; } = new List<Equiuni>();
}
