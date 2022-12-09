using MemoryPack;

namespace IT.Tests;

[MemoryPackable]
public partial record MyRecord
{
    public Id Id { get; set; }

    public string? Name { get; set; }

    public string ToJsonArray() => $"[\"{Id:/}\",\"{Name}\"]";
}