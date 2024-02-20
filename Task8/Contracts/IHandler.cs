namespace Task8.Contracts
{
    public interface IHandler
    {
        TimeSpan Timeout { get; }

        Task PerformOperation(CancellationToken cancellationToken);
    }
}
