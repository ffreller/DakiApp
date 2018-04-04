using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DakiApp.domain.Contracts;
using DakiApp.domain.Entities;
using DakiApp.repository.Context;
using DakiApp.repository.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DakiApp.webapi.Controllers
{
    [Route("api/[controller]")]
    public class CadastroController : Controller
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
        /// <param name="Usuarios">Usuário</param>
        /// <returns> ok </returns>
        /// <response code="200"> Retorna ok </response>
        ///  <response code="400"> Ocorreu um erro</response>
        [HttpPost]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Inserir([FromBody]UsuariosDomain Usuarios)
        {
            var users = _context.Usuarios;
            foreach (var usua in users)
            {
                if (Usuarios.Email == usua.Email)
                {
                    return BadRequest("E-mail já cadastrado");
                }
            }

            try
            {
                HashPassword geradorHash = new HashPassword();
                var hash = geradorHash.GenerateHash(Usuarios.Senha);
                if (hash != null)
                {
                    Usuarios.Senha = hash;
                }

                _context.Usuarios.Add(Usuarios);
                _context.SaveChanges();

                UsuarioPermissoesDomain permissoes = new UsuarioPermissoesDomain();
                permissoes.UsuarioId = Usuarios.id;
                permissoes.PermissaoId = 2;
                permissoes.DataCriacao = DateTime.Now;
                _context.UsuarioPermissoes.Add(permissoes);
                _context.SaveChanges();
                return Ok("Cadastrado com sucesso");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cadastra novo administrador
        /// </summary>
        /// <param name="Usuarios">Usuário</param>
        /// <returns> ok </returns>
        /// <response code="200"> Retorna ok </response>
        ///  <response code="400"> Ocorreu um erro</response>
        [Authorize("Bearer", Roles = "Admin")]
        [HttpPost("admin")]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult InserirAdmin([FromBody]UsuariosDomain Usuarios)
        {
            var users = _context.Usuarios;
            var lista = new List<string>();
            foreach (var usua in users)
            {
                lista.Add(usua.Email);
            }
            foreach (var email in lista)
            {
                if (Usuarios.Email == email)
                {
                    return BadRequest("E-mail já cadastrado");
                }
            }

            try
            {
                HashPassword geradorHash = new HashPassword();
                var hash = geradorHash.GenerateHash(Usuarios.Senha);
                if (hash != null)
                {
                    Usuarios.Senha = hash;
                }

                _context.Usuarios.Add(Usuarios);
                _context.SaveChanges();

                UsuarioPermissoesDomain permissoes = new UsuarioPermissoesDomain();
                permissoes.UsuarioId = Usuarios.id;
                permissoes.PermissaoId = 1;
                permissoes.DataCriacao = DateTime.Now;
                _context.UsuarioPermissoes.Add(permissoes);
                _context.SaveChanges();
                return Ok("Cadastrado com sucesso");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}