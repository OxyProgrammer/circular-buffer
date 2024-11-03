using CircularBufferDemo.Data;
using CircularBufferDemo.Models;
using System.Diagnostics;

namespace CircularBufferDemo.Presentation
{
    internal static class ConsoleHelper
    {
        internal static void ShowLogoAndIntroText()
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
    }
}
