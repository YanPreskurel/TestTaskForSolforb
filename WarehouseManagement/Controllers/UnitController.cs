using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Models.DTOs;
using WarehouseManagement.Models.Entities;
using WarehouseManagement.Services.Interfaces;

namespace WarehouseManagement.Controllers
{
    public class UnitController : Controller
    {
        private readonly IUnitService _unitService;
        private readonly IMapper _mapper;

        public UnitController(IUnitService unitService, IMapper mapper)
        {
            _unitService = unitService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(bool showArchive = false)
        {
            IEnumerable<Unit> units;

            if (showArchive)
            {
                units = await _unitService.GetAllQuery().Where(u => !u.IsActive).ToListAsync();
                ViewBag.ShowArchiveButton = true;
            }
            else
            {
                units = await _unitService.GetAllQuery().Where(u => u.IsActive).ToListAsync();
                ViewBag.ShowArchiveButton = false;
            }

            // Маппим на DTO
            var unitsDto = _mapper.Map<IEnumerable<UnitReadDto>>(units);

            return View(unitsDto);
        }


        // Добавление единицы измерения
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                TempData["Error"] = "Наименование не может быть пустым.";
                return RedirectToAction("Index");
            }

            var existing = await _unitService.GetByNameAsync(name);
            if (existing != null)
            {
                TempData["Error"] = "Такая единица измерения уже существует.";
                return RedirectToAction("Index");
            }

            var unit = new Unit
            {
                Name = name,
                IsActive = true
            };

            await _unitService.AddAsync(unit);
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            var dto = new UnitCreateDto();
            return View(dto);
        }

        

        public async Task<IActionResult> Archive()
        {
            var archived = await _unitService.GetActiveUnitsAsync();
            var unitsDto = _mapper.Map<IEnumerable<UnitReadDto>>(archived);
            ViewBag.ShowArchiveButton = true;
            return View("Index", unitsDto);
        }
    }
}
