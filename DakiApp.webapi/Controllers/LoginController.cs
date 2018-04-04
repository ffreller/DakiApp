using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using DakiApp.domain.Contracts;
using DakiApp.domain.Entities;
using DakiApp.repository.Context;
using DakiApp.repository.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DakiApp.webapi.Controllers
{
    [Route("api/")]
    public class LoginController : Controller
    {
        DakiAppContext contexto;

        public LoginController(DakiAppContext contexto_)
        {
            contexto = contexto_;

        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="usuario">Usuário</param>
        /// <param name="signingConfigurations">SigningConfigurations</param>
        /// <param name="tokenConfigurations">Token</param>
        /// <returns> ok </returns>
        /// <response code="200"> Retorna ok </response>
        /// <response code="400"> Ocorreu um erro</response>
        [Route("login")]
        [AllowAnonymous]
        [HttpPost]
        public object Login([FromBody]UsuariosDomain usuario, [FromServices]signingConfigurations signingConfigurations, [FromServices]TokenConfigurations tokenConfigurations)
        {
            try
            {
                HashPassword geradorHash = new HashPassword();
                var hash = geradorHash.GenerateHash(usuario.Senha);
                if (hash != null)
                {
                    usuario.Senha = hash;
                }

                UsuariosDomain user = contexto.Usuarios.Include("UsuarioPermissoes").Include("UsuarioPermissoes.Permissao").FirstOrDefault(c => c.Email == usuario.Email && c.Senha == usuario.Senha);

                if (user != null)
                {
                    ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(user.id.ToString(), "Login"), new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.id.ToString()),
                        new Claim("Nome", user.Nome),
                        new Claim("Id", user.id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email)
                    });

                    var claims = new List<Claim>();
                    foreach (var item in user.UsuarioPermissoes)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item.Permissao.Nome));
                    }

                    identity.AddClaims(claims);

                    var handler = new JwtSecurityTokenHandler();
                    var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                    {
                        Issuer = tokenConfigurations.Issuer,
                        Audience = tokenConfigurations.Audience,
                        SigningCredentials = signingConfigurations.SigningCredentials,
                        Subject = identity,
                    });

                    var token = handler.WriteToken(securityToken);

                    var respostaJson = new
                    {
                        user.id,
                        user.Nome,
                        permissoes = user.UsuarioPermissoes.Select(d => new
                        {
                            d.Permissao.Nome
                        }).ToArray()
                    };

                    var retorno = new { atutenticacao = true, acessToken = token, message = "OK", usuario = respostaJson };

                    return Ok(retorno);
                }

                var retornoerro = new { autenticacao = false, message = "Falha na Autenticação" };
                return BadRequest(retornoerro);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns> ok </returns>
        /// <response code="200"> Retorna ok </response>
        /// <response code="400"> Ocorreu um erro</response>
        [Route("logout")]
        [AllowAnonymous]
        [HttpPost]
        public object Logout()
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = null,
                    Audience = null,
                    SigningCredentials = null,
                    Subject = null,
                });

                var token = handler.WriteToken(securityToken);
                return Ok(token);
            }
            catch (SystemException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}