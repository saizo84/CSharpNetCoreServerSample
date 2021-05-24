using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBHandle
{
    public static class DBHandleAssemblyExtensions
    {
        public static ICollection<Assembly> AddDatabase(this ICollection<Assembly> assemblies)
        {
            var assembly = typeof(DBHandleAssemblyExtensions).Assembly;
            assemblies.Add(assembly);
            return assemblies;
        }
    }
}
