using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreLibrary.Application.ViewModels;
using NetCoreLibrary.Domain.Organizations;

namespace NetCoreLibrary.Application
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationService(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }
        
        public IEnumerable<OrganizationViewModel> Search(string searchText)
        {
            var organizations = _organizationRepository.SearchBy(searchText);
            var viewModels = organizations.Select(x => new OrganizationViewModel{OrganizationId = x.Identifier.Id, OrganizationName = x.Name.Name, IsEnabled = x.IsEnabled});

            return viewModels;
        }

        public async Task<OrganizationViewModel> GetBy(Guid organizationId)
        {
            var organization = await _organizationRepository.GetBy(new OrganizationIdentifier(organizationId));
            var viewModel = new OrganizationViewModel{OrganizationId = organization.Identifier.Id, OrganizationName = organization.Name.Name, IsEnabled = organization.IsEnabled};
            return viewModel;
        }

        public async Task SaveNew(string organizationName)
        {
            var organization = new Organization(organizationName);
            await _organizationRepository.Store(organization);
        }

        public async Task Update(OrganizationViewModel viewModel)
        {
            var organization = await _organizationRepository.GetBy(new OrganizationIdentifier(viewModel.OrganizationId));
            organization.UpdateName(viewModel.OrganizationName);
            await _organizationRepository.Store(organization);
        }

        public async Task Disable(Guid organizationId)
        {
            var organization = await _organizationRepository.GetBy(new OrganizationIdentifier(organizationId));
            organization.Disable();
            await _organizationRepository.Store(organization);
        }

        public async Task Enable(Guid organizationId)
        {
            var organization = await _organizationRepository.GetBy(new OrganizationIdentifier(organizationId));
            organization.Enable();
            await _organizationRepository.Store(organization);
        }
    }
}