#if NETSTANDARD2_0 || NETSTANDARD2_1 || NETCOREAPP3_1

namespace System.Diagnostics;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Struct, Inherited = false)]
internal sealed class StackTraceHiddenAttribute : Attribute
{
    public StackTraceHiddenAttribute() { }
}

#endif