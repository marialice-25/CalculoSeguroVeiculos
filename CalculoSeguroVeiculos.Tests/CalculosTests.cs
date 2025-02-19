using CalculoSeguroVeiculos.Application.DTOs;
using CalculoSeguroVeiculos.Application.Services;
using CalculoSeguroVeiculos.Domain.Entities;
using CalculoSeguroVeiculos.Domain.Interfaces;
using Moq;

namespace CalculoSeguroVeiculos.Tests
{
    public class CalculosTests
    {
        private readonly Mock<ISeguroRepository> _mockSeguroRepository;
        private readonly SeguroService _seguroService;

        public CalculosTests()
        {
            _mockSeguroRepository = new Mock<ISeguroRepository>();
            _seguroService = new SeguroService(_mockSeguroRepository.Object);
        }

        [Fact]
        public async Task CalcularSeguroAsync_DeveCalcularValoresCorretamente()
        {
            // Arrange
            var dto = new SeguroDto
            {
                ValorVeiculo = 50000m,
                MarcaModelo = "Toyota Corolla",
                NomeSegurado = "João Silva",
                CPF = "12345678900",
                Idade = 30
            };

            // Act
            var resultado = await _seguroService.CalcularSeguroAsync(dto);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(50000m, resultado.Veiculo.Valor);
            Assert.Equal("Toyota Corolla", resultado.Veiculo.MarcaModelo);
            Assert.Equal("João Silva", resultado.Segurado.Nome);
            Assert.Equal(30, resultado.Segurado.Idade);

            // Cálculo esperado baseado na fórmula da SeguroService
            var taxaRiscoEsperada = (dto.ValorVeiculo * 5) / (2 * dto.ValorVeiculo); // Deve ser 2.5%
            var premioRiscoEsperado = (taxaRiscoEsperada * 0.01m) * dto.ValorVeiculo;
            var premioPuroEsperado = premioRiscoEsperado * (1 + 0.03m);
            var premioComercialEsperado = premioPuroEsperado * (1 + 0.05m);
            var valorSeguroEsperado = Math.Round(premioComercialEsperado, 2);

            Assert.Equal(Math.Round(taxaRiscoEsperada, 2), resultado.TaxaRisco);
            Assert.Equal(Math.Round(premioRiscoEsperado, 2), resultado.PremioRisco);
            Assert.Equal(Math.Round(premioPuroEsperado, 2), resultado.PremioPuro);
            Assert.Equal(valorSeguroEsperado, resultado.ValorSeguro);
        }

        [Fact]
        public async Task CalcularSeguroAsync_DeveAdicionarVeiculoESeguradoNoRepositorio()
        {
            // Arrange
            var dto = new SeguroDto
            {
                ValorVeiculo = 40000m,
                MarcaModelo = "Honda Civic",
                NomeSegurado = "Maria Oliveira",
                CPF = "98765432100",
                Idade = 28
            };

            // Act
            await _seguroService.CalcularSeguroAsync(dto);

            // Assert: Verifica se os métodos do repositório foram chamados corretamente
            _mockSeguroRepository.Verify(repo => repo.AdicionarVeiculoAsync(It.IsAny<Veiculo>()), Times.Once);
            _mockSeguroRepository.Verify(repo => repo.AdicionarSeguradoAsync(It.IsAny<Segurado>()), Times.Once);
            _mockSeguroRepository.Verify(repo => repo.AdicionarSeguroAsync(It.IsAny<Seguro>()), Times.Once);
        }

        [Theory]
        [InlineData(0)]  // Testa caso onde o valor do veículo é 0
        [InlineData(-10000)] // Testa caso onde o valor do veículo é negativo
        public async Task CalcularSeguroAsync_DeveLancarExcecao_SeValorVeiculoInvalido(decimal valorVeiculo)
        {
            // Arrange
            var dto = new SeguroDto
            {
                ValorVeiculo = valorVeiculo,
                MarcaModelo = "Ford Focus",
                NomeSegurado = "Carlos Mendes",
                CPF = "11122233344",
                Idade = 40
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _seguroService.CalcularSeguroAsync(dto));
        }
    }
}