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
            if (currentPlayer.SelectedPokemon.Health <= 0)
            {
                
                Printer.ForceSwitchMessage(currentPlayer);
                currentPlayer.CarryToCementerio();
                ForceSwitchPokemon(currentPlayer);
            }

            // Ejecutar acción del jugador si no cambió Pokémon
            PlayerAction(currentPlayer, opposingPlayer);

            if (!Calculator.HasActivePokemon(opposingPlayer))
            {
                Printer.DisplayWinner(currentPlayer.Name);
                break;
            }

            // Cambiar turnos
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

        bool repeatTurn = true;

        while (repeatTurn) // Repetir mientras el turno no termine
        {
            int choice = Calculator.ValidateSelectionInGivenRange(1, 3);

            switch (choice)
            {
                case 1: // Atacar
                    Attack(player, rival);
                    repeatTurn = false; // Finaliza el turno
                    break;

                case 2: // Usar objeto
                    UseItem(player);
                    repeatTurn = false; // Finaliza el turno
                    break;

                case 3: // Cambiar Pokémon
                    bool changed = VoluntarySwitchPokemon(player);
                    if (changed)
                    {
                        repeatTurn = false; // Finaliza el turno si se realiza el cambio
                    }
                    else
                    {
                        // Si no cambió Pokémon, vuelve a mostrar las opciones
                        Printer.ShowTurnInfo(player, player.SelectedPokemon);
                    }
                    break;
            }
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
    private static bool VoluntarySwitchPokemon(IPlayer player)
    {
        List<IPokemon> pokemons = player.Pokemons;

        // Mostrar la opción de cambiar Pokémon
        Printer.SwitchQuestion(player);

        int option = Calculator.ValidateSelectionInGivenRange(1, 2);
        if (option == 2) // El jugador cancela el cambio
        {
            Printer.CancelSwitchMessage();
            return false; // No cambiará el Pokémon
        }

        Console.Clear();

        // Mostrar inventario para elegir Pokémon
        Printer.ShowInventory(pokemons);

        // Validar que el Pokémon elegido no esté debilitado
        int selectedPokemon;
        do
        {
            selectedPokemon = Calculator.ValidateSelectionInGivenRange(1, pokemons.Count);
        } while (player.Pokemons[selectedPokemon - 1].Health <= 0);

        // Cambiar el Pokémon
        player.SwitchPokemon(selectedPokemon);

        // Confirmar el cambio
        Printer.SwitchConfirmation(player, 0);
        Console.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadLine();
        Console.Clear();

        Printer.ShowSelectedPokemon(player.SelectedPokemon, player.Name);

        Console.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadLine();
        return true; // Pokémon cambiado exitosamente
    }


    
    /// <summary>
    /// Method that allows to change the selected Pokémon. (Foreced by defeat of current pokeon)
    /// </summary>
    /// <param name="player"></param>
    private static void ForceSwitchPokemon(IPlayer player)
    {
        List<IPokemon> pokemons = player.Pokemons;
    
        // Notificar que el Pokémon ha sido derrotado y el jugador debe cambiarlo
        Printer.ForceSwitchMessage(player);
    
        // Mover el Pokémon derrotado al cementerio antes de cambiar
        player.CarryToCementerio();  // Elimina el Pokémon del equipo y lo agrega al cementerio

        Printer.ShowInventory(player.Pokemons);

        int selectedPokemon;
        do
        {
            // Validar que el jugador elija un Pokémon disponible (y que esté en buen estado)
            selectedPokemon = Calculator.ValidateSelectionInGivenRange(1, pokemons.Count);
        } while (player.Pokemons[selectedPokemon - 1].Health <= 0); // Asegurar que el Pokémon esté en estado saludable

        // Cambiar el Pokémon seleccionado
        player.SwitchPokemon(selectedPokemon);

        // Confirmar el cambio de Pokémon
        Printer.SwitchConfirmation(player, 0);
        Console.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadLine();
    }


}