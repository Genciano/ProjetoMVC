using Microsoft.EntityFrameworkCore;
using ProjetoMVC.Data.Mapeamento;
using ProjetoMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMVC.Data
{
    public class JovemProgramadorContexto: DbContext
    {
      
        public JovemProgramadorContexto(DbContextOptions<JovemProgramadorContexto> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlunoMapping());
        }

        public DbSet<AlunoModel> Aluno { get; set; }

    }    
}
 
