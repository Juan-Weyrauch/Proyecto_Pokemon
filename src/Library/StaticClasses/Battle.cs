using System.Security.Cryptography;
using Library.Classes;
using Library.Interfaces;
namespace Library.StaticClasses;

/// <summary>
/// Static class responsible for managing the battle between two players.
/// It includes turn selection, displaying the current player's options, 
/// and delegating actions to the appropriate methods.
///
/// You’re not re-creating the values; you’re simply
/// accessing them as part of Battle.StartBattle. When you pass
/// player1 and player2 to Battle, you’re passing the references to
/// these Player objects. This means that Battle is using the same player
/// instances created in Facade—it’s not making new copies of them.
/// </summary>
public static class Battle
{
    /// <summary>
    /// Starts the battle by selecting the turn order and guiding each player's actions.
    /// </summary>
    public static void StartBattle()
    {
        Player currentPlayer = Player.Player1;
        Player opposingPlayer = Player.Player2;

        while (Calculator.HasActivePokemon(currentPlayer) && Calculator.HasActivePokemon(opposingPlayer))
        {
            // Check if the current player's selected Pokémon is defeated
            if (currentPlayer.SelectedPokemon.Health <= 0)
            {
                Printer.ForceSwitchMessage(currentPlayer);
                ForceSwitchPokemon(currentPlayer);
            }

            // Execute the player's turn
            PlayerAction(currentPlayer, opposingPlayer);

            // Check if the opposing player has lost all Pokémon
            if (!Calculator.HasActivePokemon(opposingPlayer))
            {
                Printer.DisplayWinner(currentPlayer.Name);
                break;
            }

            // Swap turns
            (currentPlayer, opposingPlayer) = (opposingPlayer, currentPlayer);
        }
    }

    

    /// <summary>
    /// Handles the selected action for the current player's turn.
    /// </summary>
    private static void PlayerAction(IPlayer player, IPlayer rival)
    {
        if (player.SelectedPokemon.Health <= 0)
        {
            // Safety check to ensure a defeated Pokémon is not used
            Printer.ForceSwitchMessage(player);
            ForceSwitchPokemon(player);
            return;
        }

        Printer.YourTurn(player.Name);
        Printer.ShowTurnInfo(player, player.SelectedPokemon);

        int choice = Calculator.ValidateSelectionInGivenRange(1, 3);

        switch (choice)
        {
            case 1:
                Attack(player, rival);
                break;
            case 2:
                UseItem(player);
                break;
            case 3:
                VoluntarySwitchPokemon(player);
                break;
        }
    }




    /// <summary>
    /// This method is responsible for:
    ///     1) Showing the available attacks to the player.
    ///     2) Calling the damage Calculator and storing that int.
    ///     3) Actually 'doing' the damage to the opponents selected Pokémon.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="rival"></param>
    private static void Attack(IPlayer player, IPlayer rival)
    {
        IPokemon attacker = player.SelectedPokemon;
        IPokemon receiver = rival.SelectedPokemon;
        
        //1) Display the available attacks:
        Printer.ShowAttacks(attacker, receiver);
        
        // Let the player pick one.
        int attackInput = Calculator.ValidateSelectionInGivenRange(1, 4);

        
        // We get the attack of the Pokémon
        IAttack attack = attacker.GetAttack(attackInput - 1); 
        
        //2) now we call for a function that uses the attack on the rivals Pokémon.
        Calculator.InfringeDamage(attack, receiver);
        
        //3) We print (both) Pokémon life
        Printer.ShowSelectedPokemon(attacker, player.Name);
        Printer.ShowSelectedPokemon(receiver, rival.Name);
        

        //End: Returns to StartBattle
    }

    /// <summary>
    /// Method that allows the player to use an item.
    /// </summary>
    /// <param name="player"></param>
    /// <exception cref="NotImplementedException"></exception>
    private static void UseItem(IPlayer player)
    {
        // Logic for using an item from player's inventory
        throw new NotImplementedException();
    }
    
    
    /// <summary>
    /// Method that allows to change the selected Pokémon. (Voluntarily)
    /// </summary>
    /// <param name="player"></param>
    private static void VoluntarySwitchPokemon(IPlayer player)
    {
        List<IPokemon> pokemons = player.Pokemons;
        // We show a message so that the player knows that it's changing its Pokémon
        Printer.SwitchConfirmation(player, 0);
        // We show the inventory so that the player chooses a Pokémon
        Printer.ShowInventory(pokemons);
        
        // We ask the player for its input
        int selectedPokemon;
        do
        {
            selectedPokemon = Calculator.ValidateSelectionInGivenRange(1, pokemons.Count);
        } while (player.Pokemons[selectedPokemon - 1].Health <= 0); // Ensure a usable Pokémon is chosen

        player.SwitchPokemon(selectedPokemon);
        
        //this method will show the player that it has changed its Pokémon when the value is 0
        Printer.SwitchConfirmation(player, 1);

        Printer.ShowSelectedPokemon(player.SelectedPokemon, player.Name);
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    
    /// <summary>
    /// Method that allows to change the selected Pokémon. (Foreced by defeat of current pokeon)
    /// </summary>
    /// <param name="player"></param>
    private static void ForceSwitchPokemon(IPlayer player)
    {
        List<IPokemon> pokemons = player.Pokemons;
        // We let know the user that it's Pokémon has been defeated, and it needs to change the actual one.
        Printer.ForceSwitchMessage(player);
        Printer.ShowInventory(player.Pokemons);

        int selectedPokemon;
        do
        {
            selectedPokemon = Calculator.ValidateSelectionInGivenRange(1, pokemons.Count);
        } while (player.Pokemons[selectedPokemon - 1].Health <= 0); // Ensure a healthy Pokémon is chosen

        player.SwitchPokemon(selectedPokemon);

        Printer.ShowSelectedPokemon(player.SelectedPokemon, player.Name);
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

}