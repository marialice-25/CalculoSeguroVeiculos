using CalculoSeguroVeiculos.Application.DTOs;
using CalculoSeguroVeiculos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculoSeguroVeiculos.Application.Interfaces
{
    public interface ISeguroService
    {
        Task<IEnumerable<Seguro>> GetSeguros();
        Task<Seguro> CalcularSeguroAsync(SeguroDto dto);
        Task<List<Seguro>> PesquisarSeguroAsync(string termo);
    }
}
