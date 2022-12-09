using IT;
using MessagePack.Formatters;
using System;

namespace MessagePack.Resolvers;

public class IdResolver : IFormatterResolver
{
    static class Cache<T>
    {
        public static readonly IMessagePackFormatter<T>? formatter;

        static Cache()
        {
            formatter = (IMessagePackFormatter<T>?)GetFormatter(typeof(T));
        }

        private static object? GetFormatter(Type t)
        {
            if (t == typeof(Id)) return IdFormatter.Instance;

            if (t == typeof(Id?)) return new StaticNullableFormatter<Id>(IdFormatter.Instance);

            return null;
        }
    }

    public readonly static IFormatterResolver Instance = new IdResolver();

    public IMessagePackFormatter<T>? GetFormatter<T>() => Cache<T>.formatter;
}