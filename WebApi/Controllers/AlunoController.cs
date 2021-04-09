using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.Models;


namespace WebApi.Controllers
{
    /* Nesta parte eu estou fazendo a decoração da nossa API, ou seja, é através desses comandos que serão permitidos
    os acessos externos a nossa API.
    Para utilizar o EnableCors é necessário referenciar corretamente using System.Web.Http.Cors
     */
    [EnableCors("*","*","*")]
    public class AlunoController : ApiController
    {
        // GET: api/Aluno

        // GET: api/Aluno
        [HttpGet]
        [Route("Recuperar")]
          public IHttpActionResult Get()
          {
            try
            {
                Alunos aluno = new Alunos();
                return Ok(aluno.ListarAluno());
            }              
            catch (Exception ex)
            {
                return InternalServerError(ex);
             }
            
          }        
          // GET: api/Aluno/5
          [Route("Recuperar/{id:int}/{nome}/{sobrenome=Barcelos}")]
          public Alunos Get(int id)
          {
              Alunos aluno = new Alunos();
              return aluno.ListarAluno().Where(x => x.id ==id).FirstOrDefault();
          }
        [HttpGet]
        [Route(@"RecuperaPorDataNome/{data:regex([0-9]{4}\-[0-9]{2})}/{nome:minlength(5)}")]
        public IHttpActionResult Recuperar(string data, string nome)
        {
            try
            {
                Alunos aluno = new Alunos();
                IEnumerable<Alunos> alunos = aluno.ListarAluno().Where(x => x.data == data || x.nome == nome);
                if (!alunos.Any())
                    return NotFound();

                return Ok(alunos);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }




                 

        // POST: api/Aluno
        public List<Alunos> Post(Alunos aluno)
        {
            Alunos _aluno = new Alunos();
            _aluno.Inserir(aluno);
            return _aluno.ListarAluno();

        }

        // PUT: api/Aluno/5
        public List<Alunos> Put(int id, Alunos aluno)
        {
            Alunos _aluno = new Alunos();
            _aluno.id = id;
            _aluno.Atualizar(aluno);
            return _aluno.ListarAluno();

        }

        // DELETE: api/Aluno/5
        public void Delete(int id)
        {
            Alunos _aluno = new Alunos();
            _aluno.Deletar(id);
           

        }
    }
}
