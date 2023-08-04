using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CarRepairGarage.Common.GeneralApplicationConstants;

namespace CarRepairGarage.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = Roles.AdminRole)]
    [Area("Admin")]
    public class BaseController : Controller
    {
    }
}
