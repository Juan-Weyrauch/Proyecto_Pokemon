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
    public static void StartBattle()
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

    private static void AplicarEfectos(IPokemon pokemon)
    {
        Random random = new Random();

        switch (pokemon.State)
        {
            case Estado.Paralizado:
                if (random.NextDouble() < 0.5)  // 50% de probabilidad de no poder atacar
                {
                    Console.WriteLine($"{pokemon.Name} está paralizado y no puede atacar.");
                }
                else
                {
                    Console.WriteLine($"{pokemon.Name} está paralizado pero puede atacar.");
                }
                break;
            
            case Estado.Dormido:
                if (pokemon.TurnosDormido > 0)
                {
                    Console.WriteLine($"{pokemon.Name} está dormido y pierde el turno.");
                    pokemon.TurnosDormido--; // Reduce los turnos de sueño
                    return; // Sale si el Pokémon pierde el turno
                }
                else
                {
                    // Verifica si el Pokémon se despierta antes de terminar los 4 turnos
                    if (random.NextDouble() < 0.25) // 25% de probabilidad de despertar antes
                    {
                        Console.WriteLine($"{pokemon.Name} se ha despertado antes de tiempo.");
                        pokemon.State = Estado.Normal; // Cambia el estado a normal
                    }
                    else
                    {
                        Console.WriteLine($"{pokemon.Name} sigue dormido.");
                    }
                }
                break;
            
            case Estado.Quemado:
                Console.WriteLine($"{pokemon.Name} está quemado y recibe daño residual.");
                pokemon.DecreaseHealth((int)(pokemon.InitialHealth * 0.10)); // Daño residual por quemadura

                // Verificación de muerte por quemadura
                if (pokemon.Health <= 0)
                {
                    Console.WriteLine($"{pokemon.Name} ha muerto por quemaduras.");
                    // Lógica para eliminar al Pokémon y elegir otro
                    // Aquí puedes llamar a la función que maneja el cambio de Pokémon
                }
                else
                {
                    Console.WriteLine($"{pokemon.Name} sufre daño por quemaduras.");
                }
                break;

            case Estado.Envenenado:
                Console.WriteLine($"{pokemon.Name} está envenenado y recibe daño residual.");
                pokemon.DecreaseHealth((int)(pokemon.InitialHealth * 0.05)); // Daño residual por veneno

                // Verificación de muerte por veneno
                if (pokemon.Health <= 0)
                {
                    Console.WriteLine($"{pokemon.Name} ha muerto por envenenamiento.");
                    // Lógica para eliminar al Pokémon y elegir otro
                    // Aquí puedes llamar a la función que maneja el cambio de Pokémon
                        
                }
                else
                {
                    Console.WriteLine($"{pokemon.Name} sufre daño por veneno.");
                }
                break;

            default:
                break;
        }
    }
}
