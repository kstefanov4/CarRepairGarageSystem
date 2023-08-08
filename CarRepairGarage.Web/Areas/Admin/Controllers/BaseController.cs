namespace CarRepairGarage.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static CarRepairGarage.Common.GeneralApplicationConstants;

    /// <summary>
    /// The base controller for the admin area.
    /// </summary>
    /// <remarks>
    /// This controller is the base for all controllers in the Admin area and provides common functionality and authorization settings.
    /// </remarks>
    [Authorize(Roles = Roles.AdminRole)]
    [Area("Admin")]
    public class BaseController : Controller
    {
    }
}
