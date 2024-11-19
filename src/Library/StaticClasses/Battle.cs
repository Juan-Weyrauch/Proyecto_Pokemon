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

        int EstadoDelPokemon = ChequearEstado(attacker);

        AplicarEfectos(attacker);

        //1) Display the available attacks:
        Printer.ShowAttacks(attacker, receiver);

        // Let the player pick one.
        int attackInput = Calculator.ValidateSelectionInGivenRange(1, 4);


        // We get the attack of the Pokémon
        IAttack attack = attacker.GetAttack(attackInput - 1).Clone();
        if (attack.Special == 0)
        {
            //2) now we call for a function that uses the attack on the rivals Pokémon.
            Calculator.InfringeDamage(attack, receiver);

            //3) We print (both) Pokémon life
            Printer.ShowSelectedPokemon(attacker, player.Name);
            Printer.ShowSelectedPokemon(receiver, rival.Name);
        }
        else
        {
            ChequearAtaqueEspecial(attack, player, rival);
            Calculator.InfringeDamage(attack, receiver);
        }


        //End: Returns to StartBattle
    }
    /// <summary>
    /// Checks the current state of the Pokémon.
    /// </summary>
    /// <param name="pokemon">The Pokémon whose state is being checked.</param>
    /// <returns>Returns 0 if the Pokémon is in normal state, otherwise 1.</returns>
    public static int ChequearEstado(IPokemon pokemon)
    {
        // Ahora usamos directamente el enum Estado en lugar de un valor numérico
        if (pokemon.State == Estado.Normal)
        {
            return 0; // Normal
        }
        else
        {
            return 1; // Otros estados
        }
    }
    /// <summary>
    /// Checks if the selected attack has any special effect and applies it if necessary.
    /// </summary>
    /// <param name="ataque">The attack being used.</param>
    /// <param name="jugadorActual">The current player using the attack.</param>
    /// <param name="jugadorRival">The opposing player being attacked.</param>
    public static void ChequearAtaqueEspecial(IAttack ataque, IPlayer jugadorActual, IPlayer jugadorRival)
    {
        IAttack ataqueActual = ataque;
        int tipo = ataqueActual.Special;
        IPokemon attacker = jugadorActual.SelectedPokemon;
        IPokemon receiver = jugadorRival.SelectedPokemon;

        if (tipo == 1) // Quemar
        {
            //ImpresoraDeTexto.ImprimirCambioEstado(nombrePoke, nombreAtaque, 1);
            receiver.CambiarEstado(1); // Cambia el estado del rival a quemado
        }
        else if (tipo == 2) // Dormir
        {
            //ImpresoraDeTexto.ImprimirCambioEstado(nombrePoke, nombreAtaque, 2);
            receiver.CambiarEstado(2);
        }
        else if (tipo == 3) // Paralizar
        {
            //ImpresoraDeTexto.ImprimirCambioEstado(nombrePoke, nombreAtaque, 3);
            receiver.CambiarEstado(3);
        }
        else if (tipo == 4) // Veneno
        {
            //ImpresoraDeTexto.ImprimirCambioEstado(nombrePoke, nombreAtaque, 4);
            receiver.CambiarEstado(4);
        }
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
        selectedPokemon = Calculator.ValidateSelectionInGivenRange(1, pokemons.Count);
        player.SwitchPokemon(selectedPokemon - 1);

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
        // Notificar que el Pokémon ha sido derrotado y el jugador debe cambiarlo
        Printer.ForceSwitchMessage(player);

        // Mover el Pokémon derrotado al cementerio antes de cambiar
        player.CarryToCementerio(); // Elimina el Pokémon del equipo y lo agrega al cementerio

        List<IPokemon> pokemons = player.Pokemons;

        Printer.ShowInventory(player.Pokemons);

        // we ask for pokemon input
        int selectedPokemon;
        selectedPokemon = Calculator.ValidateSelectionInGivenRange(1, pokemons.Count);
        // Cambiar el Pokémon seleccionado
        player.SwitchPokemon(selectedPokemon - 1);


        // Confirmar el cambio de Pokémon
        Printer.SwitchConfirmation(player, 0);
        Console.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadLine();
    }



    /// <summary>
    /// Applies the effects of a Pokémon's current state (e.g., paralysis, sleep, burn, poison).
    /// </summary>
    /// <param name="pokemon">The Pokémon whose effects are being applied.</param>
    private static void AplicarEfectos(IPokemon pokemon)
    {
        Random random = new Random();

        switch (pokemon.State)
        {
            case Estado.Paralizado:
                if (random.NextDouble() < 0.5) // 50% de probabilidad de no poder atacar
                {
                    Printer.ImprimirCambioEstado(pokemon.Name,3);
                }
                else
                {
                    Console.WriteLine($"{pokemon.Name} está paralizado pero puede atacar.");
                }

                break;

            case Estado.Dormido:
                if (pokemon.TurnosDormido > 0)
                {
                    Printer.ImprimirCambioEstado(pokemon.Name,4);
                    pokemon.TurnosDormido--; // Reduce los turnos de sueño
                    return; // Sale si el Pokémon pierde el turno
                }
                else
                {
                    // Verifica si el Pokémon se despierta antes de terminar los 4 turnos
                    if (random.NextDouble() < 0.25) // 25% de probabilidad de despertar antes
                    {
                        Console.WriteLine($"{pokemon.Name} se ha despertado antes de tiempo.");
                        pokemon.CambiarEstado(0); // Cambia el estado a normal
                    }
                    else
                    {
                        Console.WriteLine($"{pokemon.Name} sigue dormido.");
                    }
                }

                break;

            case Estado.Quemado:
                Printer.ImprimirCambioEstado(pokemon.Name,1);
                pokemon.DecreaseHealth((int)(pokemon.InitialHealth * 0.10)); // Daño residual por quemadura
                // Verificación de muerte por quemadura
                if (pokemon.Health <= 0)
                {
                    Console.WriteLine($"{pokemon.Name} ha muerto por quemaduras.");
                    /*player.CarryToCementerio();
                    ForceSwitchPokemon(player);*/
                }
                else
                {
                    Console.WriteLine($"{pokemon.Name} sufre daño por quemaduras.");
                }

                break;

            case Estado.Envenenado:
                Printer.ImprimirCambioEstado(pokemon.Name,2);
                pokemon.DecreaseHealth((int)(pokemon.InitialHealth * 0.05)); // Daño residual por veneno
                // Verificación de muerte por veneno
                if (pokemon.Health <= 0)
                {
                    Console.WriteLine($"{pokemon.Name} ha muerto por envenenamiento.");
                    /*player.CarryToCementerio();
                    ForceSwitchPokemon(player);*/

                }
                else
                {
                    Printer.ImprimirCambioEstado(pokemon.Name,2);
                }

                break;
        }
    }
}
    