using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CarRepairGarage.Common.GeneralApplicationConstants;

namespace CarRepairGarage.Web.Areas.Test.Controllers
{
    [Authorize(Roles = Common.GeneralApplicationConstants.Roles.ManagerRole)]
    [Area("Test")]
    public class BaseController : Controller
    {
    }
}
