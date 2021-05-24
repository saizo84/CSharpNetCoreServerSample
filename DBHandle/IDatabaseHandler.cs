using DBEntities;
using System;
using System.Threading.Tasks;

namespace DBHandle
{
    public interface IDatabaseHandler
    {
        public IHandleAuthDB AuthDB { get; }
        public IHandleUserDB UserDB { get; }
        public void SaveChanges();
        public Task SaveChangesAsync();
    }

    public class DatabaseHandler : IDatabaseHandler
    {
        public AuthContext AuthContext { get; private set; }
        public IHandleAuthDB AuthDB { get; private set; }
        public IHandleUserDB UserDB { get; private set; }
        public DatabaseHandler(AuthContext authContext, 
            IHandleAuthDB authDB, IHandleUserDB userDB)
        {
            AuthContext = authContext;
            AuthDB = authDB;
            UserDB = userDB;
        }

        public void SaveChanges()
        {
            AuthContext.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await AuthContext.SaveChangesAsync();
        }
    }
}
