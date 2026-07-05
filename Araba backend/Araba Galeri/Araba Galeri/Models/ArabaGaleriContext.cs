using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Araba_Galeri.Models;

namespace Araba_Galeri.Models;

public partial class ArabaGaleriContext : DbContext
{
    public ArabaGaleriContext()
    {
    }

    public ArabaGaleriContext(DbContextOptions<ArabaGaleriContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-8EVFM1Q\\SQLSERVER;Database=ArabaGaleri;User ID=sa;Password=123456;Encrypt=False;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Kullanicilar>(entity =>
        {
            entity.ToTable("Kullanicilar");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Ad).HasMaxLength(50);
            entity.Property(e => e.Eposta)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Sifre)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<Araba_Galeri.Models.Kullanicilar> Kullanicilar { get; set; } = default!;
}
