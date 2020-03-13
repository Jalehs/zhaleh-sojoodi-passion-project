using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using passion_project.Models.HealthCenter;
using passion_project.Repository;

namespace passion_project.Controllers
{
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RoleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Role
        public IActionResult Index()
        {
            RoleRepository roleRepo = new RoleRepository(_context);
            return View(roleRepo.GetAllRoles());
        }

    }
}