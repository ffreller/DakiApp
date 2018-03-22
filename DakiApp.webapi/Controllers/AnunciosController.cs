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
        
        /// NESSE MÉTODO, DEVEM SER MOSTRADOS APENAS OS ANUNCIOS APROVADOS-----Autorizacao=true
        /// <summary>
        /// Lista todas os anúncios cadastrados e aprovados
        /// </summary>
        /// <returns> Lista de anúncios</returns>
        /// <response code="200"> Retorna uma lista de anúncios</response>
        /// <response code="400"> Ocorreu um erro</response>
        [Authorize("Bearer",Roles="Cliente")]
        [HttpGet]
        [ProducesResponseType(typeof(List<AnunciosDomain>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult ListarAprovados()
        {
            try
            {
                //checar
                var anuncios =  _context.Anuncios.Include(d => d.Autorizacao).Where(d => d.Autorizacao == true);
                return Ok(anuncios);
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }     
        }

        /// NESSE MÉTODO, DEVEM SER MOSTRADOS APENAS OS ANUNCIOS PENDENTES DE APROVAÇÃO------Autorizacao = null
        /// <summary>
        /// Lista todas os anúncios cadastrados pendentes de aprovação
        /// </summary>
        /// <returns> Lista de anúncios</returns>
        /// <response code="200"> Retorna uma lista de anúncios</response>
        /// <response code="400"> Ocorreu um erro</response>
        [Authorize("Bearer",Roles="Admin")]
        [HttpGet]
        [ProducesResponseType(typeof(List<AnunciosDomain>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult ListarPendentes()
        {
            try
            {
                // checar
                var anuncios =  _context.Anuncios.Include(d => d.Autorizacao).Where(d => d.Autorizacao == null);
                return Ok(anuncios);
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }     
        }

        /// <summary>
        /// Lista dados do anúncio requisitado
        /// </summary>
        /// <param name="id">Id do anúncio</param>
        /// <returns> Anúncio de id indicado, com todas suas perguntas e todas as alternativas destas</returns>
        /// <response code="200"> Retorna uma anúncio, com seus detalhes</response>
        /// <response code="400"> Ocorreu um erro</response>
        /// <response code="404">Id não encontrado</response>
        [Authorize("Bearer",Roles="Cliente,Admin")]
        [HttpGet ("{id}")]
        [ProducesResponseType(typeof(JsonResult), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                return Ok(_repo.BuscarPorId(id));
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cadastra novo anúncio
        /// </summary>
        /// <param name="AnunciosDomain">Anúncio</param>
        /// <returns> ok </returns>
        /// <response code="200"> Retorna ok </response>
        ///  <response code="400"> Ocorreu um erro</response>
        [Authorize("Bearer",Roles="Cliente,Admin")]
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
        /// Atualiza o anúncio indicado
        /// </summary>
        /// <param name="id">Id do anúncio</param>
        /// <returns> ok </returns>
        /// <response code="200"> Retorna uma lista de anúncios</response>
        /// <response code="400"> Ocorreu um erro</response>
        /// <response code="404"> Id não encontrado</response>
        [Authorize("Bearer",Roles="Cliente,Admin")]
        [HttpPut ("{id}")]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult Atualizar([FromBody]AnunciosDomain Anuncios)
        {
            try
            {
                var anuncios = _repo.BuscarPorId(Anuncios.id);
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
        /// Atualiza a Autorização do anúncio
        /// </summary>
        /// <param name="id">Id do anúncio</param>
        /// <returns> ok </returns>
        /// <response code="200"> Retorna número de linhas alteradas</response>
        /// <response code="400"> Ocorreu um erro</response>
        /// <response code="404"> Id não encontrado</response>
        [Authorize("Bearer",Roles="Admin")]
        [HttpPut ("{id}")]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult AtualizarAut([FromBody] AnunciosDomain Anuncios)
        {
            try
            {
                // precisamos arranjar uma maneira de enviar apenas o campo autorização no body
                var anuncios = _context.Anuncios.Include(d => d.Autorizacao).FirstOrDefault(d => d.id == Anuncios.id);
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
        /// Deleta anúncio indicado
        /// </summary>
        /// <param name="id">Id do anúncio</param>
        /// <returns> ok </returns>
        /// <response code="200"> Retorna Ok</response>
        /// <response code="400"> Ocorreu um erro</response>
        /// <response code="404"> Id não encontrado</response>
        [Authorize("Bearer",Roles="Cliente,Admin")]        
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