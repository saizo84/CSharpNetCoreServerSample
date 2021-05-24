using System.Collections.Generic;
using System.Reflection;

namespace SampleServer.PacketHandler
{
    public static class PacketHandlerAssemblyExtensions
    {
        public static ICollection<Assembly> AddPackets(this ICollection<Assembly> assemblies)
        {
            var assembly = typeof(PacketHandlerAssemblyExtensions).Assembly;
            assemblies.Add(assembly);
            return assemblies;
        }
    }
}
