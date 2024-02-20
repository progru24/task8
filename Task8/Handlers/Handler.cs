using Task8.Contracts;
using Microsoft.Extensions.Logging;

namespace Task8.Handlers
{
    public class Handler : IHandler
    {
        private readonly IConsumer _consumer;
        private readonly IPublisher _publisher;
        private readonly ILogger<Handler> _logger;

        public TimeSpan Timeout { get; }

        public Handler(
          TimeSpan timeout,
          IConsumer consumer,
          IPublisher publisher,
          ILogger<Handler> logger)
        {
            Timeout = timeout;

            _consumer = consumer;
            _publisher = publisher;
            _logger = logger;
        }

        public async Task PerformOperation(CancellationToken cancellationToken)
        {
            while(true)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                var data = await _consumer.ReadData();

                foreach(var item in data.Recipients)
                {
                    _publisher.SendData(item, data.Payload).ContinueWith(t =>
                    {
                        if (t.Result == Models.SendResult.Rejected)
                        {
                            Task.Delay(Timeout).ContinueWith(t => { _publisher.SendData(item, data.Payload); });
                        }
                    });
                }
            }            
        }
    }
}
