using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using DakiApp.domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DakiApp.webapi.Controllers
{
    public class LoginController
    {
        public IActionResult Validar([FromBody]Usuario usuario, [FromServices]signingConfigurations signingConfigurations, [FromServices]TokenConfigurations tokenConfigurations)
        {
            Usuario user = _contexto.Usuarios.Include("UsuariosPermissoes").Include("UsuariosPermissoes.Permissao").FirstOrDefault(c => c.Email == usuario.Email && c.Senha == usuario.Senha);
            if(user != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.IdUsuario.ToString(), "Login"), 
                    new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.IdUsuario.ToString()),
                    new Claim("Nome", user.Nome),
                    new Claim(ClaimTypes.Email, user.Email)
                });

                var claims = new List<Claim>();
                foreach(var item in user.UsuariosPermissoes)
                    {
                       claims.Add(new Claim(ClaimTypes.Role, item.Permissao.Nome));
                    }

                identity.AddClaims(claims);
            
                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor{
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity
                    });

                var token = handler.WriteToken(securityToken);
                var retorno = new{atutenticacao = true, acessToken = token, message = "OK"};

                return Ok(retorno);
            }

            var retornoerro = new {autenticacao = false, message = "Falha na Autentição"};
            return BadRequest(retornoerro);

        }
    }
}