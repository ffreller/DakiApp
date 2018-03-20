using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
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
    [Route("api/[controller]")]
    public class LoginController:Controller
    {   
       DakiAppContext contexto;

       public LoginController(DakiAppContext contexto_)
       {
           contexto = contexto_;

       }

        [AllowAnonymous]
        [HttpPost]
        public object Login([FromBody]UsuariosDomain usuario, [FromServices]signingConfigurations signingConfigurations, [FromServices]TokenConfigurations tokenConfigurations)
        {
            UsuariosDomain user = contexto.Usuarios.Include("UsuarioPermissoes").Include("UsuarioPermissoes.Permissao").FirstOrDefault(c => c.Email == usuario.Email && c.Senha == usuario.Senha);

            if(user != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(user.id.ToString(), "Login"), new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.id.ToString()),
                    new Claim("Nome", user.Nome),
                    new Claim(ClaimTypes.Email, user.Email)
                });

                var claims = new List<Claim>();
                foreach(var item in user.UsuarioPermissoes)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item.Permissao.Nome));
                }
                
                identity.AddClaims(claims);
            
                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor{
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                });

                var token = handler.WriteToken(securityToken);
                
                var retorno = new{atutenticacao = true,  acessToken = token, message = "OK"};

                return Ok(retorno);
            }

            var retornoerro = new {autenticacao = false, message = "Falha na Autentição"};
            return BadRequest(retornoerro);
        }
    }
}