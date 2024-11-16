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
        Player currentPlayer = Player.Player1;
        Player opposingPlayer = Player.Player2;

        // Continue looping while both players have at least one active Pokémon
        while (Calculator.HasActivePokemon(currentPlayer) && Calculator.HasActivePokemon(opposingPlayer))
        {
            // Perform the player's action
            PlayerAction(currentPlayer, opposingPlayer);

            // Check if the opposing player has no Pokémon left
            if (!Calculator.HasActivePokemon(opposingPlayer))
            {
                Printer.DisplayWinner(currentPlayer.Name); // Announce the winner
                return; // Exit the battle loop
            }

            // Check if the current player has no Pokémon left (redundant for symmetry, unlikely to occur here)
            if (!Calculator.HasActivePokemon(currentPlayer))
            {
                Printer.DisplayWinner(opposingPlayer.Name); // Announce the winner
                return; // Exit the battle loop
            }

            // Swap players for the next turn
            (currentPlayer, opposingPlayer) = (opposingPlayer, currentPlayer);
        }
    }

    /// <summary>
    /// Handles the selected action for the current player's turn.
    /// </summary>
    private static void PlayerAction(IPlayer player, IPlayer rival)
    {
        // Inform whose turn it is and show the selected Pokémon
        Printer.YourTurn(player.Name);

        // Show available options for the player and turn information
        Printer.ShowTurnInfo(player, player.SelectedPokemon);

        // Validate the player's choice
        int choice = Calculator.ValidateSelectionInGivenRange(1, 3);

        // Execute the action based on the player's choice
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
        }
    }

    /// <summary>
    /// Handles the attack action, allowing the player to choose an attack and inflict damage.
    /// </summary>
    private static void Attack(IPlayer player, IPlayer rival)
    {
        IPokemon attacker = player.SelectedPokemon;
        IPokemon receiver = rival.SelectedPokemon;

        // Display available attacks
        Printer.ShowAttacks(attacker, receiver);

        // Let the player pick one
        int attackInput = Calculator.ValidateSelectionInGivenRange(1, 4);

        // Get the selected attack
        IAttack attack = attacker.GetAttack(attackInput - 1);

        // Inflict damage using the Calculator
        Calculator.InfringeDamage(attack, receiver);

        // Show both Pokémon's status after the attack
        Printer.ShowSelectedPokemon(attacker, player.Name);
        Printer.ShowSelectedPokemon(receiver, rival.Name);
    }

    private static void UseItem(IPlayer player)
    {
        // Logic for using an item from player's inventory
        throw new NotImplementedException();
    }

    private static void SwitchPokemon(IPlayer player)
    {
        // Logic for switching Pokémon
        throw new NotImplementedException();
    }
}
