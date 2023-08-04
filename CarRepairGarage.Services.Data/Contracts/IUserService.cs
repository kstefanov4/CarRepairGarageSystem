namespace CarRepairGarage.Services.Contracts
{
    using CarRepairGarage.Web.ViewModels.User;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the contract for user-related operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Asynchronously retrieves all registered users in the system.
        /// </summary>
        /// <returns>A collection of <see cref="UserViewModel"/> representing the users.</returns>
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
    }
}
