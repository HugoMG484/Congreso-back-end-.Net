using System;
using System.Collections.Generic;
using Congreso.Models;
using Microsoft.EntityFrameworkCore;

namespace Congreso.Context;

public partial class CongresoContext : DbContext
{
    /*
    public CongresoContext()
    {
    }
    */

    public CongresoContext(DbContextOptions<CongresoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Conferencium> Conferencia { get; set; }

    public virtual DbSet<Inscripcion> Inscripcions { get; set; }

    public virtual DbSet<Participante> Participantes { get; set; }

    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAP\\SQLEXPRESS;Database=Congreso;User Id=sa;Password=asd;MultipleActiveResultSets=true;TrustServerCertificate=true");
    */

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Conferencium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__conferen__3213E83FEB933A77");

            entity.ToTable("conferencia");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Conferencista)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("conferencista");
            entity.Property(e => e.Horario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("horario");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Registro)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("registro");
        });

        modelBuilder.Entity<Inscripcion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__inscripc__3213E83F2A85A8E2");

            entity.ToTable("inscripcion");

            entity.HasIndex(e => new { e.ParticipanteId, e.ConferenciaId }, "UQ__inscripc__A80E5D2D56C6768C").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AceptaTerminosCondiciones).HasColumnName("acepta_terminos_condiciones");
            entity.Property(e => e.ConferenciaId).HasColumnName("conferencia_id");
            entity.Property(e => e.ConfirmacionAsistencia).HasColumnName("confirmacion_asistencia");
            entity.Property(e => e.ParticipanteId).HasColumnName("participante_id");

            entity.HasOne(d => d.Conferencia).WithMany(p => p.Inscripcions)
                .HasForeignKey(d => d.ConferenciaId)
                .HasConstraintName("FK__inscripci__confe__4D94879B");

            entity.HasOne(d => d.Participante).WithMany(p => p.Inscripcions)
                .HasForeignKey(d => d.ParticipanteId)
                .HasConstraintName("FK__inscripci__parti__4CA06362");
        });

        modelBuilder.Entity<Participante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__particip__3213E83FBE7C1F8B");

            entity.ToTable("participante");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellidos");
            entity.Property(e => e.Avatar)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("avatar");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Perfil)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("perfil");
            entity.Property(e => e.Twitter)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("twitter");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
