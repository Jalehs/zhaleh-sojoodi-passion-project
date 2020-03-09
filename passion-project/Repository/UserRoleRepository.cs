using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using passion_project.Model;
using passion_project.ViewModel.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace passion_project.Repository
{
    public class UserRoleRepository
    {
        IServiceProvider _serviceProvider;

        public UserRoleRepository(IServiceProvider serviceProviderxt)
        {
            _serviceProvider = serviceProviderxt;
        }

        public async Task<bool> AddUserRole(string email, string roleName)
        {
            var UserManager = _serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var user = await UserManager.FindByEmailAsync(email);
            if (user != null)
            {
                await UserManager.AddToRoleAsync(user, roleName);
            }
            return true;
        }

        public async Task<bool> RemoveUserRole(string email, string roleName)
        {
            var UserManager = _serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var user = await UserManager.FindByEmailAsync(email);
            if (user != null)
            {
                await UserManager.RemoveFromRoleAsync(user, roleName);
            }
            return true;
        }

        public async Task<IEnumerable<RoleVM>> GetUserRole(string email)
        {
            var UserManager = _serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var user = await UserManager.FindByEmailAsync(email);
            var roles = await UserManager.GetRolesAsync(user);

            List<RoleVM> roleVMObjects = new List<RoleVM>();
            foreach (var role in roles)
            {
                roleVMObjects.Add(new RoleVM { RoleId = role, RoleName = role });
            }
            return roleVMObjects;
        }
    }
}
