namespace Blog.Application.Mapping
{
    using AutoMapper;

    using System;
    using System.Linq;
    using System.Reflection;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                if (type == null)
                {
                    throw new Exception("type is null while trying to map");
                }

                var instance = Activator.CreateInstance(type);
                if (instance == null)
                {
                    throw new Exception("instance is null while trying to map");
                }

                if (instance.GetType().GetMethod("Mapping", BindingFlags.Instance | BindingFlags.NonPublic) != null)
                {
                    throw new Exception($"mapping method is private on {instance.GetType().FullName}");
                }

                MethodInfo? methodInfo = type.GetMethod("Mapping") ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");
                var res = methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}