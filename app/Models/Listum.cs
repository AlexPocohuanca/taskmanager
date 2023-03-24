using System;
using System.Collections.Generic;

namespace TaskManager.Models;

public partial class Listum
{
    public int IdLista { get; set; }

    public string NombreLista { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public virtual ICollection<Tarea> Tareas { get; } = new List<Tarea>();
}
