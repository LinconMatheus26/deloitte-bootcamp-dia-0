using Microsoft.EntityFrameworkCore;
using MinhaApi.Models;

namespace MinhaApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<LoteMinerio> LotesMinerio => Set<LoteMinerio>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<LoteMinerio>(e =>
            {
                e.ToTable("lotes_minerio");
                e.HasKey(x => x.Id);

                // Mapeamento explícito para snake_case (idêntico ao seu banco)
                e.Property(x => x.Id).HasColumnName("id");
                e.Property(x => x.CodigoLote).HasColumnName("codigo_lote").IsRequired();
                e.Property(x => x.MinaOrigem).HasColumnName("mina_origem").IsRequired();
                e.Property(x => x.TeorFe).HasColumnName("teor_fe").HasColumnType("numeric(5,2)");
                e.Property(x => x.Umidade).HasColumnName("umidade").HasColumnType("numeric(5,2)");
                e.Property(x => x.SiO2).HasColumnName("sio2").HasColumnType("numeric(5,2)");
                e.Property(x => x.P).HasColumnName("p").HasColumnType("numeric(5,3)");
                e.Property(x => x.Toneladas).HasColumnName("toneladas").HasColumnType("numeric(12,3)");
                e.Property(x => x.DataProducao).HasColumnName("data_producao");
                e.Property(x => x.Status).HasColumnName("status").HasConversion<int>();
                e.Property(x => x.LocalizacaoAtual).HasColumnName("localizacao_atual");

                e.HasIndex(x => x.CodigoLote).IsUnique();
            });
        }
    }
}