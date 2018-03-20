using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using DakiApp.domain.Contracts;
using DakiApp.domain.Entities;
using DakiApp.repository.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
namespace DakiApp.webapi.Controllers
{
    [Route("api/[controller")]
    public class LoginController:Controller
    {   
       DakiAppContext contexto;

        [AllowAnonymous]
        [HttpPost]
        public object Login([FromBody]UsuariosDomain usuario, [FromServices]signingConfigurations signingConfigurations, [FromServices]TokenConfigurations tokenConfigurations)
        {
            // UsuariosDomain user = contexto.Usuarios.Include("UsuarioPermissoes").Include("UsuarioPermissoes.Permissao").FirstOrDefault(c => c.Email == usuario.Email && c.Senha == usuario.Senha);

            if(usuario != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(usuario.id.ToString(), "Login"), new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, usuario.id.ToString()),
                    new Claim("Nome", usuario.Nome),
                    new Claim(ClaimTypes.Email, usuario.Email)
                });
                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                // var claims = new List<Claim>();
                // foreach(var item in usuario.UsuarioPermissoes.Permissao)
                // {
                //     claims.Add(new Claim(ClaimTypes.Role, item.Permissao.Nome));
                // }
                // identity.AddClaims(claims);
            
                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor{
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                    });

                var token = handler.WriteToken(securityToken);
                
                var retorno = new{atutenticacao = true, created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"), expires = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"), acessToken = token, message = "OK"};

                return Ok(retorno);
            }

            var retornoerro = new {autenticacao = false, message = "Falha na Autentição"};
            return BadRequest(retornoerro);
        }
    }
}