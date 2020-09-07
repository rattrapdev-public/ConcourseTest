using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreLibrary.Domain.Organizations
{
    public interface IOrganizationRepository
    {
        Task<Organization> GetBy(OrganizationIdentifier organizationIdentifier);
        IEnumerable<Organization> SearchBy(string name);
        Task Store(Organization organization);
    }
}