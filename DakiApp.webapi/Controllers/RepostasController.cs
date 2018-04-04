using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DakiApp.domain.Contracts;
using DakiApp.domain.Entities;
using DakiApp.repository.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System;

namespace DakiApp.webapi.Controllers
{
    [Route("api/[controller]")]
    public class RespostasController : Controller
    {

        private readonly IBaseRepository<RespostasDomain> _repo;

        private readonly DakiAppContext _context;

        public RespostasController(IBaseRepository<RespostasDomain> repo, DakiAppContext context)
        {
            _repo = repo;
            _context = context;
        }

        /// <summary>
        /// Lista todas as respostas cadastradas
        /// </summary>
        /// <returns> Lista de respotass</returns>
        /// <response code="200"> Retorna uma lista de Respostas</response>
        /// <response code="400"> Ocorreu um erro</response>
        [Authorize("Bearer", Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(typeof(List<RespostasDomain>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_repo.Listar());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Cadastra nova resposta
        /// </summary>
        /// <param name="Respostas">Resposta</param>
        /// <returns> ok </returns>
        /// <response code="200"> Retorna ok </response>
        ///  <response code="400"> Ocorreu um erro</response>
        [Authorize("Bearer", Roles = "Cliente")]
        [HttpPost]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Inserir([FromBody]List<RespostasDomain> Respostas)
        {
            List<RespostasDomain> listarespostas = new List<RespostasDomain>();
            var respostascadastradas = _context.Respostas;

            try
            {
                foreach (RespostasDomain resposta in Respostas)
                {
                    listarespostas.Add(new RespostasDomain()
                    {
                        UsuarioId = resposta.UsuarioId,
                        PerguntaId = resposta.PerguntaId,
                        QuestionarioId = resposta.QuestionarioId,
                        Texto = resposta.Texto

                    });
                }
                foreach (RespostasDomain resposta in listarespostas)
                {
                    foreach (var resp in respostascadastradas)
                    {
                        if (resp.QuestionarioId == resposta.QuestionarioId && resp.PerguntaId == resposta.PerguntaId)
                        {
                            return BadRequest("Resposta: " + "'" + resposta.Pergunta.Enunciado + "'" + " já cadastrada para esse usuário");
                        }
                    }

                    _repo.Inserir(resposta);
                };

                return Ok(Json("Realizado com sucesso"));
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}