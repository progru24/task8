using Task8.Models;

namespace Task8.Contracts
{
    public interface IPublisher
    {
        Task<SendResult> SendData(Address address, Payload payload);
    }
}
