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

        [HttpGet ("{id}")]
        [Authorize("Bearer",Roles="Cliente,Admin")]
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