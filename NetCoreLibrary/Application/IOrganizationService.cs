using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetCoreLibrary.Application.ViewModels;

namespace NetCoreLibrary.Application
{
    public interface IOrganizationService
    {
        IEnumerable<OrganizationViewModel> Search(string searchText);
        Task<OrganizationViewModel> GetBy(Guid organizationId);
        Task SaveNew(string organizationName);
        Task Update(OrganizationViewModel viewModel);
        Task Disable(Guid organizationId);
        Task Enable(Guid organizationId);
    }
}