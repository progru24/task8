namespace Task8.Models
{
    public record Event(IReadOnlyCollection<Address> Recipients, Payload Payload);
}
