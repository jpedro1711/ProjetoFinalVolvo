using Microsoft.EntityFrameworkCore;

namespace ConcessionariaAPI.Models
{
    public class ConcessionariaContext : DbContext
    {
        public DbSet<Acessorio> Acessorio { get; set; } = null;
        public DbSet<Despesa> Despesa { get; set; } = null;
        public DbSet<Endereco> Endereco { get; set; } = null;        
        public DbSet<Proprietario> Proprietario { get; set; } = null;
        public DbSet<Telefone> Telefone { get; set; } = null;
        public DbSet<Veiculo> Veiculo { get; set; } = null;
        public DbSet<Venda> Venda { get; set; } = null;
        public DbSet<Vendedor> Vendedor { get; set; } = null;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-P8BTRSBI\SQLEXPRESS;Database=DB_CONCESSIONARIA;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Despesa>()
                   .Property(e => e.Valor)
                   .HasPrecision(8, 2);
            modelBuilder.Entity<Veiculo>()
                   .HasIndex(u => u.NumeroChassi)
                   .IsUnique();
            modelBuilder.Entity<Veiculo>()
                   .Property(e => e.Valor)
                   .HasPrecision(8, 2);
            modelBuilder.Entity<Vendedor>()
                   .Property(e => e.SalarioBase)
                   .HasPrecision(6, 2);
            modelBuilder.Entity<Vendedor>()
                .Property(e => e.SalarioBase)
                .HasDefaultValue(1412.0);
            
        }
    }
}
