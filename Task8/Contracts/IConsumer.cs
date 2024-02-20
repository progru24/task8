using Task8.Models;

namespace Task8.Contracts
{
    public interface IConsumer
    {
        Task<Event> ReadData();
    }
}
