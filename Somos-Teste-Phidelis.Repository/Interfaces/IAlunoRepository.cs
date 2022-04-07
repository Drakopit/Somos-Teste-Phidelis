using Somos_Teste_Phidelis.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Somos_Teste_Phidelis.Repository.Interfaces
{
    public interface IAlunoRepository
    {
        Task<IList<Aluno>> GetAll();
        Task<IList<Aluno>> GetByName(Expression<Func<Aluno, bool>> where);
        Task<Aluno> Get(int id);
        Task Insert(Aluno aluno);
        Task Update(Aluno aluno);
        Task Delete(int id);
    }
}
