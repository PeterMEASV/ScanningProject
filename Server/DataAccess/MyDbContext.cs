using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public partial class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Activeproduct> Activeproducts { get; set; }

    public virtual DbSet<Producttemplate> Producttemplates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activeproduct>(entity =>
        {
            entity.HasKey(e => e.Ean).HasName("activeproducts_pkey");

            entity.ToTable("activeproducts", "scanningproject");

            entity.Property(e => e.Ean).HasColumnName("ean");
            entity.Property(e => e.Expirydate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expirydate");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Producttemplate>(entity =>
        {
            entity.HasKey(e => e.Ean).HasName("producttemplates_pkey");

            entity.ToTable("producttemplates", "scanningproject");

            entity.Property(e => e.Ean).HasColumnName("ean");
            entity.Property(e => e.Estimatedexpiry).HasColumnName("estimatedexpiry");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
