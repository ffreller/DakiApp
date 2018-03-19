using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DakiApp.domain.Contracts;
using DakiApp.domain.Entities;
using DakiApp.repository.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace DakiApp.webapi.Controllers
{
    [Route("api/[controller]")]
    public class AnunciosController: Controller
    {
        private readonly IBaseRepository<AnunciosDomain> _repo;

        private readonly DakiAppContext _context;

        public AnunciosController(IBaseRepository<AnunciosDomain> repo, DakiAppContext context)
        {
            _repo = repo;
            _context = context;
        }
        
        /// <summary>
        /// Lista todas os questionários cadastrados cadastrados
        /// </summary>
        /// <returns> Lista de questionários</returns>
        /// <response code="200"> Retorna uma lista de cursos</response>
        /// <response code="400"> Ocorreu um erro</response>
        [Authorize("Bearer",Roles="NOMEDAPERMISAAAOOOOOOOOOOOO")]
        [HttpGet]
        [ProducesResponseType(typeof(List<AnunciosDomain>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_repo.Listar());
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // /// <summary>
        // /// Lista dados do questionário requisitado
        // /// </summary>
        // /// <param name="id">Id do questionário</param>
        // /// <returns> Questionário de id indicado, com todas suas perguntas e todas as alternativas destas</returns>
        // /// <response code="200"> Retorna uma questionário, com seus detalhes</response>
        // /// <response code="400"> Ocorreu um erro</response>
        // /// <response code="404">Id não encontrado</response>
        // [HttpGet ("{id}")]
        // [ProducesResponseType(typeof(JsonResult), 200)]
        // [ProducesResponseType(typeof(string), 404)]
        // [ProducesResponseType(typeof(string), 400)]
        // public IActionResult BuscarPorId(int id)
        // {
        //     try
        //     { 
        //         var anuncio =  _context
        //         .Anuncios
        //         .Include(d => d.AnuncioPerguntas)
        //         .ThenInclude(d => d.Pergunta.Alternativas)
        //         .FirstOrDefault(d => d.id == id);
        //         if (anuncio == null)
        //         {
        //             return NotFound("Id não encontrado");
        //         }
            
        //         var respostaJson = new {
        //             anuncio.id,
        //             anuncio.Nome,
        //             perguntas = anuncio.AnuncioPerguntas.Select(d => new {
        //                 d.Pergunta.id,
        //                 d.Pergunta.TipoResposta,
        //                 d.Pergunta.Enunciado,
        //                 d.Pergunta.Obrigatoria,
        //                 Alternativas = d.Pergunta.Alternativas.Select(g => new {
        //                     g.id,
        //                     g.Conteudo,
        //                     g.DataCriacao,
        //                 }).ToArray(),
        //             }).ToArray()
        //         };
        //         return Ok(respostaJson);
        //     }
        //     catch(System.Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }

            // var includes = new string[]{ "AnuncioPerguntas", "AnuncioPerguntas.Pergunta.Alternativas"};
            // return Ok(_repo.BuscarPorId(id,includes));
        }

        

        /// <summary>
        /// Cadastra novo questionário
        /// </summary>
        /// <param name="AnuncioDomain">Questionário</param>
        /// <returns> ok </returns>
        /// <response code="200"> Retorna ok </response>
        ///  <response code="400"> Ocorreu um erro</response>
        [HttpPost]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Inserir([FromBody]AnunciosDomain Anuncios)
        {
            try
            {
                return Ok(_repo.Inserir(Anuncios));
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }               
        }

        /// <summary>
        /// Atualiza o questionário indicado
        /// </summary>
        /// <param name="id">Id do questionário</param>
        /// <returns> ok </returns>
        /// <response code="200"> Retorna uma lista de questionários</response>
        /// <response code="400"> Ocorreu um erro</response>
        /// <response code="404"> Id não encontrado</response>
        [HttpPut ("{id}")]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult Atualizar(int id)
        {
            try
            {
                var anuncios = _repo.BuscarPorId(id);
                if (anuncios == null)
                {
                    return NotFound("Id não encontrado");
                }
                return Ok(_repo.Atualizar(anuncios));
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deleta questionário indicado
        /// </summary>
        /// <param name="id">Id do questionário</param>
        /// <returns> ok </returns>
        /// <response code="200"> Retorna Ok</response>
        /// <response code="400"> Ocorreu um erro</response>
        /// <response code="404"> Id não encontrado</response>
        [HttpDelete ("{id}")]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult Deletar(int id)
        {
            try{
                var anuncios = _repo.BuscarPorId(id);
                if (anuncios == null){
                    return NotFound("Id não encontrado");
                }
                return Ok(_repo.Deletar(anuncios));
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }   
        }
    }
}