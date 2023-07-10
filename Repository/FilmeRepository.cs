using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DotNetDapper.Models;

namespace DotNetDapper.Repository
{
    public class FilmeRepository : IFilmeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;

        public FilmeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("SqlConnection");
        }

        public async Task<FilmeResponse> BuscaFilmeAsync(int id)
        {
            string sql = @"SELECT [Id]
                        ,[Nome]
                        ,[Ano]
                        ,[Duracao]
                    FROM [Filmes].[dbo].[Filmes]
                    WHERE [id] = @Id;";
            using var con = new SqlConnection(connectionString);
            return await con.QueryFirstOrDefaultAsync<FilmeResponse>(sql, new {Id = id});
        }
        public async Task<IEnumerable<FilmeResponse>> BuscaFilmesAsync()
        {
            string sql = @"SELECT [Id]
                                ,[Nome]
                                ,[Ano]
                                ,[Duracao]
                            FROM [Filmes].[dbo].[Filmes]
                            ";
            using(var con = new SqlConnection(connectionString))
            {
                return await con.QueryAsync<FilmeResponse>(sql);
            }
        }
        public async Task<bool> AdicionaAsync(FilmeRequest request)
        {
            string sql = @"  INSERT INTO Filmes (Nome, Ano,Duracao)
                             VALUES (@Nome, @Ano, @Duracao)";
            using var con = new SqlConnection(connectionString);
            return await con.ExecuteAsync(sql, request) > 0;
        }

        public async Task<bool> AtualizarAsync(FilmeRequest request, int id)
        {
         string sql = @"  UPDATE Filmes 
                            SET 
                             nome = @Nome, 
                             ano = @Ano, 
                             duracao = @Duracao
                             WHERE id = @Id";

            var parametros = new DynamicParameters();
            parametros.Add("Nome",  request.Nome);
            parametros.Add("Ano",  request.Ano);
            parametros.Add("Duracao",  request.Duracao);
            parametros.Add("Id",  id);

            using var con = new SqlConnection(connectionString);
            return await con.ExecuteAsync(sql, parametros) > 0;
        }

        public async Task<bool> DeletarAsync(int id)
        {
        string sql = @"  DELETE FROM Filmes 
                             WHERE id = @Id";
        using var con = new SqlConnection(connectionString);
            return await con.ExecuteAsync(sql, new {Id = id}) > 0;        
        }
    }
}