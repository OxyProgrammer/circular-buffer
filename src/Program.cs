

using CircularBufferDemo;
using CircularBufferDemo.Data;
using CircularBufferDemo.Models;
using CircularBufferDemo.Presentation;
using CircularBufferDemo.Tests;
using System.Diagnostics.Metrics;

/**
  _____   _                   _            ____        __  __         
 / ____| (_)                 | |          |  _ \      / _|/ _|        
| |       _  _ __  ___  _   _| | __ _ _ __| |_) |_   _| |_| |_ ___ _ __
| |      | || '__|/ __|| | | | |/ _` | '__| _ <| | | |  _|  _/ _ \ '__|
| |____  | || |  | (__ | |_| | | (_| | |  | |_) | |_| | | | ||  __/ |   
 \_____| |_||_|   \___| \__,_|_|\__,_|_|  |____/ \__,_|_| |_| \___|_|   

 */

//Show Logo and Intro.
ConsoleHelper.ShowLogoAndIntroText();

int testBufferSize = 5;

Console.WriteLine($"Running Tests for Buffer Size: {testBufferSize} and Items: {AppConstants.TestSampleSize}");
Console.WriteLine("============================================================================================");
Console.WriteLine();

BufferedQueueTests.TestOverwriteItemApproach(testBufferSize);
BufferedQueueTests.TestRewritePropertiesApproach(testBufferSize);

int depth = 1;
int counter = 0;
var provider = new MarketDataProvider();

//Lets say we want market data for Ric IBM.N
var marketData = new SymbolMarketData(depth)
{
    Symbol = "IBM.N"
};

Console.WriteLine("------------------------------------------");
Console.WriteLine($"Symbol:{marketData.Symbol}");
Console.WriteLine("------------------------------------------");
Console.WriteLine("Price\tSize\tOrders");
Console.WriteLine("------------------------------------------");
Console.WriteLine();

var subscription = provider.GetPriceLevels().Subscribe(
    priceLevelDto =>
    {
        if (counter < depth)
        {
            marketData.BidLevels.Add(new PriceLevel
            {
                Price = priceLevelDto.Price,
                Size = priceLevelDto.Size,
                NumberOfOrders = priceLevelDto.NumberOfOrders
            });
        }
        else
        {
            var item = marketData.BidLevels[counter % depth];
            item.Price = marketData.BidLevels[counter % depth].Price;
            item.Size = marketData.BidLevels[counter % depth].Size;
            item.NumberOfOrders = marketData.BidLevels[counter % depth].NumberOfOrders;
        }
        counter++;

        Console.ForegroundColor = AppConstants.MarketDataTicketColor;
        (_, var top) = Console.GetCursorPosition();
        Console.SetCursorPosition(0, top);
        Console.Write($"{priceLevelDto.Price:F2}\t{priceLevelDto.Size}\t{priceLevelDto.NumberOfOrders}\t");
    },
ex => Console.WriteLine($"Error: {ex.Message}"),
() => Console.WriteLine("Completed"));

Console.ReadLine();

Console.ForegroundColor = AppConstants.Regular;

subscription.Dispose();

