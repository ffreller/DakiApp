using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DakiApp.domain.Contracts;
using DakiApp.domain.Entities;

namespace DakiApp.webapi.Controllers
{
    [Route("api/[controller]")]
    public class AlternativasController: Controller
    {
        private readonly IBaseRepository<AlternativasDomain> _repo;

        public AlternativasController(IBaseRepository<AlternativasDomain> repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_repo.Listar());
        }

        [HttpGet ("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            return Ok(_repo.BuscarPorId(id));
        }

        [HttpDelete ("{id}")]
        public IActionResult Deletar(int id)
        {
            var alternativas = _repo.BuscarPorId(id);
            return Ok(_repo.Deletar(alternativas));
        }

        [HttpPost]
        public IActionResult Inserir([FromBody]AlternativasDomain Alternativas)
        {
            return Ok(_repo.Inserir(Alternativas));
        }

        [HttpPut ("{id}")]
        public IActionResult Autalizar(int id)
        {
            var alternativas = _repo.BuscarPorId(id);
            return Ok(_repo.Atualizar(alternativas));
        }


        
    }
}