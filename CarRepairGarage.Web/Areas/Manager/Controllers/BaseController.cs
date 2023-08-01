namespace CarRepairGarage.Web.Areas.Manager.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static Common.GeneralApplicationConstants;

    /// <summary>
    /// Base controller for all controllers in the Manager area.
    /// </summary>
    [Authorize(Roles = Roles.ManagerRole)]
    [Area("Manager")]
    public class BaseController : Controller
    {
    }
}
