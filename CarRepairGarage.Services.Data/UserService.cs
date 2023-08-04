namespace CarRepairGarage.Services
{
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Web.ViewModels.User;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a service for managing users in the application.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Asynchronously retrieves all users.
        /// </summary>
        /// <returns>A collection of <see cref="UserViewModel"/>.</returns>
        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            List<UserViewModel> userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var firstName = await GetUserName(user, "FirstName");
                var lastName = await GetUserName(user, "LastName");
                var role = await _userManager.GetRolesAsync(user);

                UserViewModel userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    FirstName = firstName,
                    LastName = lastName,
                    Role = role.FirstOrDefault(),
                    EMail = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    IsDeleted = user.IsDeleted,
                    DeleteOn = user.DeletedOn.ToString()
                };
                userViewModels.Add(userViewModel);
            }

            return userViewModels;
        }

        /// <summary>
        /// Retrieves the specified claim for a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="name">The name of the claim.</param>
        /// <returns>The value of the claim, or null if not found.</returns>
        internal async Task<string?> GetUserName(ApplicationUser user, string name)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var firstNameClaim = claims.FirstOrDefault(c => c.Type == name);

            return firstNameClaim?.Value;
        }
    }
}
