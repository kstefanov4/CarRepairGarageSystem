using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CarRepairGarage.Common.GeneralApplicationConstants;

namespace CarRepairGarage.Web.Areas.Manager.Controllers
{
    [Authorize(Roles = Common.GeneralApplicationConstants.Roles.ManagerRole)]
    [Area("Manager")]
    public class BaseController : Controller
    {
    }
}
