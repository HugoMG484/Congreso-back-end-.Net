using System;
using System.Collections.Generic;

namespace Congreso.Models;

public partial class Inscripcion
{
    public int Id { get; set; }

    public int? ParticipanteId { get; set; }

    public int? ConferenciaId { get; set; }

    public bool ConfirmacionAsistencia { get; set; }

    public bool AceptaTerminosCondiciones { get; set; }

    public virtual Conferencium? Conferencia { get; set; }

    public virtual Participante? Participante { get; set; }
}
