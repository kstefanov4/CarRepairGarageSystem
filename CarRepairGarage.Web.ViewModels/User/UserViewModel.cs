using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairGarage.Web.ViewModels.User
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string EMail { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public string? DeleteOn { get; set; }
    }
}
