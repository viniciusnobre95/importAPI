using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ImportApi.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblArquivo> TblArquivos { get; set; }
        public virtual DbSet<TblRegistro> TblRegistros { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=SERVER;Database=ImportDB;User Id=import;Password=import123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<TblArquivo>(entity =>
            {
                entity.Property(e => e.ArquivoId).ValueGeneratedNever();
            });

            modelBuilder.Entity<TblRegistro>(entity =>
            {
                entity.HasKey(e => new { e.ArquivoId, e.Tconst });

                entity.Property(e => e.Tconst).IsUnicode(false);

                entity.Property(e => e.EndYear).IsUnicode(false);

                entity.Property(e => e.Genres).IsUnicode(false);

                entity.Property(e => e.OriginalTitle).IsUnicode(false);

                entity.Property(e => e.PrimaryTitle).IsUnicode(false);

                entity.Property(e => e.RuntimeMinutes).IsUnicode(false);

                entity.Property(e => e.StartYear).IsUnicode(false);

                entity.Property(e => e.TitleType).IsUnicode(false);

                entity.HasOne(d => d.Arquivo)
                    .WithMany(p => p.TblRegistros)
                    .HasForeignKey(d => d.ArquivoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblRegistro_tblArquivo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
