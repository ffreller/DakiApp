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
        private readonly DakiAppContext _context;

        public CadastroController(IBaseRepository<UsuariosDomain> repo, DakiAppContext context)
        {
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
        public IActionResult Inserir([FromBody]UsuariosDomain Usuarios)
        {
            try
            {
                _context.Usuarios.Add(Usuarios);
                _context.SaveChanges();

                UsuarioPermissoesDomain permissoes = new UsuarioPermissoesDomain();
                permissoes.UsuarioId = Usuarios.id;
                permissoes.PermissaoId = 2;
                _context.UsuarioPermissoes.Add(permissoes);
                _context.SaveChanges();
                return Ok("Cadastrado com sucesso");
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }               
        }
    }
}