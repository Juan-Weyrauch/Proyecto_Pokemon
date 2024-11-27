using System.Globalization;
using Library.Game.Players;
using Library.Game.Pokemons;
using Library.Game.Utilities;

namespace Library.Facade;

/// <summary>
/// Responsable for:
///     1) Getting the users Info (Name, Pokémon Election)
///     2) Calls for the creation of the players with their elections
///     3) Calls the Battle class 
/// </summary>
public static class Facade
{
    /// <summary>
    /// Starts the program and asks the player for the initial input
    /// </summary>
    public static void Start()
    {
        //We print the options to the player
        Printer.StartPrint();
        int startSelection = Calculator.ValidateSelectionInGivenRange(1, 2); //uses > and <

        //We check the value inputted is within the desired ones
        
        if (startSelection == 2)
        {
            Printer.EndPrint();
            Environment.Exit(0);
        }
        //else continue
        //we need the user to insert their names and pick their Pokémon, so we create a second method that does this. 
        
        Facade.Selections();
    }

    public static void Selections()
    {
        // Crear el catálogo de Pokémon
        Catalogue.CreateCatalogue();

        // Iterar por los dos jugadores
        for (int i = 0; i < 2; i++)
        {
            Console.Clear();
        
            // Informar de quién es el turno
            Printer.YourTurn((i + 1).ToString(CultureInfo.InvariantCulture));
            // Pasamos el número del jugador, pero no necesitamos el Pokémon para esta parte.  

            // Variables del jugador
            List<IPokemon> playerPokemons = new List<IPokemon>();

            // Solicitar nombre del jugador
            Printer.NameSelection();
            string playerName = Console.ReadLine();

            // Mostrar todos los Pokémon disponibles
            Printer.ShowCatalogue(Catalogue.GetPokedex());

            // Selección de los Pokémon del jugador
            for (int j = 0; j < 6; j++)
            {
                Printer.AskForPokemon(j + 1, playerName); // Mostrar mensaje para seleccionar Pokémon
                int playerSelection = Calculator.ValidateSelectionInGivenRange(1, 20);
                playerPokemons.Add(Catalogue.GetPokemon(playerSelection).Clone()); // Agregar Pokémon a la lista
            }

            // Selección del Pokémon inicial
            Printer.ShowInventory(playerPokemons);
            Console.Write("\nPick your starting Pokemon: \n> ");
            int starterSelection = Calculator.ValidateSelectionInGivenRange(1, playerPokemons.Count);
            IPokemon selectedPokemon = playerPokemons[starterSelection - 1];

            // Crear jugadores en el sistema
            Facade.CreatePlayers(playerName, playerPokemons, selectedPokemon, i);
        }

        // Obtener las instancias de los jugadores
        Player player1 = Player.Player1;
        Player player2 = Player.Player2;

        // Iniciar la batalla
        Battle.StartBattle();
    }



    public static void CreatePlayers(string playerName, List<IPokemon> playerPokemons, IPokemon selectedPokemon, int playerIndex)
    {
        if (playerIndex < 0 || playerIndex > 1)
        {
            throw new ArgumentException("Invalid player index.");
        }

        if (playerIndex == 0)
        {
            Player.InitializePlayer1(playerName, playerPokemons, selectedPokemon);
        }
        else // playerIndex == 1
        {
            Player.InitializePlayer2(playerName, playerPokemons, selectedPokemon);
        }
    }
}