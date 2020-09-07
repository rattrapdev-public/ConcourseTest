using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetCoreLibrary.Domain.Organizations;

namespace NetCoreLibrary.Infrastructure
{
    public class OrganizationSqlRepository : IOrganizationRepository
    {
        private readonly NetCoreLibraryDbContext _context;

        public OrganizationSqlRepository(NetCoreLibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Organization> GetBy(OrganizationIdentifier organizationIdentifier)
        {
            var organizationDto =
                await _context.Organizations.FirstAsync(x => x.OrganizationId == organizationIdentifier.Id);

            return new Organization(organizationDto.OrganizationId, organizationDto.OrganizationName, organizationDto.IsEnabled);
        }

        public IEnumerable<Organization> SearchBy(string name)
        {
            var organizationDtos = _context.Organizations.Where(x => x.OrganizationName.Contains(name));

            return organizationDtos.Select(x => new Organization(x.OrganizationId, x.OrganizationName, x.IsEnabled));
        }

        public async Task Store(Organization organization)
        {
            if ((await _context.Organizations.AnyAsync(x => x.OrganizationId == organization.Identifier.Id)))
            {
                var dto = await _context.Organizations.FirstAsync(x => x.OrganizationId == organization.Identifier.Id);
                dto.OrganizationName = organization.Name.Name;
                dto.IsEnabled = organization.IsEnabled;
                _context.Organizations.Update(dto);
            }
            else
            {
                var dto = new OrganizationDto{OrganizationId = organization.Identifier.Id, OrganizationName = organization.Name.Name, IsEnabled = organization.IsEnabled};
                await _context.Organizations.AddAsync(dto);
            }

            await _context.SaveChangesAsync();
        }
    }
}