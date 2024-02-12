using System.Globalization;

namespace ColorConverter;

internal class Program
{
    public static string Separator { get; set; } = ",";

    [STAThread]
    static void Main(string[] args)
    {
        Console.WriteLine("Do you want to edit the configuration? (y/n)");
        ConsoleKeyInfo key = Console.ReadKey();
        if (key.Key == ConsoleKey.Y)
            Configure();

        Console.Clear();

        while (true)
        {
            Console.WriteLine("Enter your color in HEX");
            string? input = Console.ReadLine();

            string? result = HexToColor(input);
            if (result is not null)
            {
                Console.WriteLine(result);
                Clipboard.SetText(result);
            }
            else
                Console.WriteLine("Invalid input");

            Console.WriteLine("""

                Press ESC to exit
                Press C to configure
                Press any other key to continue
                """);
            key = Console.ReadKey();

            if (key.Key == ConsoleKey.Escape)
                break;
            else if (key.Key == ConsoleKey.C)
                Configure();

            Console.Clear();
        }
    }

    public static void Configure()
    {
        Console.WriteLine($"{Environment.NewLine}Enter separator");
        string? separator = Console.ReadLine();

        if (separator is not null)
            Separator = separator;
    }

    public static string? HexToColor(string? hexString)
    {
        if (!string.IsNullOrWhiteSpace(hexString))
        {
            ReadOnlySpan<char> input = hexString.AsSpan().Trim();

            if (input.Length is not 6 or 7)
                return null;

            if (input[0] == '#')
                input = input[1..];

            int r, g, b;
            r = int.Parse(input[0..2], NumberStyles.AllowHexSpecifier);
            g = int.Parse(input[2..4], NumberStyles.AllowHexSpecifier);
            b = int.Parse(input[4..6], NumberStyles.AllowHexSpecifier);

            return $"{r}{Separator}{g}{Separator}{b}";
        }

        return null;
    }
}