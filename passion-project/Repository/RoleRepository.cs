using Microsoft.AspNetCore.Identity;
using passion_project.Models.HealthCenter;
using passion_project.ViewModel.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace passion_project.Repository
{
    public class RoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
            var rolesCreated = CreateInitialRoles();
        }

        public IEnumerable<RoleVM> GetAllRoles()
        {
            return _context.Roles.Select(r => new RoleVM
            {
                RoleId = r.Id,
                RoleName = r.Name
            }).ToList();

        }

        public RoleVM GetRole(string roleName)
        {
            var role = _context.Roles.Where(r => r.Name == roleName).FirstOrDefault();
            if (role != null)
            {
                return new RoleVM { RoleName = role.Name, RoleId = role.Id };
            }
            return null;
        }

        public bool CreateRole(string roleName)
        {
            var role = GetRole(roleName);
            if (role != null)
            {
                return false;
            }
            _context.Roles.Add(new IdentityRole
            {
                Name = roleName,
                Id = roleName,
                NormalizedName = roleName.ToUpper()
            });
            _context.SaveChanges();
            return true;
        }

        public bool CreateInitialRoles()
        {
            string[] roleNames = { "Admin", "Doctor", "Patient" };
            foreach (var role in roleNames)
            {
                var created = CreateRole(role);
                if (!created)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
