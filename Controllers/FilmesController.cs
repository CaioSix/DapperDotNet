using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetDapper.Models;
using DotNetDapper.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DotNetDapper.obj
{


    [ApiController]
    [Route("api/[controller]")]
    public class FilmesController : ControllerBase
    {
        private readonly IFilmeRepository _repository;

        public FilmesController(IFilmeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("BuscaTodos")]
        public async Task<IActionResult> Get()
        {
            var filmes = await _repository.BuscaFilmesAsync();

             return filmes.Any() ? Ok(filmes) : NoContent();
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetbyID(int id)

        {
            var filme = await _repository.BuscaFilmeAsync(id);

             return filme != null? Ok(filme) : NotFound("Filme não encontrado");
        }

        [HttpPost("criar")]
        public async Task<IActionResult> Post(FilmeRequest request)
        {
            if(string.IsNullOrEmpty(request.Nome) || request.Ano <= 0)
            {
                return BadRequest("Informações invalidas");
            }

            var adicionado = await _repository.AdicionaAsync(request);

            return adicionado 
                ? Ok("Filme adicionado com sucesso")
                : BadRequest("Erro ao adicionar");

        }
 
        [HttpPut("id")]
        public async Task<IActionResult> Put(FilmeRequest request, int id)
        {
            if(id <=0) return BadRequest("Filme invalido");
            
            var filme = await _repository.BuscaFilmeAsync(id);

            if(filme == null) NotFound("Filme nao exite");

            if(string.IsNullOrEmpty(request.Nome)) request.Nome = filme.Nome;
            if(request.Ano <= 0) request.Ano = filme.Ano;

            var atualizado = await _repository.AtualizarAsync(request, id );
            
            return atualizado 
                ? Ok("Filme adicionado com sucesso")
                : BadRequest("Erro ao atualizar");
     

        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id ){
            if(id <=0) return BadRequest("Filme invalido");
            
            var filme = await _repository.BuscaFilmeAsync(id);

            if(filme == null) NotFound("Filme nao exite");

            var deletado = await _repository.DeletarAsync(id);

            return deletado 
                ? Ok("Filme deletado com sucesso")
                : BadRequest("Erro ao deletar");
        }
    }
}