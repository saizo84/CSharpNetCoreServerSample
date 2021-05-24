using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Proto;
using DBHandle;
using AutoMapper;
using DBEntities.Factories;

namespace SampleServer.Auth
{
    public interface ILoginHandler
    {
        ValueTask<(ResultCodes ResultCode, UserInfo UserInfo)> Login(
            string platformId, DeviceInfo deviceInfo,
            string platformPassword);
    }

    public class GuestLoginHandler : ILoginHandler
    {
        private readonly IDatabaseHandler _databaseHandler;
        private readonly IMapper _mapper;
        public GuestLoginHandler(IDatabaseHandler databaseHandler, IMapper mapper)
        {
            _databaseHandler = databaseHandler;
            _mapper = mapper;
        }

        public async ValueTask<(ResultCodes ResultCode, UserInfo UserInfo)> 
            Login(string platformId, DeviceInfo deviceInfo, string platformPassword)
        {
            var userInfo = new UserInfo();
            userInfo.Level = 1;
            userInfo.Exp = 0;
            userInfo.Nickname = "test";

            if (platformId is null || deviceInfo is null || platformPassword is null)
            {
                return (ResultCodes.Abnormal, null);
            }

            var findPlatformAccount = await _databaseHandler.AuthDB.GetPlatformAccount(platformId);

            DBEntities.User user = null;
            if (findPlatformAccount is null)
            {
                user = new DBEntities.User();
                user.Devices.Add(_mapper.Map<DBEntities.Device>(deviceInfo));
                user.Nickname = platformId;
                var platformAccount = PlatformAccountFactory.Create(PlatformTypes.Guest, platformId);
                user.PlatformAccounts.Add(platformAccount);
                _databaseHandler.UserDB.AddUser(user);

                //platformAccount.PlatformAccountAuthorization =
                //    PlatformAccountAuthorizationFactory.Create(platformAccount.Id, platformPassword);
            }
            else
            {
                var userId = findPlatformAccount.UserId.Value;
                //if (findPlatformAccount.PlatformAccountAuthorization is null)
                //{
                //    findPlatformAccount.PlatformAccountAuthorization =
                //        PlatformAccountAuthorizationFactory.Create(findPlatformAccount.Id, platformPassword);
                //}
                //else if (0 == findPlatformAccount.PlatformAccountAuthorization.Password.Length
                //    && 0 < platformPassword.Length)
                //{
                //    findPlatformAccount.PlatformAccountAuthorization.Password = platformPassword;
                //}
                //else if (findPlatformAccount.PlatformAccountAuthorization.Password != platformPassword)
                //{
                //    return (DO.ResultCodes.PasswordIncorrect, null);
                //}
                //user = await DatabaseLoader.LoadUser(databaseHandler, userId).ConfigureAwait(false);
                user.Nickname = platformId;
            }

            _databaseHandler.SaveChanges();

            return (ResultCodes.Success, userInfo);
        }
    }
}
