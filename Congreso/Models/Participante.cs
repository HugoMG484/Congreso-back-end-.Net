using System;
using System.Collections.Generic;

namespace Congreso.Models;

public partial class Participante
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Twitter { get; set; }

    public string? Avatar { get; set; }

    public string? Perfil { get; set; }

    public virtual ICollection<Inscripcion> Inscripcions { get; } = new List<Inscripcion>();
}
