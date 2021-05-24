using DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHandle
{
    public interface IHandleUserDB
    {
        void AddUser(User user);
    }

    public class HandleUserDB : IHandleUserDB
    {
        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
