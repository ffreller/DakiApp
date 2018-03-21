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
    public class RespostasController: Controller
    {
        
        private readonly IBaseRepository<RespostasDomain> _repo;

        private readonly DakiAppContext _context;

        public RespostasController(IBaseRepository<RespostasDomain> repo, DakiAppContext context)
        {
            _repo = repo;
            _context = context;
        }

         /// <summary>
        /// Cadastra nova resposta
        /// </summary>
        /// <param name="RespostasDomain">Resposta</param>
        /// <returns> ok </returns>
        /// <response code="200"> Retorna ok </response>
        ///  <response code="400"> Ocorreu um erro</response>
        [Authorize("Bearer",Roles="Cliente")]
        [HttpPost]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Inserir([FromBody]RespostasDomain Respostas)
        {
            try
            {
                return Ok(_repo.Inserir(Respostas));
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }               
        }
    }
}