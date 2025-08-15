using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.Design;
using WarehouseManagement.Models.DTOs;
using WarehouseManagement.Models.Entities;
using WarehouseManagement.Services;
using WarehouseManagement.Services.Interfaces;
using IResourceService = WarehouseManagement.Services.Interfaces.IResourceService;

namespace WarehouseManagement.Controllers
{
    public class ReceiptDocumentController : Controller
    {
        private readonly IReceiptDocumentService _receiptService;
        private readonly IResourceService _resourceService;
        private readonly IUnitService _unitService;
        private readonly IMapper _mapper;

        public ReceiptDocumentController(IReceiptDocumentService receiptService, IResourceService resourceService, IUnitService unitService, IMapper mapper)
        {
            _receiptService = receiptService;
            _resourceService = resourceService;
            _unitService = unitService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var documents = await _receiptService.GetAllAsync();
            var dtoList = _mapper.Map<IEnumerable<ReceiptDocumentReadDto>>(documents);
            return View(dtoList);
        }

        public IActionResult Create()
        {
            var dto = new ReceiptDocumentCreateDto();

            ViewBag.Resources = new SelectList(_resourceService.GetAllQuery().Where(r => r.IsActive), "Id", "Name");
            ViewBag.Units = new SelectList(_unitService.GetAllQuery().Where(u => u.IsActive), "Id", "Name");

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReceiptDocumentCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Resources = new SelectList(_resourceService.GetAllQuery().Where(r => r.IsActive), "Id", "Name");
                ViewBag.Units = new SelectList(_unitService.GetAllQuery().Where(u => u.IsActive), "Id", "Name");
                return View(dto);
            }

            var entity = _mapper.Map<ReceiptDocument>(dto);
            await _receiptService.AddAsync(entity);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Filter(string number, DateTime? from, DateTime? to, List<int>? resourceIds, List<int>? unitIds)
        {
            var docs = await _receiptService.GetFilteredAsync(number, resourceIds, unitIds, from, to);
            var dtoList = _mapper.Map<IEnumerable<ReceiptDocumentReadDto>>(docs);

            // Здесь нужно использовать SelectList с выбранными элементами
            ViewBag.Resources = new SelectList(await _resourceService.GetAllResourcesAsync(), "Id", "Name", resourceIds);
            ViewBag.Units = new SelectList(await _unitService.GetAllUnitsAsync(), "Id", "Name", unitIds);

            return View("Index", dtoList);
        }
    }
}
