namespace IT.Tests;

#if NETCOREAPP3_1_OR_GREATER
[MemoryPack.MemoryPackable]
#endif
public partial record MyRecord
{
    public Id Id { get; set; }

    public string? Name { get; set; }

    public string ToJsonArray() => $"[\"{Id:/}\",\"{Name}\"]";
}