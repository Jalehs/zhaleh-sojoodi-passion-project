using passion_project.Models.HealthCenter;
using passion_project.ViewModel.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace passion_project.Repository
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<UserVM> GetAllUsers()
        {
            return _context.Users.Select(u => new UserVM() { Email = u.Email }).ToList();

        }
    }
}
