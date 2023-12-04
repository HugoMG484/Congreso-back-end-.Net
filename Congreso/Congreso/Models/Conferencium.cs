using System;
using System.Collections.Generic;

namespace Congreso.Models;

public partial class Conferencium
{
    public int Id { get; set; }

    public string Horario { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Conferencista { get; set; } = null!;

    public string? Registro { get; set; }

    public virtual ICollection<Inscripcion> Inscripcions { get; } = new List<Inscripcion>();
}
