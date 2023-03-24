using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Models;

public partial class TaskmanagerContext : DbContext
{
    public TaskmanagerContext()
    {
    }

    public TaskmanagerContext(DbContextOptions<TaskmanagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Listum> Lista { get; set; }

    public virtual DbSet<Responsable> Responsables { get; set; }

    public virtual DbSet<Tarea> Tareas { get; set; }

 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Listum>(entity =>
        {
            entity.HasKey(e => e.IdLista).HasName("PRIMARY");

            entity.ToTable("lista");

            entity.Property(e => e.IdLista)
                .HasColumnType("int(11)")
                .HasColumnName("idLista");
            entity.Property(e => e.NombreLista)
                .HasMaxLength(50)
                .HasColumnName("nombreLista");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Responsable>(entity =>
        {
            entity.HasKey(e => e.IdResponsable).HasName("PRIMARY");

            entity.ToTable("responsable");

            entity.Property(e => e.IdResponsable)
                .HasColumnType("int(11)")
                .HasColumnName("idResponsable");
            entity.Property(e => e.ApellidoResponsable)
                .HasMaxLength(50)
                .HasColumnName("apellidoResponsable");
            entity.Property(e => e.NombreResponsable)
                .HasMaxLength(50)
                .HasColumnName("nombreResponsable");
            entity.Property(e => e.Telefono)
                .HasColumnType("int(11)")
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasKey(e => e.IdTarea).HasName("PRIMARY");

            entity.ToTable("tarea");

            entity.HasIndex(e => e.IdLista, "idLista");

            entity.HasIndex(e => e.IdResponsable, "idResponsable");

            entity.Property(e => e.IdTarea)
                .HasColumnType("int(11)")
                .HasColumnName("idTarea");
            entity.Property(e => e.DescTarea)
                .HasMaxLength(50)
                .HasColumnName("descTarea");
            entity.Property(e => e.Dificultad)
                .HasColumnType("int(11)")
                .HasColumnName("dificultad");
            entity.Property(e => e.IdLista)
                .HasColumnType("int(11)")
                .HasColumnName("idLista");
            entity.Property(e => e.IdResponsable)
                .HasColumnType("int(11)")
                .HasColumnName("idResponsable");
            entity.Property(e => e.NombreTarea)
                .HasMaxLength(50)
                .HasColumnName("nombreTarea");
            entity.Property(e => e.Tiempo)
                .HasColumnType("int(11)")
                .HasColumnName("tiempo");

            entity.HasOne(d => d.IdListaNavigation).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.IdLista)
                .HasConstraintName("tarea_ibfk_1");

            entity.HasOne(d => d.IdResponsableNavigation).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.IdResponsable)
                .HasConstraintName("tarea_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
