using CalculoSeguroVeiculos.Application.DTOs;
using CalculoSeguroVeiculos.Application.Interfaces;
using CalculoSeguroVeiculos.Domain.Entities;
using CalculoSeguroVeiculos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculoSeguroVeiculos.Application.Services
{
    public class SeguroService : ISeguroService
    {
        private readonly ISeguroRepository _seguroRepository;

        private const decimal MARGEM_SEGURANCA = 0.03m;
        private const decimal LUCRO = 0.05m;

        public SeguroService(ISeguroRepository seguroRepository)
        {
            _seguroRepository = seguroRepository;
        }

        public async Task<IEnumerable<Seguro>> GetSeguros()
        {
            return await _seguroRepository.ObterTodosSegurosAsync();
        }

        public async Task<List<Seguro>> PesquisarSeguroAsync(string termo)
        {
            return await _seguroRepository.PesquisarAsync(termo);
        }

        public async Task<Seguro> CalcularSeguroAsync(SeguroDto dto)
        {
            if (dto.ValorVeiculo <= 0)
            {
                throw new ArgumentException("O valor do veículo deve ser maior que zero");
            }

            // Realiza os cálculos
            var taxaRisco = (dto.ValorVeiculo * 5) / (2 * dto.ValorVeiculo); //em %
            var premioRisco = (taxaRisco * 0.01m) * dto.ValorVeiculo;
            var premioPuro = premioRisco * (1 + MARGEM_SEGURANCA);
            var premioComercial = premioPuro * (1 + LUCRO);

            // Cria as entidades com os dados fornecidos pelo usuário
            var veiculo = new Veiculo
            {
                Valor = dto.ValorVeiculo,
                MarcaModelo = dto.MarcaModelo
            };

            var segurado = new Segurado
            {
                Nome = dto.NomeSegurado,
                CPF = dto.CPF,
                Idade = dto.Idade,
            };

            // Armazena os dados no repositório
            await _seguroRepository.AdicionarVeiculoAsync(veiculo);
            await _seguroRepository.AdicionarSeguradoAsync(segurado);

            var seguro = new Seguro
            {
                Veiculo = veiculo,
                Segurado = segurado,
                VeiculoID = veiculo.VeiculoID,
                SeguradoID = segurado.SeguradoID,
                ValorSeguro = Math.Round(premioComercial, 2),
                TaxaRisco = Math.Round(taxaRisco, 2),
                PremioRisco = Math.Round(premioRisco, 2),
                PremioPuro = Math.Round(premioPuro, 2),
                PremioComercial = Math.Round(premioComercial, 2)
            };

            // Armazena o seguro no repositório
            await _seguroRepository.AdicionarSeguroAsync(seguro);

            // Retorna o objeto Seguro com todos os dados
            return seguro;
        }
    }
}
