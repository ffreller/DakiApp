using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DakiApp.domain.Contracts;
using DakiApp.domain.Entities;
using DakiApp.repository.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using DakiApp.repository.Repositories;
using System;
using System.IO;
using System.Text;

namespace DakiApp.webapi.Controllers
{
    [Route("api/[controller]")]
    public class QuestionariosController : Controller
    {
        private readonly IBaseRepository<QuestionariosDomain> _repo;

        private readonly IBaseRepository<RespostasDomain> _repo1;

        private readonly DakiAppContext _context;

        public QuestionariosController(IBaseRepository<QuestionariosDomain> repo, DakiAppContext context, IBaseRepository<RespostasDomain> repo1)
        {
            _repo = repo;
            _repo1 = repo1;
            _context = context;
        }

        /// <summary>
        /// Passa respostas do questionário para excel
        /// </summary>
        /// <returns> Arquivo excel</returns>
        /// <param name="dados">Id do questionário e data</param>
        /// <response code="200"> Retorna arquivo excel requisitado</response>
        /// <response code="400"> Ocorreu um erro</response>
        /// <response code="404"> Id ou data incorreta </response>
        //[Authorize("Bearer",Roles="Admin")]
        [HttpPost]
        [Route("excel")]
        [ProducesResponseType(typeof(List<QuestionariosDomain>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult GetExcel([FromBody] ExcelDomain dados)
        {
            var nomearquivo = $"RespostasExcel-{DateTime.Now.ToString().Replace(':', '-').Replace('/', '-').Replace(' ', '-')}.csv";
            var caminhoarquivo = $"{Directory.GetCurrentDirectory()}\\{nomearquivo}";
            StreamWriter excel = new StreamWriter(caminhoarquivo, false, Encoding.UTF8);
            try
            {
                string link = "http://ffreller-001-site1.dtempurl.com/" + nomearquivo;
                var include = _repo1.Listar(new string[] { "Pergunta", "Usuario", "Questionario" });
                var filtro = include.Where(a => a.QuestionarioId == dados.questionarioId && a.DataCriacao >= dados.data);
                var respostas = filtro.Select(a => new
                {
                    a.Texto,
                    a.Pergunta.Enunciado,
                    a.Pergunta.id,
                    a.Usuario.Nome,
                    a.UsuarioId,
                    a.QuestionarioId
                });
                if (respostas == null)
                {
                    return NotFound("Id ou data incorreta");
                }

                excel.WriteLine("Id da pergunta;Enunciado da pergunta;Id do questionário;Nome do usuário;Id do usuário;Texto de resposta");

                foreach (var x in respostas)
                {
                    excel.WriteLine(x.id + ";" + x.Enunciado + ";" + x.QuestionarioId + ";" + x.Nome + ";" + x.UsuarioId + ";" + x.Texto);
                }

                excel.Close();

                return Ok("Excel criado com sucesso, segue link:" + link);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lista todas os questionários cadastrados
        /// </summary>
        /// <returns> Lista de questionários</returns>
        /// <response code="200"> Retorna uma lista de questionários</response>
        /// <response code="400"> Ocorreu um erro</response>
        [Authorize("Bearer", Roles = "Cliente,Admin")]
        [HttpGet]
        [ProducesResponseType(typeof(List<QuestionariosDomain>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_repo.Listar());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lista dados do questionário requisitado
        /// </summary>
        /// <param name="id">Id do questionário</param>
        /// <returns> Questionário de id indicado, com todas suas perguntas e todas as alternativas destas</returns>
        /// <response code="200"> Retorna uma questionário, com seus detalhes</response>
        /// <response code="400"> Ocorreu um erro</response>
        /// <response code="404">Id não encontrado</response>
        [Authorize("Bearer", Roles = "Cliente,Admin")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(JsonResult), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                var questionario = _context
                .Questionarios
                .Include(d => d.QuestionarioPerguntas)
                .ThenInclude(d => d.Pergunta.Alternativas)
                .FirstOrDefault(d => d.id == id);
                if (questionario == null)
                {
                    return NotFound("Id não encontrado");
                }

                var respostaJson = new
                {
                    questionario.id,
                    questionario.Nome,
                    perguntas = questionario.QuestionarioPerguntas.Select(d => new
                    {
                        d.Pergunta.id,
                        d.Pergunta.TipoResposta,
                        d.Pergunta.Enunciado,
                        d.Pergunta.Obrigatoria,
                        Alternativas = d.Pergunta.Alternativas.Select(g => new
                        {
                            g.id,
                            g.Conteudo,
                            g.DataCriacao,
                        }).ToArray(),
                    }).ToArray()
                };
                return Ok(respostaJson);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

            // var includes = new string[]{ "QuestionarioPerguntas", "QuestionarioPerguntas.Pergunta.Alternativas"};
            // return Ok(_repo.BuscarPorId(id,includes));
        }



        // /// <summary>
        // /// Cadastra novo questionário
        // /// </summary>
        // /// <param name="QuestionariosDomain">Questionário</param>
        // /// <returns> ok </returns>
        // /// <response code="200"> Retorna ok </response>
        // ///  <response code="400"> Ocorreu um erro</response>
        // [HttpPost]
        // [ProducesResponseType(typeof(int), 200)]
        // [ProducesResponseType(typeof(string), 400)]
        // public IActionResult Inserir([FromBody]QuestionariosDomain Questionarios)
        // {
        //     try
        //     {
        //         return Ok(_repo.Inserir(Questionarios));
        //     }
        //     catch(System.Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }               
        // }

        // /// <summary>
        // /// Atualiza o questionário indicado
        // /// </summary>
        // /// <param name="id">Id do questionário</param>
        // /// <returns> ok </returns>
        // /// <response code="200"> Retorna uma lista de questionários</response>
        // /// <response code="400"> Ocorreu um erro</response>
        // /// <response code="404"> Id não encontrado</response>
        // [HttpPut ("{id}")]
        // [ProducesResponseType(typeof(int), 200)]
        // [ProducesResponseType(typeof(string), 400)]
        // [ProducesResponseType(typeof(string), 404)]
        // public IActionResult Atualizar(int id)
        // {
        //     try
        //     {
        //         var questionarios = _repo.BuscarPorId(id);
        //         if (questionarios == null)
        //         {
        //             return NotFound("Id não encontrado");
        //         }
        //         return Ok(_repo.Atualizar(questionarios));
        //     }
        //     catch(System.Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        // }

        // /// <summary>
        // /// Deleta questionário indicado
        // /// </summary>
        // /// <param name="id">Id do questionário</param>
        // /// <returns> ok </returns>
        // /// <response code="200"> Retorna Ok</response>
        // /// <response code="400"> Ocorreu um erro</response>
        // /// <response code="404"> Id não encontrado</response>
        // [HttpDelete ("{id}")]
        // [ProducesResponseType(typeof(int), 200)]
        // [ProducesResponseType(typeof(string), 400)]
        // [ProducesResponseType(typeof(string), 404)]
        // public IActionResult Deletar(int id)
        // {
        //     try{
        //         var questionarios = _repo.BuscarPorId(id);
        //         if (questionarios == null){
        //             return NotFound("Id não encontrado");
        //         }
        //         return Ok(_repo.Deletar(questionarios));
        //     }
        //     catch(System.Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }   
        // }
    }
}