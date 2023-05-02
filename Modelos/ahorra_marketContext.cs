using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ahorra_marcket.Modelos
{
    public partial class ahorra_marketContext : DbContext
    {
        public ahorra_marketContext()
        {
        }

        public ahorra_marketContext(DbContextOptions<ahorra_marketContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Perfile> Perfiles { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<ProductoTiendum> ProductoTienda { get; set; } = null!;
        public virtual DbSet<Tiendum> Tienda { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<UsuariosPerfile> UsuariosPerfiles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;Database=ahorra_marcket; User Id=global_user; Password=Ahorra2022Agt89*");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Perfile>(entity =>
            {
                entity.HasKey(e => e.IdPerfil)
                    .HasName("PK__perfiles__1D1C876888478452");

                entity.ToTable("perfiles");

                entity.Property(e => e.IdPerfil).HasColumnName("id_perfil");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__producto__FF341C0DAF676F47");

                entity.ToTable("producto");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.FechaIng)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ing")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<ProductoTiendum>(entity =>
            {
                entity.HasKey(e => e.IdProductoTienda)
                    .HasName("PK__producto__572D666D64E5B2E6");

                entity.ToTable("producto_tienda");

                entity.Property(e => e.IdProductoTienda).HasColumnName("id_producto_tienda");

                entity.Property(e => e.FechaIng)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ing")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.IdTienda).HasColumnName("id_tienda");

                entity.Property(e => e.Precio).HasColumnName("precio");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.ProductoTienda)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__producto___id_pr__2B3F6F97");

                entity.HasOne(d => d.IdTiendaNavigation)
                    .WithMany(p => p.ProductoTienda)
                    .HasForeignKey(d => d.IdTienda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__producto___id_ti__2A4B4B5E");
            });

            modelBuilder.Entity<Tiendum>(entity =>
            {
                entity.HasKey(e => e.IdTienda)
                    .HasName("PK__tienda__7C49D7365528BE8C");

                entity.ToTable("tienda");

                entity.Property(e => e.IdTienda).HasColumnName("id_tienda");

                entity.Property(e => e.FechaIng)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ing")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__usuarios__D2D146374157ACB2");

                entity.ToTable("usuarios");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.FechaIng)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ing")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Pass)
                    .HasColumnType("text")
                    .HasColumnName("pass");
            });

            modelBuilder.Entity<UsuariosPerfile>(entity =>
            {
                entity.HasKey(e => e.IdUserPerf)
                    .HasName("PK__usuarios__2A4E95F4DDE0A13A");

                entity.ToTable("usuarios_perfiles");

                entity.Property(e => e.IdUserPerf).HasColumnName("id_user_perf");

                entity.Property(e => e.IdPerfil).HasColumnName("id_perfil");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany(p => p.UsuariosPerfiles)
                    .HasForeignKey(d => d.IdPerfil)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__usuarios___id_pe__33D4B598");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UsuariosPerfiles)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__usuarios___id_us__34C8D9D1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
