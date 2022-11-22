using EuromonBooks.Database.Abstractions;
using EuromonBooks.Database.Abstractions.Models;
using EuromonBooks.Database.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace EuromonBooks.Database
{
    public abstract partial class BaseDbContext : IDatabase
    {
        public async Task<List<RoleQuery>> GetAllRoles()
        {
            return await Roles.AsNoTracking().ToListAsync();
        }

        public async Task<RoleQuery> GetRoleDetails(int id)
        {
            return await Roles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IdQuery> CreateRole(RoleQuery request)
        {
            await Roles.AddAsync(request);

            await SaveChangesAsync();

            return new IdQuery { Id = request.Id.Value };
        }

        public async Task UpdateRole(RoleQuery request)
        {
            Roles.Update(request);

            await SaveChangesAsync();
        }
    }
}