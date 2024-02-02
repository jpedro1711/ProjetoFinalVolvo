using ConcessionariaAPI.Models;
using ConcessionariaAPI.Models.ViewModels;
using ConcessionariaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConcessionariaAPI.Controllers.ViewControllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendasController : Controller
    {
        private IService<Venda> _service;
        private IVeiculoService<Veiculo> _veiculoService;
        public IVendedorService<Vendedor> _vendedorService;

        public VendasController()
        {
            _service = new VendaServico(new ConcessionariaContext());
            _veiculoService = new VeiculoService(new ConcessionariaContext());
            _vendedorService = new VendedorService(new ConcessionariaContext());
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await this._service.GetAll());
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var venda = await _service.GetById(id);
                return View(venda);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }

        }

        [HttpGet("ConfirmDelete/{id}")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            try
            {
                var venda = await _service.GetById(id);
                return View(venda);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }

        }

        [HttpGet("Delete/{VendaId}")]
        public async Task<IActionResult> Delete(int VendaId)
        {
            await _service.Delete(VendaId);
            return RedirectToAction("Index");
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var venda = await _service.GetById(id);
                return View(venda);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }

        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            var cars = await _veiculoService.GetAll();
            var sellers = await _vendedorService.GetAll();
            var viewModel = new VendaFormViewModel { Veiculos = cars, Vendedores = sellers };
            return View(viewModel);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] Venda venda)
        {
            var veiculo = await _veiculoService.GetById(venda.VeiculoId);
            venda.Veiculo.NumeroChassi = veiculo.NumeroChassi;
            venda.Veiculo.VersaoSistema = veiculo.VersaoSistema;

            await _service.Create(venda);
            return RedirectToAction("Index");
        }
    }
}