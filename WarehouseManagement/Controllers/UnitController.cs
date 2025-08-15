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
        private readonly IReceiptDocumentService _receiptDocumentService;
        private readonly IMapper _mapper;

        public UnitController(IUnitService unitService, IReceiptDocumentService receiptDocumentService, IMapper mapper)
        {
            _unitService = unitService;
            _receiptDocumentService = receiptDocumentService;
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

            var unitsDto = _mapper.Map<IEnumerable<UnitReadDto>>(units);

            return View(unitsDto);
        }


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

        public async Task<IActionResult> Edit(int id)
        {
            var unit = await _unitService.GetByIdAsync(id);
            if (unit == null)
                return NotFound();

            var dto = new UnitUpdateDto
            {
                Id = unit.Id,
                Name = unit.Name,
                IsActive = unit.IsActive
            };

            return View(dto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UnitUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var existing = await _unitService.GetByIdAsync(dto.Id);
            if (existing == null)
                return NotFound();

            existing.Name = dto.Name;

            await _unitService.UpdateAsync(existing);

            TempData["Success"] = "Единица измерения сохранена.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Archive(int id)
        {
            var unit = await _unitService.GetByIdAsync(id);
            if (unit == null) return NotFound();

            unit.IsActive = false;
            await _unitService.UpdateAsync(unit);
            TempData["Success"] = "Единица измерения отправлена в архив.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var unit = await _unitService.GetByIdAsync(id);
            if (unit == null) return NotFound();

            var usedDocuments = await _receiptDocumentService.GetByUnitsAsync(new List<int> { id });

            if (usedDocuments.Any())
            {
                TempData["Error"] = "Эта единица измерения используется и не может быть удалена. Можно только архивировать.";
                return RedirectToAction("Edit", new { id });
            }

            await _unitService.DeleteAsync(id);
            TempData["Success"] = "Единица измерения удалена.";
            return RedirectToAction("Index");
        }
    }
}
