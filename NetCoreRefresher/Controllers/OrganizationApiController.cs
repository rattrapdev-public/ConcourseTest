using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreLibrary.Application;
using NetCoreLibrary.Application.ViewModels;
using NetCoreRefresher.Authorization;

namespace NetCoreRefresher.Controllers
{
    [Route("api/organizations")]
    [ApiController]
    [Authorize]
    public class OrganizationApiController : Controller
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationApiController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }
        
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string searchText)
        {
            var organizations = _organizationService.Search(searchText);
            return Json(organizations);
        }
        
        [HttpGet("{Id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var organization = await _organizationService.GetBy(id);
            return Json(organization);
        }

        [HttpPost("new")]
        public async Task<IActionResult> SaveNew([FromForm] string organizationName)
        {
            await _organizationService.SaveNew(organizationName);
            return Ok();
        }
        
        [HttpPost("")]
        public async Task<IActionResult> Update([FromForm] OrganizationViewModel viewModel)
        {
            await _organizationService.Update(viewModel);
            return Ok();
        }

        [HttpDelete("{id:Guid}/disable")]
        [Authorize(Policy = AdminApiKeyRequirement.AdminApiRequirement)]
        public async Task<IActionResult> Disable(Guid id)
        {
            await _organizationService.Disable(id);
            return Ok();
        }

        [HttpPost("{id:Guid}/enable")]
        [Authorize(Policy = AdminApiKeyRequirement.AdminApiRequirement)]
        public async Task<IActionResult> Enable(Guid id)
        {
            await _organizationService.Enable(id);
            return Ok();
        }
    }
}