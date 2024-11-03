using CircularBufferDemo.Data;
using CircularBufferDemo.DataStructures;
using CircularBufferDemo.Models;
using System.Diagnostics;
using System.Globalization;

namespace CircularBufferDemo.Tests
{
    public static class BufferedQueueTests
    {
        static PriceLevelDto[] _masterList = new PriceLevelDto[AppConstants.TestSampleSize];
        private static Random _random = new Random();

        static BufferedQueueTests()
        {
            for (int i = 0; i < _masterList.Length; i++)
            {
                _masterList[i] = new PriceLevelDto
                {
                    Price = (decimal)(_random.NextDouble() * 100.0),
                    Size = _random.Next(1, 1000),
                    NumberOfOrders = _random.Next(1, 20)
                };
            }
        }


        public static void TestOverwriteItemApproach(int bufferSize)
        {
            var buffer = new CircularBuffer<PriceLevel>(bufferSize);
            var stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < _masterList.Length; i++)
            {
                buffer.Add(new PriceLevel
                {
                    Price = _masterList[i].Price,
                    Size = _masterList[i].Size,
                    NumberOfOrders = _masterList[i].NumberOfOrders
                });
            }

            stopwatch.Stop();

            Console.WriteLine("================================================");
            Console.WriteLine($"Overwrite Item Approach for Buffer Size: {bufferSize} and {_masterList.Length.ToString("N0", CultureInfo.GetCultureInfo("en-US"))} items");
            Console.WriteLine($"Time taken: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Memory usage: {GC.GetTotalMemory(false).ToString("N0", CultureInfo.GetCultureInfo("en-US"))} bytes");
            Console.WriteLine("================================================");
            Console.WriteLine("");
        }

        public static void TestRewritePropertiesApproach(int bufferSize)
        {
            var buffer = new CircularBuffer<PriceLevel>(bufferSize);

            var stopwatch = Stopwatch.StartNew();


            for (int i = 0; i < bufferSize; i++)
            {
                buffer.Add(new PriceLevel
                {
                    Price = _masterList[i].Price,
                    Size = _masterList[i].Size,
                    NumberOfOrders = _masterList[i].NumberOfOrders
                });
            }

            for (int i = bufferSize; i < _masterList.Length; i++)
            {
                var item = buffer[i % bufferSize];
                item.Price = _masterList[i].Price;
                item.Size = _masterList[i].Size;
                item.NumberOfOrders = _masterList[i].NumberOfOrders;
            }

            stopwatch.Stop();

            Console.WriteLine("================================================");
            Console.WriteLine($"Rewrite Properties Approach for Buffer Size: {bufferSize} and {_masterList.Length.ToString("N0", CultureInfo.GetCultureInfo("en-US"))} items");
            Console.WriteLine($"Time taken: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Memory usage: {GC.GetTotalMemory(false).ToString("N0", CultureInfo.GetCultureInfo("en-US"))} bytes");
            Console.WriteLine("================================================");
            Console.WriteLine("");
        }
    }
}