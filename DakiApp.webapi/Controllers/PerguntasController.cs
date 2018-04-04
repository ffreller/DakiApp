using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DakiApp.domain.Contracts;
using DakiApp.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace DakiApp.webapi.Controllers
{
    [Route("api/[controller]")]
    public class PerguntasController: Controller
    {
        private readonly IBaseRepository<PerguntasDomain> _repo;

        public PerguntasController(IBaseRepository<PerguntasDomain> repo)
        {
            _repo = repo;
        }

        // [HttpGet]
        // public IActionResult Listar()
        // {
        //     return Ok(_repo.Listar());
        // }

        
        /// <summary>
        /// Retorna pergunta do id identificado
        /// </summary>
        /// <param name="id">Id da pergunta</param>
        /// <returns> ok </returns>
        /// <response code="200"> Retorna ok </response>
        /// <response code="400"> Ocorreu um erro</response>
        /// <response code="404">Id não encontrado</response>
        [Authorize("Bearer",Roles="Cliente,Admin")]
        [HttpGet ("{id}")]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 400)]

        public IActionResult BuscarPorId(int id)
        {
            return Ok(_repo.BuscarPorId(id, new string[] {"QuestionarioPerguntas.Questionario"}));
        }

        // [HttpDelete ("{id}")]
        // public IActionResult Deletar(int id)
        // {
        //     var perguntas = _repo.BuscarPorId(id);
        //     return Ok(_repo.Deletar(perguntas));
        // }

        // [HttpPost]
        // public IActionResult Inserir([FromBody]PerguntasDomain Perguntas)
        // {
        //     return Ok(_repo.Inserir(Perguntas));
        // }

        // [HttpPut ("{id}")]
        // public IActionResult Autalizar(int id)
        // {
        //     var perguntas = _repo.BuscarPorId(id);
        //     return Ok(_repo.Atualizar(perguntas));
        // }
        
    }
}