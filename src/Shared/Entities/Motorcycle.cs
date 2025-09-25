namespace Shared.Entities;

#pragma warning disable CS8618
public sealed class Motorcycle
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public string Model { get; set; }
    public string Plate { get; set; }
}
#pragma warning restore CS8618