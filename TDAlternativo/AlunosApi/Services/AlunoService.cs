using AlunosApi.Context;
using AlunosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AlunosApi.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly AppDbContext _appDbContext;

        public AlunoService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Aluno>> GetAlunos()
        {
            try
            {
                return await _appDbContext.Alunos.ToListAsync();
            }
            catch 
            {
                throw;
            }          
        }

        public async Task<IEnumerable<Aluno>> GetAlunosByNome(string nome)
        {
            IEnumerable<Aluno> alunos;
            if (!string.IsNullOrWhiteSpace(nome))
            {
                alunos =  await _appDbContext.Alunos.Where(n => n.Nome.Contains(nome)).ToListAsync();
            }
            else
            {
                alunos =  await GetAlunos();
            }
            return alunos;
        }
        public async Task<Aluno> GetAluno(int id)
        {
            var aluno = await _appDbContext.Alunos.FindAsync(id);

            return aluno;
        }

        public async Task CreateAluno(Aluno aluno)
        {
            _appDbContext.Alunos.Add(aluno);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAluno(Aluno aluno)
        {
            _appDbContext.Entry(aluno).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAluno(Aluno aluno)
        {
            _appDbContext.Alunos.Remove(aluno);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
