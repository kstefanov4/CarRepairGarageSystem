namespace CarRepairGarage.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    
    [Authorize]
    public class BaseController : Controller
    {
    }
}
