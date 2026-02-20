using Microsoft.EntityFrameworkCore;
using ValeMonitoramento.Models;

namespace ValeMonitoramento.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Equipamento> Equipamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 1. Garante que o Código do Equipamento seja Único no Banco
        modelBuilder.Entity<Equipamento>()
            .HasIndex(e => e.Codigo)
            .IsUnique();

        // 2. Converte o Enum Tipo para String no Banco (ex: salva "Caminhao" em vez de 0)
        modelBuilder.Entity<Equipamento>()
            .Property(e => e.Tipo)
            .HasConversion<string>()
            .HasMaxLength(50);

        // 3. Converte o Enum Status para String no Banco (ex: salva "Operacional" em vez de 0)
        modelBuilder.Entity<Equipamento>()
            .Property(e => e.StatusOperacional)
            .HasConversion<string>()
            .HasMaxLength(50);

        // 4. Configura o Horímetro para precisão decimal (18 dígitos, 2 decimais)
        modelBuilder.Entity<Equipamento>()
            .Property(e => e.Horimetro)
            .HasPrecision(18, 2);

        // 5. Mapeia DateOnly explicitamente para o tipo 'date' do PostgreSQL
        modelBuilder.Entity<Equipamento>()
            .Property(e => e.DataAquisicao)
            .HasColumnType("date");
            
        base.OnModelCreating(modelBuilder);
    }
}