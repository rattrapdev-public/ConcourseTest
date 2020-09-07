using System.Collections.Generic;
using System.Threading.Tasks;
using NetCoreLibrary.Domain.Organizations;

namespace NetCoreLibrary.Domain.Users
{
    public interface IUserRepository
    {
        Task<User> GetBy(UserIdentifier identifier);
        IEnumerable<User> GetBy(OrganizationIdentifier organizationIdentifier);
        Task Store(User user);
    }
}