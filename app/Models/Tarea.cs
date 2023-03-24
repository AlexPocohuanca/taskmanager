using System;
using System.Collections.Generic;

namespace TaskManager.Models;

public partial class Tarea
{
    public int IdTarea { get; set; }

    public int? IdLista { get; set; }

    public string NombreTarea { get; set; } = null!;

    public string DescTarea { get; set; } = null!;

    public int? Dificultad { get; set; }

    public int? IdResponsable { get; set; }

    public int? Tiempo { get; set; }

    public virtual Listum? IdListaNavigation { get; set; }

    public virtual Responsable? IdResponsableNavigation { get; set; }
}
