using OrderManagerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<TipoProduto> TipoProdutos { get; set; }
        public DbSet<StatusPedido> StatusPedidos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração do tipo de dado para as colunas 'Nome', 'Email' e 'Numero' como VARCHAR
            modelBuilder.Entity<Clientes>()
                .Property(c => c.Nome)
                .HasColumnType("VARCHAR(100)");

            modelBuilder.Entity<Clientes>()
                .Property(c => c.Email)
                .HasColumnType("VARCHAR(50)");

            modelBuilder.Entity<Clientes>()
                .Property(c => c.Numero)
                .HasColumnType("VARCHAR(50)");
                
            // Configuração para a tabela StatusPedido (com os valores possíveis)
            modelBuilder.Entity<StatusPedido>()
                .HasData(
                    new StatusPedido { Id = 1, Status = "Em andamento" },
                    new StatusPedido { Id = 2, Status = "Concluido" },
                    new StatusPedido { Id = 3, Status = "Cancelado" }
                );
            modelBuilder.Entity<StatusPedido>()
                .Property(p => p.Status)
                .HasColumnType("VARCHAR(20)");

            // Relacionamento entre Pedidos e StatusPedido (via chave estrangeira)
            modelBuilder.Entity<Pedidos>()
                .HasOne(p => p.StatusPedido)
                .WithMany() 
                .HasForeignKey(p => p.StatusPedidoId) 
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Configuração do valor padrão para StatusPedidoId como "Em andamento" (Id = 1)
            modelBuilder.Entity<Pedidos>()
                .Property(p => p.StatusPedidoId)
                .HasDefaultValue(1); 

            // Relacionamento entre Pedidos e Clientes
            modelBuilder.Entity<Pedidos>()
                .HasOne(p => p.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento entre Pedidos e Produtos
            modelBuilder.Entity<Pedidos>()
                .HasOne(p => p.Produto)
                .WithMany()
                .HasForeignKey(p => p.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuração para o tipo de dado decimal no Valor de Produtos
            modelBuilder.Entity<Produtos>()
                .Property(p => p.Valor)
                .HasColumnType("decimal(18,2)");

            // Índice único para Nome de Produtos
            modelBuilder.Entity<Produtos>()
                .HasIndex(p => p.Nome)
                .IsUnique()
                .HasDatabaseName("IX_Produtos_NomeUnico");

            // Relacionamento entre Produtos e TipoProduto
            modelBuilder.Entity<Produtos>()
                .HasOne(p => p.TipoProduto)
                .WithMany(tp => tp.Produtos)
                .HasForeignKey(p => p.TipoProdutoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
