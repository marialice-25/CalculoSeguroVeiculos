using CalculoSeguroVeiculos.Application.Services;
using CalculoSeguroVeiculos.Domain.Entities;
using CalculoSeguroVeiculos.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CalculoSeguroSeguros.WebUI.Controllers
{
    public class SeguroController : Controller
    {
        private readonly HttpClient _httpClient;

        public SeguroController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5281/api/seguros/");
        }

        public async Task<IActionResult> Index(SeguroModel seguro)
        {
            var model = seguro ?? new SeguroModel();
            model.Seguros = await ObterTodosOsSeguros();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CalcularSeguro(SeguroModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("", content);

            if (response.IsSuccessStatusCode) {
                var responseContent = await response.Content.ReadAsStringAsync();
                var seguroData = JsonConvert.DeserializeObject<SeguroModel>(responseContent);
                seguroData.ValorVeiculo = model.ValorVeiculo;

                return RedirectToAction("Index", seguroData);
            }
            else {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Error", new { message = errorMessage });
            }
        }

        private async Task<List<SeguroModel>> ObterTodosOsSeguros()
        {
            var response = await _httpClient.GetAsync("todos");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<SeguroModel>>(responseContent) ?? new List<SeguroModel>();
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
            }

            return new List<SeguroModel>();
        }

        public async Task<IActionResult> PesquisarSeguro(string searchTerm)
        {
            var model = new SeguroModel();

            if (string.IsNullOrEmpty(searchTerm))
            {
                ModelState.AddModelError("", "Informe um CPF, Nome ou Modelo para pesquisar");
                model.Seguros = new List<SeguroModel>();
                return View("Index", model);
            }

            var response = await _httpClient.GetAsync($"pesquisar?termo={searchTerm}");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var seguros = JsonConvert.DeserializeObject<List<SeguroModel>>(responseContent);
                model.Seguros = seguros;
                return View("Index", model);
            }

            ModelState.AddModelError("", "Nenhum seguro encontrado");
            model.Seguros = new List<SeguroModel>();
            return View("Index", model);
        }


        public IActionResult Error(string message)
        {
            ViewBag.ErrorMessage = message;
            return View();
        }
    }
}