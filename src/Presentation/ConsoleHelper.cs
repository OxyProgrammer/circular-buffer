using CircularBufferDemo.Data;
using CircularBufferDemo.Models;
using System.Reflection.Emit;

namespace CircularBufferDemo.Presentation
{
    internal static class ConsoleHelper
    {
        internal static void ShowLogoAndHeaderText()
        {
            Console.ForegroundColor = AppConstants.LogoColor;

            string[] logo =
            [
                "  _____   _                   _            ____        __  __         ",
                " / ____| (_)                 | |          |  _ \\      / _|/ _|        ",
                "| |       _  _ __  ___  _   _| | __ _ _ __| |_) |_   _| |_| |_ ___ _ __",
                "| |      | || '__|/ __|| | | | |/ _` | '__| _ <| | | |  _|  _/ _ \\ '__|",
                "| |____  | || |  | (__ | |_| | | (_| | |  | |_) | |_| | | | ||  __/ |   ",
                " \\_____| |_||_|   \\___| \\__,_|_|\\__,_|_|  |____/ \\__,_|_| |_| \\___|_|   "
            ];

            int width = logo[0].Length + 4;

            string horizontalBorder = new string('=', width);

            Console.WriteLine(horizontalBorder);
            Console.WriteLine("|" + new string(' ', width - 2) + "|");

            foreach (string line in logo)
            {
                Console.WriteLine($"|  {line}  |");
            }

            Console.WriteLine("|" + new string(' ', width - 2) + "|");
            Console.WriteLine(horizontalBorder);
            Console.WriteLine();
            Console.ForegroundColor = AppConstants.InstructionsColor;

            string[] messages =
            [
               "Press Any Key to Exit the application."
            ];


            int maxLength = messages.Max(s => s.Length);
            width = maxLength + 4;  // 4 for padding (2 on each side)

            horizontalBorder = "+" + new string('-', width - 2) + "+";

            Console.WriteLine(horizontalBorder);

            foreach (string message in messages)
            {
                int padding = width - message.Length - 3;  // -3 for "| " and " |"
                Console.WriteLine($"| {message}{new string(' ', padding)}|");
            }

            Console.WriteLine(horizontalBorder);

            Console.ResetColor();
        }

        internal static void ShowMarketDataTicking()
        {
            var provider = new MarketDataProvider();

            //The depth is taken 1 to facilitate logging in console.
            //However, the buffer will work just fine for any depth.
            int depth = 1;

            var marketData = new SymbolMarketData(depth)
            {
                Symbol = "IBM.N"
            };

            int counter = 0;

            PrintPriceLevelsHeader();
            Console.ForegroundColor = AppConstants.MarketDataTicketColor;
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
                    (_, var top) = Console.GetCursorPosition();
                    Console.SetCursorPosition(0, top);
                    Console.Write($"${priceLevelDto.Price:F2}\t{priceLevelDto.Size}\t{priceLevelDto.NumberOfOrders}\t");
                },
            ex => Console.WriteLine($"Error: {ex.Message}"),
            () => Console.WriteLine("Completed")
            );

            Console.ReadLine();
            subscription.Dispose();
        }

        private static void PrintPriceLevelsHeader()
        {
            Console.WriteLine("Price\tSize\tOrders");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();
        }
    }
}
