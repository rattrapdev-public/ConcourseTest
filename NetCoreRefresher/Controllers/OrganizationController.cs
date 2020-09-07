using System;
using Microsoft.AspNetCore.Mvc;
using NetCoreLibrary.Application;

namespace NetCoreRefresher.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        public IActionResult Index([FromQuery] string searchText)
        {
            if (searchText == null)
            {
                searchText = String.Empty;
            }
            var viewModels = _organizationService.Search(searchText);
            return View("Results", viewModels);
        }
    }
}