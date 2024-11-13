using Library.Classes;
using Library.Interfaces;
using Library.StaticClasses;

namespace Library.StaticClasses;

/// <summary>
/// Static class responsible for managing the battle between two players.
/// It includes turn selection, displaying the current player's options, 
/// and delegating actions to the appropriate methods.
/// </summary>
public static class Battle
{
    /// <summary>
    /// Starts the battle by selecting the turn order and guiding each player's actions.
    /// </summary>
    public static void StartBattle()
    {
        Console.Clear();
        
        // Initialize players
        Player player1 = Player.Player1;
        Player player2 = Player.Player2;
        
        // These are like placeholders, we assign their values later on.
        Player player;
        Player rival;
        // Determine who goes first
        int turno = Calculator.FirstTurnSelection();
       
        if (turno == 1)
        {
            // We assign the values here!
            player = player1;
            rival = player2;
        }
        else
        {
            // (Or) We assign the values here!
            player = player2;
            rival = player1;
        }

        // Display each player's Pokémon
        Printer.ShowSelectedPokemon(player1.SelectedPokemon, player1.Name);
        Printer.ShowSelectedPokemon(player2.SelectedPokemon, player2.Name);

        // Begin battle loop
        while (true)
        {
            Console.Clear();
            Printer.ShowTurnInfo(player); // We display: Name, Options.

            // Player chooses an action
            PlayerAction(player, rival);
            
            // Switch turns
            (player, rival) = (rival, player); //this was suggested by rider, lol
        }
    }



    /// <summary>
    /// Handles the selected action for the current player's turn.
    /// </summary>
    private static void PlayerAction(Player player, Player rival)
    {
        int choice = Convert.ToInt32(Console.ReadLine());
        choice = Calculator.ValidateSelectionInGivenRange(choice, 1, 3);
        switch (choice)
        {
            case 1:
                Attack(player, rival);
                break;
            case 2:
                UseItem(player);
                break;
            case 3:
                SwitchPokemon(player);
                break;
            // no need for default:, we use ValidateSelectionInGivenRange
        }
    }

    private static void Attack(IPlayer player, IPlayer rival)
    {
        IPokemon pokemon = player.SelectedPokemon;
        IPokemon rivalPokemon = rival.SelectedPokemon;
        // Example usage of Calculator for attack damage calculation
        int damage = Calculator.CalculateAttack(player.SelectedPokemon, rival.SelectedPokemon);
        rival.SelectedPokemon.Health -= damage;
        Console.WriteLine($"{player.Name}'s {player.SelectedPokemon.Name} dealt {damage} damage to {rival.SelectedPokemon.Name}!");
    }

    private static void UseItem(IPlayer player)
    {
        // Logic for using an item from player's inventory
        Printer.DisplayInventory(player.Potions);
        int itemChoice = int.Parse(Console.ReadLine() ?? "0");
        player.UseItem(itemChoice);
        Console.WriteLine($"{player.Name} used an item!");
    }

    private static void SwitchPokemon(Player player)
    {
        // Logic for switching Pokémon
        Console.WriteLine($"{player.Name}, select a new Pokémon:");
        Printer.DisplayPokemons(player.Pokemons);
        int newPokemonIndex = int.Parse(Console.ReadLine() ?? "0");
        player.SwitchPokemon(newPokemonIndex);
        Console.WriteLine($"{player.Name} switched Pokémon!");
    }
}
