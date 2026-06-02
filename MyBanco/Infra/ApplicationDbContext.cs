using Microsoft.EntityFrameworkCore;
using MyBanco.Models;

namespace MyBanco.Infra;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> 
    options) : DbContext(options)
{
    public DbSet<CarteiraEntity> Wallets { get; set; } 
    
    public DbSet<TransferenciaEntity> Transfers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CarteiraEntity>()
            .HasIndex(c => new { c.CPFCNPJ, c.Email })
            .IsUnique();
        
        modelBuilder.Entity<CarteiraEntity>()
            .Property(c => c.SaldoConta)
            .HasColumnType("decimal(18,2)");
        
        modelBuilder.Entity<CarteiraEntity>()
            .Property(c => c.UserType)
            .HasConversion<string>();

        modelBuilder.Entity<TransferenciaEntity>()
            .HasKey(t => t.IdTransferencia);

        modelBuilder.Entity<TransferenciaEntity>()
            .HasOne(t => t.Sender)
            .WithMany()
            .HasForeignKey(t => t.SenderId)
            .OnDelete(DeleteBehavior.Restrict) // Caso seja deletado a acao
            .HasConstraintName("FK_Transferencias_Sender");

        modelBuilder.Entity<TransferenciaEntity>()
            .Property(t => t.Valor)
            .HasColumnType("decimal(18,2)");
        
        modelBuilder.Entity<TransferenciaEntity>()
            .HasOne(t => t.Receiver)
            .WithMany()
            .HasForeignKey(t => t.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict) // Impede a deletacao em massa
            .HasConstraintName("FK_Transferencias_Receiver");
    }
}