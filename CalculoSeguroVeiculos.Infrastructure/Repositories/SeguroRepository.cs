using CalculoSeguroVeiculos.Application.DTOs;
using CalculoSeguroVeiculos.Domain.Entities;
using CalculoSeguroVeiculos.Domain.Interfaces;
using CalculoSeguroVeiculos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculoSeguroVeiculos.Infrastructure.Repositories
{
    public class SeguroRepository : ISeguroRepository
    {
        private readonly ApplicationDbContext _context;

        public SeguroRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Seguro>> ObterTodosSegurosAsync()
        {
            return await _context.Seguro.Include(s => s.Segurado).Include(v => v.Veiculo).ToListAsync();
        }

        public async Task<List<Seguro>> PesquisarAsync(string termo)
        {
            return await _context.Seguro
                .Include(s => s.Veiculo)
                .Include(s => s.Segurado)
                .Where(s =>
                    s.Segurado.Nome.Contains(termo) ||
                    s.Segurado.CPF.Contains(termo) ||
                    s.Veiculo.MarcaModelo.Contains(termo))
                .ToListAsync();
        }

        public async Task<decimal> ObterMediaSeguroAsync()
        {
            return await _context.Seguro.AverageAsync(s => s.ValorSeguro);
        }

        public async Task<Seguro> CalcularSeguroAsync(Seguro seguro)
        {
            throw new NotImplementedException();
        }

        public async Task AdicionarSeguroAsync(Seguro seguro)
        {
            _context.Seguro.Add(seguro);
            await _context.SaveChangesAsync();
        }

        public async Task AdicionarVeiculoAsync(Veiculo veiculo)
        {
            _context.Veiculo.Add(veiculo);
            await _context.SaveChangesAsync();
        }

        public async Task AdicionarSeguradoAsync(Segurado segurado)
        {
            _context.Segurado.Add(segurado);
            await _context.SaveChangesAsync();
        }
    }
}
