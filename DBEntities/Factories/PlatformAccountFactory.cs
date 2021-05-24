using Sample.Proto;

namespace DBEntities.Factories
{
    public static class PlatformAccountFactory
    {
        public static PlatformAccount Create(PlatformTypes platformType, string platformId) =>
            new()
            {
                PlatformId = platformId,
                PlatformType = platformType,
            };
    }
}
