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
            entity.Property(e => e.ImagenCategoria).HasMaxLength(70);
            entity.Property(e => e.NombreCategoria).HasMaxLength(40);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
