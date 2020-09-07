using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetCoreLibrary.Domain.Organizations;
using NetCoreLibrary.Domain.Users;

namespace NetCoreLibrary.Infrastructure
{
    public class UserSqlRepository : IUserRepository
    {
        private readonly NetCoreLibraryDbContext _context;

        public UserSqlRepository(NetCoreLibraryDbContext context)
        {
            _context = context;
        }
        
        public async Task<User> GetBy(UserIdentifier identifier)
        {
            var dto =
                await _context.Users.FirstAsync(x => x.UserId == identifier.Id);

            return new User(dto.UserId, dto.Username, dto.Email, dto.HashedPassword, dto.OrganizationId);
        }

        public IEnumerable<User> GetBy(OrganizationIdentifier organizationIdentifier)
        {
            var dtos = _context.Users.Where(x => x.OrganizationId.Equals(organizationIdentifier.Id));

            return dtos.Select(x => new User(x.UserId, x.Username, x.Email, x.HashedPassword, x.OrganizationId));
        }

        public async Task Store(User user)
        {
            if ((await _context.Users.AnyAsync(x => x.UserId == user.Identifier.Id)))
            {
                var dto = await _context.Users.FirstAsync(x => x.UserId == user.Identifier.Id);
                dto.Username = user.Credentials.Username;
                dto.Email = user.Credentials.Email;
                dto.HashedPassword = user.Password.HashedPassword;
                _context.Users.Update(dto);
            }
            else
            {
                var dto = new UserDto
                {
                    UserId = user.Identifier.Id, Username = user.Credentials.Username, Email = user.Credentials.Email,
                    HashedPassword = user.Password.HashedPassword, OrganizationId = user.OrganizationIdentifier.Id
                };
                await _context.Users.AddAsync(dto);
            }

            await _context.SaveChangesAsync();
        }
    }
}