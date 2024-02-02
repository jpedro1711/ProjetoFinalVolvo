using ConcessionariaAPI.Models;
using ConcessionariaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConcessionariaAPI.Controllers.ViewControllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendasController : Controller
    {
        private IService<Venda> _service;

        public VendasController()
        {
            _service = new VendaServico(new ConcessionariaContext());
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

        [HttpGet("Create")]
        public IActionResult Create() 
        {
            return View();
        }
    }
}
