using System.ComponentModel.Design;
using System.Globalization;
using Library.Classes;
using Library.Interfaces;

namespace Library.StaticClasses;

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
        int startSelection =Convert.ToInt16(Console.ReadLine(), CultureInfo.InvariantCulture);
        //We check the value inputted is within the desired ones
        Calculator.ValidateSelectionInGivenRange(startSelection, 1, 2); //uses > and <
        
        if (startSelection == 2)
        {
            Printer.EndPrint();
        }
        //else continue
        //we need the user to insert their names and pick their Pokémon, so we create a second method that does this. 
        
        Facade.Selections();
    }

    private static void Selections()
    {
        //We create all the Pokémon here
        Catalogue.CreateCatalogue();
        
        //We shall iterate for the two players
        for (int i = 0; i<2; i++)
        {
            // The three things that the player has to input: Name, Pokémon, and a selected one
            /*Name of the player*/   string playerName;
            /*Player's Pokémon */   List<IPokemon> playerPokemons = new List<IPokemon>();
            /*Selected pokemon  */   IPokemon selectedPokemon;
            
            //ask for the name
            Printer.NameSelection();
            playerName = Console.ReadLine();
            if(i==1){Printer.YourTurn("2");}
            
            //show all Pokémon
            Printer.ShowCatalogue(Catalogue.GetPokedex());
            int playerSelection;
            for (int j = 0; j < 6; j++)
            {
                Printer.AskForPokemon(j, playerName);
                //let the user pick one
                playerSelection = Convert.ToInt32(Console.ReadLine(), CultureInfo.InvariantCulture);
                //Validate it is within range 
                playerSelection = Calculator.ValidateSelectionInGivenRange(playerSelection, 1, 20);

                //add it to the list of Pokémon
                playerPokemons.Add(Catalogue.GetPokemon(playerSelection));
            }
            //let the user pick it's first Pokémon
            Printer.ShowInventory(playerPokemons);
            Console.Write("\nPick your starting Pokemon: \n> ");
            playerSelection = Convert.ToInt32(Console.ReadLine(), CultureInfo.InvariantCulture);
            playerSelection = Calculator.ValidateSelectionInGivenRange(playerSelection, 1, 6);
            selectedPokemon = playerPokemons[playerSelection-1];
            
            //Cretion of the two players:
             
            if (i == 0)
            { Player.InitializePlayer1(playerName, playerPokemons, selectedPokemon); }
            else if (i == 1)
            { Player.InitializePlayer2(playerName, playerPokemons, selectedPokemon); }
        }
        // Access singleton instances (if needed)
        Player player1 = Player.Player1;
        Player player2 = Player.Player2;
    }
}