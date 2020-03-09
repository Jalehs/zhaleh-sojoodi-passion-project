using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace passion_project.ViewModel.Identity
{
    public class RoleVM
    {
        public string RoleId { get; set; }
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
