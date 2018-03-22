using System.Linq;
using DakiApp.domain.Contracts;
using DakiApp.domain.Entities;
using DakiApp.repository.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DakiApp.webapi.Controllers
{
    [Route("api/[controller]")]
    public class CadastroController:Controller
    {
        private readonly IBaseRepository<UsuariosDomain> _repo;
        private readonly IBaseRepository<UsuarioPermissoesDomain> _repo1;
        private readonly DakiAppContext _context;

        public CadastroController(IBaseRepository<UsuariosDomain> repo, IBaseRepository<UsuarioPermissoesDomain> repo1, DakiAppContext context)
        {
            _repo1=repo1;
            _repo = repo;
            _context = context;
        }
        
         /// <summary>
        /// Cadastra novo usuário
        /// </summary>
        /// <param name="UsuariosDomain">Usuário</param>
        /// <returns> ok </returns>
        /// <response code="200"> Retorna ok </response>
        ///  <response code="400"> Ocorreu um erro</response>
        [HttpPost]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Inserir([FromBody]UsuariosDomain Usuarios, [FromBody]UsuarioPermissoesDomain UsuarioPermissoes)
        {
            try
            {
                int id;

                _context.Usuarios.Add(Usuarios);
                _context.SaveChanges();
                id = Usuarios.id;
                return Ok();
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }               
        }
    }
}