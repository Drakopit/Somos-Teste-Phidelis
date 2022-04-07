using Microsoft.AspNetCore.Mvc;
using Somos_Teste_Phidelis.Domain;
using Somos_Teste_Phidelis.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Somos_Teste_Phidelis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoRepository _repository;

        public AlunoController(IAlunoRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<AlunoController>
        [HttpGet("todos")]
        public async Task<ActionResult<List<Aluno>>> GetAll()
        {
            try
            {
                var alunos = await _repository.GetAll();
                if (alunos.Count > 0)
                    return Ok(alunos);
                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<List<Aluno>>> GetByName(string name)
        {
            try
            {
                var alunos = await _repository.GetByName(a => a.Name.Contains(name));
                if (alunos.Count > 0)
                    return Ok(alunos);
                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<AlunoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> Get(int id)
        {
            try
            {
                var aluno = await _repository.Get(id);
                if (aluno != null)
                    return Ok(aluno);
                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST api/<AlunoController>
        [HttpPost]
        public async Task Post([FromBody] Aluno aluno)
        {
            try
            {
                await _repository.Insert(aluno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT api/<AlunoController>/5
        [HttpPut]
        public async Task Put([FromBody] Aluno aluno)
        {
            try
            {
                await _repository.Update(aluno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // DELETE api/<AlunoController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            try
            {
                await _repository.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
