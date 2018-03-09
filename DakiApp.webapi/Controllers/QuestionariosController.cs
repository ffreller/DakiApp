using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DakiApp.domain.Contracts;
using DakiApp.domain.Entities;
using DakiApp.repository.Context;
using Microsoft.EntityFrameworkCore;

namespace DakiApp.webapi.Controllers
{
    [Route("api/[controller]")]
    public class QuestionariosController: Controller
    {
        private readonly IBaseRepository<QuestionariosDomain> _repo;

        private readonly DakiAppContext _context;

        public QuestionariosController(IBaseRepository<QuestionariosDomain> repo, DakiAppContext context)
        {
            _repo = repo;
            _context = context;
        }
        
        /// <summary>
        /// Lista todas os questionários cadastrados cadastrados
        /// </summary>
        /// <returns> Lista de questionários</returns>
        /// <response code "200"> Retorna uma lista de curso</response>
        /// /// <response code "400"> Ocorreu um erro</response>
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_repo.Listar());
        }

        /// <summary>
        /// Lista dados do questionário requisitado
        /// </summary>
        /// <returns> Questionário de id indicado, com todas suas perguntas e todas as alternativas destas</returns>
        /// <response code "200"> Retorna uma questionário, com seus detalhes</response>
        /// /// <response code "400"> Ocorreu um erro</response>
        [HttpGet ("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var questionario =  _context
                .Questionarios
                .Include(d => d.QuestionarioPerguntas)
                .ThenInclude(d => d.Pergunta.Alternativas)
                .FirstOrDefault(d => d.id == id);
            
            var respostaJson = new {
                questionario.id,
                questionario.Nome,
                perguntas = questionario.QuestionarioPerguntas.Select(d => new {
                    d.Pergunta.id,
                    d.Pergunta.TipoResposta,
                    d.Pergunta.Enunciado,
                    Alternativas = d.Pergunta.Alternativas.Select(g => new {
                        g.id,
                        g.Conteudo,
                        g.DataCriacao,
                    }).ToArray(),
                }).ToArray()
            };
            return Ok(respostaJson);

            // var includes = new string[]{ "QuestionarioPerguntas", "QuestionarioPerguntas.Pergunta.Alternativas"};
            // return Ok(_repo.BuscarPorId(id,includes));
        }

        /// <summary>
        /// Deleta questionário indicado
        /// </summary>
        /// <returns> ok </returns>
        /// <response code "200"> Retorna uma lista de questionários</response>
        /// /// <response code "400"> Ocorreu um erro</response>
        [HttpDelete ("{id}")]
        public IActionResult Deletar(int id)
        {
            var questionarios = _repo.BuscarPorId(id);
            return Ok(_repo.Deletar(questionarios));
        }

        /// <summary>
        /// Cadastra novo questionário
        /// </summary>
        /// <returns> ok </returns>
        /// <response code "200"> Retorna uma lista de quesionário</response>
        /// /// <response code "400"> Ocorreu um erro</response>
        [HttpPost]
        public IActionResult Inserir([FromBody]QuestionariosDomain Questionarios)
        {
            return Ok(_repo.Inserir(Questionarios));
        }

        /// <summary>
        /// Atualiza o questionário indicado
        /// </summary>
        /// <returns> ok </returns>
        /// <response code "200"> Retorna uma lista de questionários</response>
        /// /// <response code "400"> Ocorreu um erro</response>
        [HttpPut ("{id}")]
        public IActionResult Autalizar(int id)
        {
            var questionarios = _repo.BuscarPorId(id);
            return Ok(_repo.Atualizar(questionarios));
        }


        
    }
}