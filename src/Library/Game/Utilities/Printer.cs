using Library.Game.Attacks;
using Library.Game.Items;
using Library.Game.Players;
using Library.Game.Pokemons;

namespace Library.Game.Utilities;

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
    /// Recieves the winner's name and displays a box indicating the winner.
    /// </summary>
    /// <param name="winner">Name of the winner.</param>
    public static void DisplayWinner(string winner)
    {
        Console.Clear();

        // Calculate the box width based on the length of the winner message
        int winnerMessageLength = $"The winner is {winner}!!".Length;
        int boxWidth = winnerMessageLength + 4; // Add space for borders and padding

        // Construction of the top and bottom borders for the box
        string topBorder = $"╔{new string('═', boxWidth - 2)}╗";
        string bottomBorder = $"╚{new string('═', boxWidth - 2)}╝";

        // Create the message with the winner's name, ensuring it's centered
        string winnerMessage = $"The winner is {winner}!!";
        string centeredMessage = $"║ {winnerMessage.PadRight(boxWidth - 4)} ║"; // Adjust padding based on box width

        // Print the box
        Console.WriteLine(topBorder);
        Console.WriteLine(centeredMessage);
        Console.WriteLine(bottomBorder);
    }


    /// <summary>
    /// Sends a sign if the index is out of range.
    /// </summary>
    /// <param name="min">Minimum acceptable value.</param>
    /// <param name="max">Maximum acceptable value.</param>
    public static void IndexOutOfRange(int min, int max)
    {
        Console.Clear();

        // Messages to display
        string firstMessage = $"El valor debe ser mayor que {min}";
        string secondMessage = $"y menor que {max}";

        // Calculate the box width based on the longest message
        int boxWidth = Math.Max(firstMessage.Length, secondMessage.Length) + 4; // Add space for borders and padding

        // Construct the top and bottom borders for the box
        string topBorder = $"╔{new string('═', boxWidth - 2)}╗";
        string bottomBorder = $"╚{new string('═', boxWidth - 2)}╝";

        // Format each message to be centered within the box
        string centeredFirstMessage = $"║ {firstMessage.PadRight(boxWidth - 4)} ║";
        string centeredSecondMessage = $"║ {secondMessage.PadRight(boxWidth - 4)} ║";

        // Print the box
        Console.WriteLine(topBorder);
        Console.WriteLine(centeredFirstMessage);
        Console.WriteLine(centeredSecondMessage);
        Console.WriteLine(bottomBorder);
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
    /// <param name="name">Name of the player whose turn it is.</param>
   public static void YourTurn(string name)
{
    ArgumentNullException.ThrowIfNull(name);

    Console.Clear();

    string message = $"Your turn Player {name}";

    // Calcular el ancho del cuadro basado en la longitud del mensaje
    int boxWidth = message.Length + 4;

    string topBorder = $"╔{new string('═', boxWidth - 2)}╗";
    string bottomBorder = $"╚{new string('═', boxWidth - 2)}╝";

    Console.WriteLine(topBorder);
    Console.WriteLine($"║ {message.PadRight(boxWidth - 4)} ║");
    Console.WriteLine(bottomBorder);
}



    /// <summary>
    /// This method has to show the player all the Pokémon available for selection in a fashion manner.
    /// </summary>
    public static void ShowCatalogue(Dictionary<int, IPokemon> pokedex)
    {
        ArgumentNullException.ThrowIfNull(pokedex);
        
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

        // Ensure that each box is properly closed (this will print the bottom border)
        CloseBoxes(boxes);
    }

    /// <summary>
    /// Closes each Pokémon box with the bottom border.
    /// </summary>
    /// <param name="boxes">The list of Pokémon box strings to be closed.</param>
    private static void CloseBoxes(List<string[]> boxes)
    {
        foreach (var box in boxes)
        {
            // Print the bottom border for each box
            Console.WriteLine(box[box.Length - 1]); // The last line in each box is the bottom border
        }
    }


    /// <summary>
    /// Formats a Pokémon entry as an array of strings representing each line of the box.
    /// </summary>
    /// <param name="index">The index of the Pokémon.</param>
    /// <param name="name">The name of the Pokémon.</param>
    /// <param name="life">The life points of the Pokémon.</param>
    /// <returns>An array of strings, each representing a line in the box format.</returns>
    private static string[] FormatPokemonBox(int index, string name, int life)
    {
        // Messages to display
        string indexMessage = $"Number: {index}";
        string nameMessage = $"Name: {name}";
        string lifeMessage = $"Life: {life}";

        // Determine the maximum message length to set box width
        int maxMessageLength = Math.Max(indexMessage.Length, Math.Max(nameMessage.Length, lifeMessage.Length));
        int boxWidth = maxMessageLength + 4; // Add space for borders and padding

        // Construct the top and bottom borders of the box
        string boxTop = $"╔{new string('═', boxWidth - 2)}╗";
        string boxBottom = $"╚{new string('═', boxWidth - 2)}╝";

        // Format each line, centering the messages
        string indexLine = $"║ {indexMessage.PadRight(boxWidth - 4)} ║";
        string nameLine = $"║ {nameMessage.PadRight(boxWidth - 4)} ║";
        string lifeLine = $"║ {lifeMessage.PadRight(boxWidth - 4)} ║";

        // Return the formatted box as an array of strings
        return new string[] { boxTop, indexLine, nameLine, lifeLine, boxBottom };
    }

    /// <summary>
    /// Prints a row of Pokémon boxes side-by-side without adding extra spaces between boxes.
    /// </summary>
    /// <param name="boxes">List of box lines for the row.</param>
    private static void PrintRow(List<string[]> boxes)
{
    // Imprime cada línea de las cajas en secuencia para todas las cajas en la fila
    for (int i = 0; i < boxes[0].Length; i++)
    {
        foreach (var box in boxes)
        {
            // Imprime cada línea de la caja directamente, sin espacios adicionales
            Console.Write(box[i]);
        }

        // Mueve a la siguiente línea después de imprimir una fila de cajas
        Console.WriteLine();
    }

    // Imprime una línea adicional solo si es necesario (esto se podría eliminar si no es deseado)
    Console.WriteLine();
}



    /// <summary>
    /// Prints a box so that the user can visualize what he's doing.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="name"></param>
    public static void AskForPokemon(int index, string name)
    {
        Console.Write($"{name}! Pick your Pokemon N°{index }: ");
        Console.WriteLine("");
    }

    /// <summary>
    /// Shows the user inventory in a boxed format.
    /// </summary>
    /// <param name="inventory">List of IPokemon items, between 1 and 6 items.</param>
    public static void ShowInventory(List<IPokemon> inventory)
    {
        ArgumentNullException.ThrowIfNull(inventory);
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
        string title = "║                                      Your Team                                             ║";
        string bottom = "╚════════════════════════════════════════════════════════════════════════════════════════════╝";
    
        Console.WriteLine(top);
        Console.WriteLine(title);
        Console.WriteLine(bottom);
    }

    /// <summary>
    /// Shows the player it's Pokémon.
    /// </summary>
    /// <param name="pokemon"></param>
    /// <param name="name"></param>
    public static void ShowSelectedPokemon(IPokemon pokemon, string name)
    {
        ArgumentNullException.ThrowIfNull(pokemon);

        // Define lines with Pokémon details
        string line1 = $"This is your pokemon {name}!";
        string line2 = $"Name: {pokemon.Name}";
        string line3 = $"Life: {pokemon.Health}/100";
        string line4 = $"Status: {pokemon.State}"; // New line for Pokémon's status

        // Find the longest line for box width calculation
        int boxWidth = Math.Max(Math.Max(Math.Max(line1.Length, line2.Length), line3.Length), line4.Length) + 4; // Adding 4 for padding/borders

        // Create the top and bottom borders
        string topBorder = $"╔{new string('═', boxWidth - 2)}╗";
        string bottomBorder = $"╚{new string('═', boxWidth - 2)}╝";

        // Print the box
        Console.WriteLine(topBorder);
        Console.WriteLine($"║ {line1.PadRight(boxWidth - 4)} ║");
        Console.WriteLine($"║ {line2.PadRight(boxWidth - 4)} ║");
        Console.WriteLine($"║ {line3.PadRight(boxWidth - 4)} ║");
        Console.WriteLine($"║ {line4.PadRight(boxWidth - 4)} ║"); // Include the new line
        Console.WriteLine(bottomBorder);
    }



/// <summary>
/// Show the attacks of each Pokémon, displaying their name, damage, type, effectiveness, and special effect.
/// </summary>
/// <param name="attacker">The Pokémon whose attacks will be displayed.</param>
/// <param name="receiver">The Pokémon that will receive the attack.</param>
public static void ShowAttacks(IPokemon attacker, IPokemon receiver)
{
    ArgumentNullException.ThrowIfNull(attacker);

    // Header message for the attacker's attacks
    string headerMessage = $"Attacks of {attacker.Name}";
    int headerWidth = headerMessage.Length + 4;

    // Display the header box dynamically sized
    string headerTop = $"╔{new string('═', headerWidth - 2)}╗";
    string headerBottom = $"╚{new string('═', headerWidth - 2)}╝";
    Console.WriteLine(headerTop);
    Console.WriteLine($"║ {headerMessage.PadRight(headerWidth - 4)} ║");
    Console.WriteLine(headerBottom);

    int i = 1;

    // Iterate through each attack in the Pokémon's attack list
    foreach (IAttack attack in attacker.AtackList)
    {
        // Calculate effectiveness
        double effectiveness = Calculator.CheckEffectiveness(attack, receiver);

        // Format attack details
        string attackIndex = $"Attack {i}";
        string attackName = $"Name: {attack.Name}";
        string attackDamage = $"Damage: {attack.Damage}";
        string attackType = $"Type: {attack.Type}";
        string attackEffectiveness = $"Effectiveness: {effectiveness}";
        string specialEffect = $"Special Effect: {attack.Special}";

        // Determine the maximum width of the content
        int boxWidth = Math.Max(
            Math.Max(attackIndex.Length, attackName.Length),
            Math.Max(attackDamage.Length, Math.Max(
                Math.Max(attackType.Length, attackEffectiveness.Length), 
                specialEffect.Length))
        ) + 4;

        // Build the top and bottom borders for the box
        string boxTop = $"╔{new string('═', boxWidth - 2)}╗";
        string boxBottom = $"╚{new string('═', boxWidth - 2)}╝";

        // Display the attack details in a dynamically sized box
        Console.WriteLine(boxTop);
        Console.WriteLine($"║ {attackIndex.PadRight(boxWidth - 4)} ║");
        Console.WriteLine($"║ {attackName.PadRight(boxWidth - 4)} ║");
        Console.WriteLine($"║ {attackDamage.PadRight(boxWidth - 4)} ║");
        Console.WriteLine($"║ {attackType.PadRight(boxWidth - 4)} ║");
        Console.WriteLine($"║ {attackEffectiveness.PadRight(boxWidth - 4)} ║");
        Console.WriteLine($"║ {specialEffect.PadRight(boxWidth - 4)} ║");
        Console.WriteLine(boxBottom);

        i++;
    }

    Console.WriteLine(); // Extra line for spacing
}



    /// <summary>
    /// Displays whose turn it is and prompts the player to choose an action.
    /// </summary>
    /// /// <summary>
    /// Prints the current and initial health of the Pokémon.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="pokemon">The Pokémon whose health will be shown.</param>
    public static void ShowTurnInfo(IPlayer player, IPokemon pokemon)
    {
        ArgumentNullException.ThrowIfNull(player);
        ArgumentNullException.ThrowIfNull(pokemon);

        // Determina el texto más largo
        string line1 = $"{player.Name}'s turn!";
        string line2 = $"{pokemon.Name} Health: {pokemon.Health}/{pokemon.InitialHealth}";
        string line3 = "What would you like to do?";
        string line4 = "1. Attack";
        string line5 = "2. Use Item";
        string line6 = "3. Switch Pokémon";

        // Calcula el ancho del cuadro basado en el texto más largo
        int maxLength = Math.Max(Math.Max(line1.Length, line2.Length), Math.Max(line3.Length, Math.Max(line4.Length, Math.Max(line5.Length, line6.Length))));
        int boxWidth = maxLength + 4; // Añade espacio para los bordes y una separación adicional.

        // Construcción del cuadro
        string topBorder = $"╔{new string('═', boxWidth)}╗";
        string bottomBorder = $"╚{new string('═', boxWidth)}╝";

        // Imprime el cuadro con las líneas centradas
        Console.WriteLine(topBorder);

        // Asegurando que el nombre del jugador esté correctamente alineado
        Console.WriteLine($"║ {line1.PadRight(boxWidth - 2)} ║");  // Restamos 2 para los bordes
        Console.WriteLine($"║ {line2.PadRight(boxWidth - 2)} ║");
        Console.WriteLine($"║ {line3.PadRight(boxWidth - 2)} ║");
        Console.WriteLine($"║ {line4.PadRight(boxWidth - 2)} ║");
        Console.WriteLine($"║ {line5.PadRight(boxWidth - 2)} ║");
        Console.WriteLine($"║ {line6.PadRight(boxWidth - 2)} ║");

        Console.WriteLine(bottomBorder);
    }




    /// <summary>
    /// This method prints to the usser the effectiveness after each attack.
    /// Gets called in: Calculator.InfringeDamage()
    /// </summary>
    /// <param name="value"></param>
    /// <param name="attack"></param>
    public static void Effectiveness(int value, IAttack attack)
    {
        // possible values = 0.0, 0.5, 2.0
        if (value == 0)
        {
            Console.WriteLine($"Attack {attack} was ineffective! X0 Damage!");
        }        
        else if (value == 1)
        {
            Console.WriteLine($"Attack {attack} was used! x1 Damage!");
        }        
        else if (value == 2)
        {
            Console.WriteLine($"Attack {attack} was effective! X2 Damage!");
        }
        else if (value == 3)
        {
            Console.WriteLine($"Attack {attack} was slightly ineffective! X0.5 Damage!");
        }
    }
    
    /// <summary>
    /// Shows the player that their Pokémon has been defeated and that they need to change the current one.
    /// </summary>
    /// <param name="player">The player whose Pokémon has been defeated.</param>
    public static void ForceSwitchMessage(IPlayer player)
    {
        ArgumentNullException.ThrowIfNull(player);

        IPokemon pokemon = player.SelectedPokemon;

        // Messages to display
        string firstMessage = $"{player.Name}, your Pokémon {pokemon.Name} has been defeated!";
        string secondMessage = "Please pick another one from your list!";

        // Determine the maximum message length to set box width
        int boxWidth = Math.Max(firstMessage.Length, secondMessage.Length) + 4; // Add space for borders and padding

        // Construct the top and bottom borders for the box
        string boxTop = $"╔{new string('═', boxWidth - 2)}╗";
        string boxBottom = $"╚{new string('═', boxWidth - 2)}╝";

        // Format each line, ensuring they are padded to match the box width
        string formattedFirstMessage = $"║ {firstMessage.PadRight(boxWidth - 4)} ║";
        string formattedSecondMessage = $"║ {secondMessage.PadRight(boxWidth - 4)} ║";

        // Print the dynamically sized box
        Console.Clear();
        Console.WriteLine(boxTop);
        Console.WriteLine(formattedFirstMessage);
        Console.WriteLine(formattedSecondMessage);
        Console.WriteLine(boxBottom);
    }

    /// <summary>
    /// Asks the player for confirmation.
    /// </summary>
    /// <param name="player">The player to confirm the Pokémon switch.</param>
    public static void SwitchQuestion(IPlayer player)
    { 
        ArgumentNullException.ThrowIfNull(player);

        IPokemon pokemon = player.SelectedPokemon;

        // Messages to display
        string firstMessage = $"{player.Name}, do you want to change your Pokémon {pokemon.Name}?";
        string secondMessage = "1) Yes  2) No";

        // Determine the maximum message length to set box width
        int boxWidth = Math.Max(firstMessage.Length, secondMessage.Length) + 4; // Add space for borders and padding

        // Construct the top and bottom borders for the box
        string boxTop = $"╔{new string('═', boxWidth - 2)}╗";
        string boxBottom = $"╚{new string('═', boxWidth - 2)}╝";

        // Format each line, ensuring they are padded to match the box width
        string formattedFirstMessage = $"║ {firstMessage.PadRight(boxWidth - 4)} ║";
        string formattedSecondMessage = $"║ {secondMessage.PadRight(boxWidth - 4)} ║";

        // Print the dynamically sized box
        Console.WriteLine(boxTop);
        Console.WriteLine(formattedFirstMessage);
        Console.WriteLine(formattedSecondMessage);
        Console.WriteLine(boxBottom);
    }

    /// <summary>
    /// Asks the player for confirmation.
    /// </summary>
    /// <param name="player">The player being asked for confirmation.</param>
    /// <param name="option">The option selected by the player.</param>
    public static void SwitchConfirmation(IPlayer player, int option)
    {
        ArgumentNullException.ThrowIfNull(player);

        if (option == 0)
        {
            IPokemon pokemon = player.SelectedPokemon;

            // Messages to display
            string firstMessage = $"{player.Name}, your selected Pokémon has been changed!";
            string secondMessage = $"Now it is {pokemon.Name}.";

            // Determine the maximum message length to set box width
            int boxWidth = Math.Max(firstMessage.Length, secondMessage.Length) + 4; // Add space for borders and padding

            // Construct the top and bottom borders for the box
            string boxTop = $"╔{new string('═', boxWidth - 2)}╗";
            string boxBottom = $"╚{new string('═', boxWidth - 2)}╝";

            // Format each line, ensuring they are padded to match the box width
            string formattedFirstMessage = $"║ {firstMessage.PadRight(boxWidth - 4)} ║";
            string formattedSecondMessage = $"║ {secondMessage.PadRight(boxWidth - 4)} ║";

            // Print the dynamically sized box
            Console.WriteLine(boxTop);
            Console.WriteLine(formattedFirstMessage);
            Console.WriteLine(formattedSecondMessage);
            Console.WriteLine(boxBottom);
        }
    }

    /// <summary>
    /// Lets the player see that the action has been canceled.
    /// </summary>
    public static void CancelSwitchMessage()
    {
        Console.WriteLine("Has decidido no cambiar de Pokémon. Continúa con tu turno.");
        Console.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadKey();
        Console.Clear();
    }

    /// <summary>
    /// This a method to print the list of items from each player, this make possible that
    /// we can give this information to the player.
    /// </summary>
    /// <param name="items">List of items of the player.  </param>
    public static void PrintearItems(List<List<Item>> items)
    {
        // Check if the items list is null or empty
        if (items != null && items.Count > 0)
        {
            // Create a table-like header
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine($"║  You have these items:                ║");

            // Loop through each category of items
            for (int i = 0; i < items.Count; i++)
            {
                // Check if the current category has items in it
                if (items[i].Count > 0)
                {
                    // Dynamically display the item category and count
                    string itemName = items[i].FirstOrDefault()?.Name ?? "Unnamed Item";
                    Console.WriteLine($"║ {i + 1}) {itemName} x{items[i].Count,-4}    ║");
                }
            }

            // Close the table-like border
            Console.WriteLine("╚═══════════════════════════════════════╝");
        }
        else
        {
            // If no items are available
            Console.WriteLine("You don't have any items.");
        }
    }
    /// <summary>
    /// Displays a summary of the attack performed during the battle.
    /// </summary>
    /// <param name="attacker">The Pokémon that performed the attack.</param>
    /// <param name="attack">The attack used by the Pokémon.</param>
    /// <param name="receiver">The Pokémon that received the attack.</param>
    /// <param name="damage">The amount of damage inflicted.</param>
    public static void AttackSummary(IPokemon attacker, IAttack attack, IPokemon receiver, int damage)
    {
        // Calculate the width dynamically based on the longest line
        if (attacker != null && receiver != null && attack != null)
        {
            string line1 = $"{attacker.Name} used {attack.Name}!";
            string line2 = $"It dealt {damage} damage.";
            string line3 = $"{receiver.Name} has {receiver.Health} HP remaining.";

            int boxWidth = Math.Max(line1.Length, Math.Max(line2.Length, line3.Length)) + 4;

            // Create borders
            string topBorder = $"╔{new string('═', boxWidth - 2)}╗";
            string bottomBorder = $"╚{new string('═', boxWidth - 2)}╝";

            // Print attack summary
            Console.WriteLine(topBorder);
            Console.WriteLine($"║ {line1.PadRight(boxWidth - 4)} ║");
            Console.WriteLine($"║ {line2.PadRight(boxWidth - 4)} ║");
            Console.WriteLine($"║ {line3.PadRight(boxWidth - 4)} ║");
            Console.WriteLine(bottomBorder);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pokemon"></param>
    public static void CantAttackBecauseOfStatus (IPokemon pokemon)
    {
        if (pokemon == null)
            return;

        // Message content
        string line1 = $"{pokemon.Name} can't attack!";
        string line2 = $"Reason: It is {pokemon.State}.";

        // Calculate box width dynamically
        int boxWidth = Math.Max(line1.Length, line2.Length) + 4;

        // Create borders
        string topBorder = $"╔{new string('═', boxWidth - 2)}╗";
        string bottomBorder = $"╚{new string('═', boxWidth - 2)}╝";

        // Print status message
        Console.WriteLine(topBorder);
        Console.WriteLine($"║ {line1.PadRight(boxWidth - 4)} ║");
        Console.WriteLine($"║ {line2.PadRight(boxWidth - 4)} ║");
        Console.WriteLine(bottomBorder);
    }

    /// <summary>
    /// Displays the effect and the life that the pokemon looses.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="effect"></param>
    /// <param name="losedHealth"></param>
    public static void DisplayEffect(string name, SpecialEffect effect, int losedHealth)
    {
        // Calculate the width dynamically based on the longest line
        if (name == null) return;
        
        string line1 = $"{name} is {effect}ed";
        string line2 = $"It lost {losedHealth} HP!";
        string line3 = $"Go retaliate!";
        // should be using switch but..
        if (effect == SpecialEffect.Sleep) { line1 = $"{name} is Asleep"; }

        int boxWidth = Math.Max(line1.Length, Math.Max(line2.Length, line3.Length)) + 4;

        // Create borders
        string topBorder = $"╔{new string('═', boxWidth - 2)}╗";
        string bottomBorder = $"╚{new string('═', boxWidth - 2)}╝";

        // Print attack summary
        Console.Clear();
        Console.WriteLine(topBorder);
        Console.WriteLine($"║ {line1.PadRight(boxWidth - 4)} ║");
        Console.WriteLine($"║ {line2.PadRight(boxWidth - 4)} ║");
        Console.WriteLine($"║ {line3.PadRight(boxWidth - 4)} ║");
        Console.WriteLine(bottomBorder);
    }


}