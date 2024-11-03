using CircularBufferDemo.DataStructures;

namespace CircularBufferDemo.Models
{
    public class SymbolMarketData
    {
        public string Symbol { get; set; }
        public CircularBuffer<PriceLevel> BidLevels { get; set; }

        public SymbolMarketData(int depth)
        {
            BidLevels = new CircularBuffer<PriceLevel>(depth);
        }
    }

    public class PriceLevel
    {
        public decimal Price { get; set; }
        public int Size { get; set; }
        public int NumberOfOrders { get; set; }
    }
}
