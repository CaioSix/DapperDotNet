using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetDapper.Models
{
    public class FilmeResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Ano { get; set; }
        public int Duracao { get; set; }
        
    }
}