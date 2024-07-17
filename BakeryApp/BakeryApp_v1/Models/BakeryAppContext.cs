using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BakeryApp_v1.Models;

public partial class BakeryAppContext : DbContext
{
    public BakeryAppContext()
    {
    }

    public BakeryAppContext(DbContextOptions<BakeryAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Ingrediente> Ingredientes { get; set; }

    public virtual DbSet<Marketing> Marketings { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Receta> Recetas { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Unidadesmedidum> Unidadesmedida { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=127.0.0.1;port=3306;database=BakeryApp;user=Bakery;password=BakeryApp.*");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PRIMARY");

            entity.ToTable("categorias");

            entity.HasIndex(e => e.NombreCategoria, "uq_nombre_categoria").IsUnique();

            entity.Property(e => e.DescripcionCategoria).HasMaxLength(255);
            entity.Property(e => e.ImagenCategoria).HasMaxLength(80);
            entity.Property(e => e.NombreCategoria).HasMaxLength(40);
        });

        modelBuilder.Entity<Ingrediente>(entity =>
        {
            entity.HasKey(e => e.IdIngrediente).HasName("PRIMARY");

            entity.ToTable("ingredientes");

            entity.HasIndex(e => e.UnidadMedidaIngrediente, "fk_unidad_ingrediente");

            entity.HasIndex(e => e.NombreIngrediente, "uk_nombre_ingrediente").IsUnique();

            entity.Property(e => e.DescripcionIngrediente).HasMaxLength(100);
            entity.Property(e => e.FechaCaducidadIngrediente).HasColumnType("date");
            entity.Property(e => e.NombreIngrediente).HasMaxLength(50);
            entity.Property(e => e.PrecioUnidadIngrediente).HasPrecision(10);

            entity.HasOne(d => d.UnidadMedidaIngredienteNavigation).WithMany(p => p.Ingredientes)
                .HasForeignKey(d => d.UnidadMedidaIngrediente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_unidad_ingrediente");

            entity.HasMany(d => d.IdReceta).WithMany(p => p.IdIngredientes)
                .UsingEntity<Dictionary<string, object>>(
                    "Ingredientesreceta",
                    r => r.HasOne<Receta>().WithMany()
                        .HasForeignKey("IdReceta")
                        .HasConstraintName("fk_id_receta_rec"),
                    l => l.HasOne<Ingrediente>().WithMany()
                        .HasForeignKey("IdIngrediente")
                        .HasConstraintName("fk_id_ingrediente_rec"),
                    j =>
                    {
                        j.HasKey("IdIngrediente", "IdReceta").HasName("PRIMARY");
                        j.ToTable("ingredientesrecetas");
                        j.HasIndex(new[] { "IdReceta" }, "fk_id_receta_rec");
                    });
        });

        modelBuilder.Entity<Marketing>(entity =>
        {
            entity.HasKey(e => e.IdMarketing).HasName("PRIMARY");

            entity.ToTable("marketing");

            entity.Property(e => e.Correo).HasMaxLength(80);
            entity.Property(e => e.Nombre).HasMaxLength(25);
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PRIMARY");

            entity.ToTable("personas");

            entity.HasIndex(e => e.IdRol, "per_fk_id_rol");

            entity.HasIndex(e => e.Correo, "uk_correo_per").IsUnique();

            entity.HasIndex(e => e.Telefono, "uk_telefono_per").IsUnique();

            entity.Property(e => e.CodigoRecuperacion).HasMaxLength(20);
            entity.Property(e => e.Contra).HasMaxLength(100);
            entity.Property(e => e.Correo).HasMaxLength(80);
            entity.Property(e => e.Nombre).HasMaxLength(25);
            entity.Property(e => e.PrimerApellido).HasMaxLength(25);
            entity.Property(e => e.SegundoApellido).HasMaxLength(25);
            entity.Property(e => e.Telefono).HasMaxLength(13);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Personas)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("per_fk_id_rol");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PRIMARY");

            entity.ToTable("productos");

            entity.HasIndex(e => e.IdCategoria, "fk_id_Categoria");

            entity.HasIndex(e => e.IdReceta, "fk_id_receta");

            entity.HasIndex(e => e.NombreProducto, "uq_nombre_Producto").IsUnique();

            entity.Property(e => e.DescripcionProducto).HasMaxLength(255);
            entity.Property(e => e.ImagenProducto).HasMaxLength(80);
            entity.Property(e => e.NombreProducto).HasMaxLength(40);
            entity.Property(e => e.PrecioProducto).HasPrecision(10);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("fk_id_Categoria");

            entity.HasOne(d => d.IdRecetaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdReceta)
                .HasConstraintName("fk_id_receta");
        });

        modelBuilder.Entity<Receta>(entity =>
        {
            entity.HasKey(e => e.IdReceta).HasName("PRIMARY");

            entity.ToTable("recetas");

            entity.HasIndex(e => e.NombreReceta, "uk_nombre_receta").IsUnique();

            entity.Property(e => e.NombreReceta).HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.NombreRol).HasMaxLength(20);
        });

        modelBuilder.Entity<Unidadesmedidum>(entity =>
        {
            entity.HasKey(e => e.IdUnidad).HasName("PRIMARY");

            entity.ToTable("unidadesmedida");

            entity.Property(e => e.NombreUnidad).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
