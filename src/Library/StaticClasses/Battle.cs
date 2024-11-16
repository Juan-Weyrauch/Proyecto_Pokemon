using Library.Classes;
using Library.Interfaces;
using Library.StaticClasses;

namespace Library.StaticClasses;

/// <summary>
/// Static class responsible for managing the battle between two players.
/// It includes turn selection, displaying the current player's options, 
/// and delegating actions to the appropriate methods.
///
/// You’re not actually re-creating the values; you’re simply
/// accessing them as part of Battle.StartBattle. When you pass
/// player1 and player2 to Battle, you’re passing the references to
/// these Player objects. This means that Battle is using the same player
/// instances created in Selections—it’s not making new copies of them.
/// </summary>
public static class Battle
{
    /// <summary>
    /// Starts the battle by selecting the turn order and guiding each player's actions.
    /// </summary>
    public static void StartBattle(Player player1, Player player2)
    {

        Player currentPlayer = Player.Player1;
        Player opposingPlayer = Player.Player2;


        // Continue looping while both players have at least one active Pokémon
        
        while (Calculator.HasActivePokemon(currentPlayer) && Calculator.HasActivePokemon(opposingPlayer))
        {
            // Perform the player's action
            PlayerAction(currentPlayer, opposingPlayer);

            // Check if the battle should end (if any player's Pokémon died)
            if (!Calculator.HasActivePokemon(opposingPlayer))
            {
                Printer.DisplayWinner(currentPlayer.Name); // Announce the winner
                break;
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

        // Informar de quién es el turno y mostrar el Pokémon seleccionado
        Printer.YourTurn(player.Name);

        // Mostrar las opciones disponibles para el jugador y la información del turno
        Printer.ShowTurnInfo(player, player.SelectedPokemon);

        // Validar la elección del jugador

        int choice = Calculator.ValidateSelectionInGivenRange(1, 3);

        // Ejecutar la acción basada en la elección del jugador
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
