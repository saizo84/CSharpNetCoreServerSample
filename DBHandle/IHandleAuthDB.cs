using DBEntities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace DBHandle
{
    public interface IHandleAuthDB
    {
        public Task<PlatformAccount> GetPlatformAccount(string platformId);
        public Task<PlatformAccount> GetPlatformAccountForUserId(long userId);
    }

    public class HandleAuthDB : IHandleAuthDB
    {
        private readonly AuthContext _authContext;

        public HandleAuthDB(AuthContext authContext)
        {
            _authContext = authContext;
        }

        public async Task<PlatformAccount> GetPlatformAccount(string platformId)
        {
            var platformAccount =
                await _authContext.PlatformAccounts
                .IncludeOptimized(p => p.PlatformAccountAuthorization)
                .SingleOrDefaultAsync(p => p.PlatformId == platformId);

            return platformAccount;
        }

        public async Task<PlatformAccount> GetPlatformAccountForUserId(long userId)
        {
            var platformAccount =
                await _authContext.PlatformAccounts
                .IncludeOptimized(p => p.PlatformAccountAuthorization)
                .SingleOrDefaultAsync(p => p.UserId == userId);

            return platformAccount;
        }
    }
}
