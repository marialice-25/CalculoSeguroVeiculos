using CalculoSeguroVeiculos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculoSeguroVeiculos.Domain.Interfaces
{
    public interface ISeguroRepository
    {
        Task<List<Seguro>> ObterTodosSegurosAsync();
        Task<Seguro> CalcularSeguroAsync(Seguro seguro);
        Task<List<Seguro>> PesquisarAsync(string termo);
        Task AdicionarSeguroAsync(Seguro seguro);
        Task AdicionarVeiculoAsync(Veiculo veiculo);
        Task AdicionarSeguradoAsync(Segurado segurado);
    }
}
