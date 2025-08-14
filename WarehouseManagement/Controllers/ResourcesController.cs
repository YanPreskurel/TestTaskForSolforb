using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Data;
using WarehouseManagement.Models;
using WarehouseManagement.Models.Entities;

namespace WarehouseManagement.Controllers
{
    public class ResourcesController : Controller
    {
        public ResourcesController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
