namespace CarRepairGarage.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Base controller class that provides common functionality and is authorized for all actions.
    /// </summary>
    [Authorize]
    public class BaseController : Controller
    {
    }
}
