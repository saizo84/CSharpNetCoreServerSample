using System.Collections.Generic;
using System.Reflection;

namespace SampleServer.Auth
{
    public static class AuthAssemblyExtensions
    {
        public static ICollection<Assembly> AddAuth(this ICollection<Assembly> assemblies)
        {
            var assembly = typeof(AuthAssemblyExtensions).Assembly;
            assemblies.Add(assembly);
            return assemblies;
        }
    }
}
