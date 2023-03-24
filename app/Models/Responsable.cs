using System;
using System.Collections.Generic;

namespace TaskManager.Models;

public partial class Responsable
{
    public int IdResponsable { get; set; }

    public string NombreResponsable { get; set; } = null!;

    public string ApellidoResponsable { get; set; } = null!;

    public int? Telefono { get; set; }

    public virtual ICollection<Tarea> Tareas { get; } = new List<Tarea>();

    public string NombreCompletoResponsable => $"{NombreResponsable} {ApellidoResponsable}";
}
