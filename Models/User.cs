using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace artf_MVC.Models;

public class User
{
    public int Iduser { get; set; }

    [Display(Name = "Tipo de usuario")]
    public string Tipouser { get; set; } = null!;
    [Display(Name = "Nombre de usuario")]
    public string Nomuser { get; set; } = null!;
    [Display(Name = "Primer Apellido")]
    public string Pauser { get; set; } = null!;
    [Display(Name = "Segundo Apellido")]
    public string? Sauser { get; set; }
    [Display(Name = "Correo electrónico")]
    public string Mailuser { get; set; } = null!;
    [Display(Name = "Contraseña")]
    public string Passuser { get; set; } = null!;

    public virtual ICollection<Canrf> Canrves { get; set; } = new List<Canrf>();

    public virtual ICollection<Insrf> Insrves { get; set; } = new List<Insrf>();

    public virtual ICollection<Modrf> Modrves { get; set; } = new List<Modrf>();

    public virtual ICollection<Rectrf> Rectrves { get; set; } = new List<Rectrf>();

    public virtual ICollection<Solrf> Solrves { get; set; } = new List<Solrf>();
}
