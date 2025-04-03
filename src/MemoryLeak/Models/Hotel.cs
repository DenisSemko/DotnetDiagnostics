namespace MemoryLeak.Models;

public record Hotel
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public int Capacity { get; init; }
}