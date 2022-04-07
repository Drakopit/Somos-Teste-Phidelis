using Microsoft.EntityFrameworkCore;
using Somos_Teste_Phidelis.Domain;
using System;

namespace Somos_Teste_Phidelis.Repository
{
    public class PhidelisContext : DbContext
    {
        public PhidelisContext(DbContextOptions<PhidelisContext> options) : base(options) {}

        public DbSet<Aluno> Aluno { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
