using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetDapper.Models;

namespace DotNetDapper.Repository
{
    public interface IFilmeRepository
    {
        Task<IEnumerable<FilmeResponse>> BuscaFilmesAsync();

        Task<FilmeResponse> BuscaFilmeAsync(int id);

        Task<bool> AdicionaAsync(FilmeRequest request);

        Task<bool> AtualizarAsync(FilmeRequest request, int id);

        Task<bool> DeletarAsync(int id);
    }
}