using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using passion_project.Models.HealthCenter;
using passion_project.Repository;
using passion_project.ViewModel.Identity;

namespace passion_project.Controllers
{
    public class UserRoleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IServiceProvider _serviceProvider;

        public UserRoleController(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;

        }
        public IActionResult Index()
        {
            UserRepository userRepo = new UserRepository(_context);
            return View(userRepo.GetAllUsers());
        }

        public async Task<IActionResult> Detail(string userName) 
        { 
            UserRoleRepository userRoleRepo = new UserRoleRepository(_serviceProvider); 
            var roles = await userRoleRepo.GetUserRole(userName); 
            ViewBag.UserName = userName; 
            return View(roles); 
        }

        public IActionResult Create(string userName)
        {
            ViewBag.SelectedUser = userName;
            RoleRepository roleRepo = new RoleRepository(_context);
            var roles = roleRepo.GetAllRoles().ToList();
            var preRoleList = roles.Select(r => new SelectListItem { Value = r.RoleName, Text = r.RoleName}).ToList();
            var roleList = new SelectList(preRoleList, "Value", "Text");
            ViewBag.RoleSelectList = roleList;

            UserRepository userRepo = new UserRepository(_context);
            var users = userRepo.GetAllUsers().ToList();
            var preUserList = users.Select(u => new SelectListItem { Value = u.Email, Text = u.Email });
            SelectList userList = new SelectList(preUserList, "Value", "Text");
            ViewBag.UserSelectList = userList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserRoleVM userRoleModel)
        {
            UserRoleRepository userRoleRepo = new UserRoleRepository(_serviceProvider);
            if (ModelState.IsValid)
            {
                var usr = await userRoleRepo.AddUserRole(userRoleModel.Email, userRoleModel.Role);
            }
            try
            {
                return RedirectToAction("Index", "UserRole");
            }
            catch
            {
                return View();
            }
        }

    }
}