namespace Library.StaticClasses;

/// <summary>
/// This class is responsible for showing the user what's happening on console
/// </summary>
public static class Printer
{
    /// <summary>
    /// Shows the message that starts the game
    /// </summary>
    public static void StartPrint()
    {
        Console.Clear();
        Console.WriteLine("╔═══════════════════════════════════════╗");
        Console.WriteLine("║       Welcome to Pokemon Battle       ║");
        Console.WriteLine("╚═══════════════════════════════════════╝");

        Console.WriteLine();

        Console.WriteLine("╔═══════════════════════════════╗");
        Console.WriteLine("║\tPick an option:  \t║");
        Console.WriteLine("║\t1) Start         \t║");
        Console.WriteLine("║\t2) Leave         \t║");
        Console.WriteLine("╚═══════════════════════════════╝");
        Console.WriteLine();
    }

    /// <summary>
    /// Prints the end of the game when you select to leave at the beginning 
    /// </summary>
    public static void EndPrint()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════╗");
        Console.WriteLine("║    Thanks for playing!!    ║");
        Console.WriteLine("╚════════════════════════════╝");
    }

    /// <summary>
    /// Sends a sign if the index is out of range.
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    public static void IndexOutOfRange(int min, int max)
    {
        Console.WriteLine($"╔═════════════════════════════════════╗");
        Console.WriteLine($"║    El valor debe ser mayor que {min}    ║");
        Console.WriteLine($"║            y menor que {max}            ║");
        Console.WriteLine($"╚═════════════════════════════════════╝");
    }

    /// <summary>
    /// This method has to show an "enter your name" badge/
    /// </summary>
    public static void NameSelection()
    {
        Console.WriteLine("╔═══════════════════════════════╗");
        Console.WriteLine("║        Enter your name        ║");
        Console.Write("╚═══════════════════════════════╝\n> ");
    }

    /// <summary>
    /// Shows a box specifying who has to play
    /// </summary>
    /// <param name="name"></param>
    public static void YourTurn(string name)
    {
        Console.Clear();
        Console.WriteLine( "╔═══════════════════════════════════╗");
        Console.WriteLine($"║        Your turn Player {name}         ║");
        Console.Write(     "╚═══════════════════════════════════╝ \n");
    }

    /// <summary>
    /// This method has to show the player all the Pokémon available for selection in a fashion manner.
    /// </summary>
    public static void ShowCatalogue(Dictionary<int, IPokemon> pokedex)
    {
        int count = 0;
        List<string[]> boxes = new List<string[]>();

        foreach (var entry in pokedex)
        {
            // Get the formatted box lines for each Pokémon and add to list
            boxes.Add(FormatPokemonBox(entry.Key, entry.Value.Name, entry.Value.Health));
            count++;

            // After every 5 Pokémon, print a new row
            if (count % 5 == 0)
            {
                PrintRow(boxes);
                boxes.Clear(); // Clear boxes for the next row
            }
        }

        // Print any remaining boxes if the last row was not complete
        if (boxes.Count > 0)
        {
            PrintRow(boxes);
        }
    }

    /// <summary>
    /// Formats a Pokémon entry as an array of strings representing each line of the box.
    /// </summary>
    /// <param name="index">The index of the Pokémon.</param>
    /// <param name="name">The name of the Pokémon.</param>
    /// <returns>An array of strings, each representing a line in the box format.</returns>
    private static string[] FormatPokemonBox(int index, string name, int life)
    {
        string boxTop = "╔════════════════════════════╗";
        string boxBottom = "╚════════════════════════════╝";
        string indexLine = $"║  Number: {index,-18}║"; // Aligns the index within the box
        string nameLine = $"║  Name: {name,-20}║"; // Aligns the name within the box
        string lifeLine = $"║  Life: {life,-20}║"; // Aligns the name within the box

        return new string[] { boxTop, indexLine, nameLine, lifeLine ,boxBottom };
    }

    /// <summary>
    /// Prints a row of Pokémon boxes side-by-side without adding extra spaces between boxes.
    /// </summary>
    /// <param name="boxes">List of box lines for the row.</param>
    private static void PrintRow(List<string[]> boxes)
    {
        // Print each line of the boxes in sequence for all boxes in the row
        for (int i = 0; i < boxes[0].Length; i++)
        {
            foreach (var box in boxes)
            {
                Console.Write(box[i] + "  "); // Print each line of the box directly, without added spaces
            }

            Console.WriteLine(); // Move to the next line for the row
        }

        Console.WriteLine(); // Extra space between rows
    }


    /// <summary>
    /// Prints a box so that the user can visualize what he's doing.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="name"></param>
    public static void AskForPokemon(int index, string name)
    {
        Console.Write($"{name}! Pick your Pokemon N°{index + 1}: ");
        Console.WriteLine("");
    }

    /// <summary>
    /// Shows the user inventory in a boxed format.
    /// </summary>
    /// <param name="inventory">List of IPokemon items, between 1 and 6 items.</param>
    public static void ShowInventory(List<IPokemon> inventory)
    {
        Console.Clear();
        PrintInventoryHeader(); // Print the "Your Inventory" header
        
        int count = 0;
        List<string[]> boxes = new List<string[]>();

        foreach (IPokemon pokemon in inventory)
        {
            // Create and add the formatted box for each Pokémon to the list
            boxes.Add(FormatPokemonBox(count + 1, pokemon.Name, pokemon.Health));
            count++;

            // Print a row after every 3 Pokémon or when reaching the end of the list
            if (count % 3 == 0 || count == inventory.Count)
            {
                PrintRow(boxes);
                boxes.Clear(); // Clear for the next row
            }
        }
    }
    
    
    /// <summary>
    /// Prints the header box for the inventory.
    /// </summary>
    private static void PrintInventoryHeader()
    {
        string top = "╔════════════════════════════════════════════════════════════════════════════════════════════╗";
        string title = "║                                      Your Inventory                                        ║";
        string bottom = "╚════════════════════════════════════════════════════════════════════════════════════════════╝";
    
        Console.WriteLine(top);
        Console.WriteLine(title);
        Console.WriteLine(bottom);
    }

    public static void ShowSelectedPokemon(IPokemon pokemon, string name)
    {
        Console.WriteLine("╔═══════════════════════════════════╗");
        Console.WriteLine($"║  This is your pokemon {name}!\t║");
        Console.WriteLine("╚══════════════════════════════════╝");
        Console.WriteLine("╔═══════════════════════════════════╗");
        Console.WriteLine($"║    Name: {pokemon.Name}\t\t║");
        Console.WriteLine($"║    Life: {pokemon.Health}/100\t\t    ║");
        Console.WriteLine("╚══════════════════════════════════╝");
    }
}