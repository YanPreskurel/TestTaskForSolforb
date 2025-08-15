using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Models.DTOs;
using WarehouseManagement.Models.Entities;
using WarehouseManagement.Services;
using WarehouseManagement.Services.Interfaces;

namespace WarehouseManagement.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IResourceService _resourceService;
        private readonly IReceiptDocumentService _receiptDocumentService;
        private readonly IMapper _mapper;

        public ResourceController(
            IResourceService resourceService,
            IReceiptDocumentService receiptDocumentService,
            IMapper mapper)
        {
            _resourceService = resourceService;
            _receiptDocumentService = receiptDocumentService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(bool showArchive = false)
        {
            IEnumerable<Resource> resources;

            if (showArchive)
            {
                resources = await _resourceService.GetAllQuery().Where(u => !u.IsActive).ToListAsync();
                ViewBag.ShowArchiveButton = true;
            }
            else
            {
                resources = await _resourceService.GetAllQuery().Where(u => u.IsActive).ToListAsync();
                ViewBag.ShowArchiveButton = false;
            }

            var resourcesDto = _mapper.Map<IEnumerable<ResourceReadDto>>(resources);

            return View(resourcesDto);
        }

        public IActionResult Create()
        {
            var dto = new ResourceCreateDto();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                TempData["Error"] = "Наименование не может быть пустым.";
                return RedirectToAction("Index");
            }

            var existing = await _resourceService.GetByNameAsync(name);
            if (existing != null)
            {
                TempData["Error"] = "Такой ресурс уже существует.";
                return RedirectToAction("Index");
            }

            var resource = new Resource
            {
                Name = name,
                IsActive = true
            };

            await _resourceService.AddAsync(resource);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Archive()
        {
            var archived = await _resourceService.GetAllQuery().Where(r => r.IsActive != true).ToListAsync();
            var resourcesDto = _mapper.Map<IEnumerable<ResourceReadDto>>(archived);
            return View("Index", resourcesDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var resource = await _resourceService.GetByIdAsync(id);
            if (resource == null)
                return NotFound();

            var dto = new ResourceUpdateDto
            {
                Id = resource.Id,
                Name = resource.Name,
                IsActive = resource.IsActive
            };


            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ResourceUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var existing = await _resourceService.GetByIdAsync(dto.Id);
            if (existing == null)
                return NotFound();

            existing.Name = dto.Name;
            await _resourceService.UpdateAsync(existing);

            TempData["Success"] = "Ресурс сохранен.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Archive(int id)
        {
            var resource = await _resourceService.GetByIdAsync(id);
            if (resource == null) return NotFound();

            resource.IsActive = false;
            await _resourceService.UpdateAsync(resource);

            TempData["Success"] = "Ресурс отправлен в архив.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var resource = await _resourceService.GetByIdAsync(id);
            if (resource == null) return NotFound();

            var usedDocuments = await _receiptDocumentService.GetByResourcesAsync(new List<int> { id });

            if (usedDocuments.Any())
            {
                TempData["Error"] = "Этот ресурс используется в документах и не может быть удален. Можно только архивировать.";
                return RedirectToAction("Edit", new { id });
            }

            await _resourceService.DeleteAsync(id);
            TempData["Success"] = "Ресурс удален.";

            return RedirectToAction("Index");         
        }

    }
}
