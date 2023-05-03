using System;

namespace MZ_WorkerService.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsSubclassOfRawGeneric(this Type type, Type generic)
        {
            try
            {
                while (type != null && type != typeof(object))
                {
                    var cur = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
                    if (generic == cur)
                    {
                        return true;
                    }
                    type = type.BaseType!;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }
    }
}
