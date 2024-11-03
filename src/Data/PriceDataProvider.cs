using System.Reactive.Linq;

namespace CircularBufferDemo.Data
{
    public class MarketDataProvider
    {
        private readonly Random _random = new Random();

        public IObservable<PriceLevelDto> GetPriceLevels(long? n = null)
        {
            return Observable.Create<PriceLevelDto>(observer =>
            {
                var count = 0L;
                var timer = new Timer(_ =>
                {
                    var priceLevel = GenerateRandomPriceLevel();

                    observer.OnNext(priceLevel);

                    if (n.HasValue && ++count >= n.Value)
                    {
                        observer.OnCompleted();
                    }
                }, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(10));

                return () => timer.Dispose();
            });
        }

        private PriceLevelDto GenerateRandomPriceLevel()
        {
            return new PriceLevelDto
            {
                Price = (decimal)(_random.NextDouble() * 10.0),
                Size = _random.Next(1, 100),
                NumberOfOrders = _random.Next(1, 20)
            };
        }
    }
}
