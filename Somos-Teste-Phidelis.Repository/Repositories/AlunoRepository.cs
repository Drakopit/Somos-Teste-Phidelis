using Microsoft.EntityFrameworkCore;
using Somos_Teste_Phidelis.Domain;
using Somos_Teste_Phidelis.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Somos_Teste_Phidelis.Repository.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly PhidelisContext _context;

        public AlunoRepository(PhidelisContext context)
        {
            _context = context;
        }

        public async Task<Aluno> Get(int id)
        {
            try
            {
                return await _context.Aluno.Where(a => a.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IList<Aluno>> GetByName(Expression<Func<Aluno, bool>> where)
        {
            try
            {
                return await _context.Aluno.Where(where).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IList<Aluno>> GetAll()
        {
            try
            {
                return await _context.Aluno.OrderByDescending(a => a.Date).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(Aluno aluno)
        {
            try
            {
                await _context.Aluno.AddAsync(aluno);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Aluno aluno)
        {
            try
            {
                var result = await _context.Aluno.FirstOrDefaultAsync(a => a.Id == aluno.Id);
                if (result != null)
                {
                    _context.Entry(result).State = EntityState.Detached;
                    _context.Entry(aluno).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var aluno = await _context.Aluno.FirstOrDefaultAsync(a => a.Id == id);
                _context.Remove(aluno);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
