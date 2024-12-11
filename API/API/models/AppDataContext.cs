using System;
using Microsoft.EntityFrameworkCore;


namespace API.models;

public class AppDataContext : DbContext
{
    public DbSet<Imcs>? TabelaImcs { get; set; }
    public DbSet<Aluno>? TabelaAluno { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Api.db");
    }
}
