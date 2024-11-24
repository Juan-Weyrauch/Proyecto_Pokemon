
using Library.Game.Attacks;
using Library.Game.Items;
using Library.Game.Players;
using Library.Game.Pokemons;
using Library.Game.Utilities;


namespace Library.Facade
{
    /// <summary>
    /// Proporciona la lógica principal para gestionar las batallas Pokémon entre dos jugadores.
    /// Contiene métodos para iniciar la batalla, gestionar acciones de los jugadores y aplicar efectos de estado.
    /// </summary>
    public static class Battle
    {
        /// <summary>
        /// Inicia una batalla entre dos jugadores, alternando turnos hasta que uno pierda.
        /// </summary>
        public static void StartBattle()
        {
            Player currentPlayer = Player.Player1;
            Player opposingPlayer = Player.Player2;

            while (Calculator.HasActivePokemon(currentPlayer) && Calculator.HasActivePokemon(opposingPlayer))
            {
                // Check if the current player's Pokémon is fainted
                if (currentPlayer.SelectedPokemon.Health <= 0)
                {
                    Printer.ForceSwitchMessage(currentPlayer);
                    currentPlayer.CarryToCementerio();
                    ForceSwitchPokemon(currentPlayer);
                }

                // Execute player action if no Pokémon switch
                PlayerAction(currentPlayer, opposingPlayer);

                if (!Calculator.HasActivePokemon(opposingPlayer))
                {
                    Printer.DisplayWinner(currentPlayer.Name);
                    break;
                }

                // Switch turns
                (currentPlayer, opposingPlayer) = (opposingPlayer, currentPlayer);
            }
        }

        /// <summary>
        /// Gestiona las acciones de un jugador durante su turno en la batalla.
        /// Aplica efectos de estado, verifica el estado del Pokémon seleccionado y permite al jugador realizar una acción.
        /// </summary>
        /// <param name="player">El jugador que está tomando su turno.</param>
        /// <param name="rival">El jugador rival que será afectado por las acciones del jugador actual.</param>
        private static void PlayerAction(IPlayer player, IPlayer rival)
        {
            // Apply status effects before action
            bool feedback = AplicarEfectos(player.SelectedPokemon); //If something changed on the pokemon.
            
            // If the turn is skipped, exit early
            if (feedback)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
                return; // Skip the turn if paralyzed or asleep
            }

            // Ensure the player isn't trying to use a fainted Pokémon
            if (player.SelectedPokemon.Health <= 0)
            {
                Printer.ForceSwitchMessage(player);
                ForceSwitchPokemon(player);
                return;
            }

            Printer.YourTurn(player.Name);
            Printer.ShowTurnInfo(player, player.SelectedPokemon);

            bool repeatTurn = true;

            while (repeatTurn) // Repeat while the turn is not over
            {
                int choice = Calculator.ValidateSelectionInGivenRange(1, 3);

                switch (choice)
                {
                    case 1: // AttackList
                        Attack(player, rival);
                        repeatTurn = false; // Ends the turn
                        break;

                    case 2: // Use item
                        UseItem(player, rival);
                        repeatTurn = false; // Ends the turn
                        break;

                    case 3: // Switch Pokémon
                        bool changed = VoluntarySwitchPokemon(player);
                        if (changed)
                        {
                            repeatTurn = false; // Ends the turn if Pokémon was switched
                        }
                        else
                        {
                            // If no Pokémon was switched, show options again
                            Printer.ShowTurnInfo(player, player.SelectedPokemon);
                        }

                        break;
                }
            }
        }

        private static void Attack(IPlayer player, IPlayer rival)
        {
            IPokemon attacker = player.SelectedPokemon;
            IPokemon receiver = rival.SelectedPokemon;

            // Show the available attacks
            Printer.ShowAttacks(attacker, receiver);

            // Let the player pick one
            int attackInput = Calculator.ValidateSelectionInGivenRange(1, 4);

            // Get the attack selected by the player
            IAttack attack = attacker.GetAttack(attackInput - 1);
            receiver.CambiarEstado(attack.State);
            

            // Apply the attack on the rival's Pokémon
            Calculator.InfringeDamage(attack, receiver);

            // Show the updated health of both Pokémon
            Printer.ShowSelectedPokemon(attacker, player.Name);
            Printer.ShowSelectedPokemon(receiver, rival.Name);
        }

        private static void UseItem(IPlayer player, IPlayer rival)
        {
            // Show the player's available items
            Printer.PrintearItems(player.Items);

            // Ask the player to select an item
            int itemSelection = Calculator.ValidateSelectionInGivenRange(1, 3); // Assuming 3 types of items
            Item item = player.GetItem(itemSelection); // Call the player's UseItem method

            if (item != null)
            {
                // If the item is a healing item (SuperPotion or TotalCure), ask the player which Pokémon to use it on
                if (item is SuperPotion or TotalCure)
                {
                    Printer.ShowInventory(player.Pokemons);
                    Console.WriteLine($"Which Pokémon do you want to use {item.Name} on?");
                    int pokemonChoice = Calculator.ValidateSelectionInGivenRange(1, player.Pokemons.Count);
                    item.Use(player.Pokemons[pokemonChoice - 1]); // Use the item on the selected Pokémon
                    player.RemoveItem(itemSelection);
                }
                // If it's a revive potion, use it on a Pokémon in the cemetery
                else if (item is RevivePotion)
                {
                    if (player.Cementerio.Count > 0)
                    {
                        Printer.ShowInventory(player.Cementerio);
                        Console.WriteLine($"Which Pokémon do you want to revive with {item.Name}?");
                        int pokemonChoice = Calculator.ValidateSelectionInGivenRange(1, player.Cementerio.Count);
                        item.Use(player.Cementerio[pokemonChoice - 1]); // Revive the selected Pokémon
                        player.RemoveItem(itemSelection);
                    }
                    else
                    {
                        Console.WriteLine("No Pokémon to revive.");
                        PlayerAction(player, rival);
                    }
                }
            }
        }

        private static bool VoluntarySwitchPokemon(IPlayer player)
        {
            List<IPokemon> pokemons = player.Pokemons;

            // Show the option to change Pokémon
            Printer.SwitchQuestion(player);

            int option = Calculator.ValidateSelectionInGivenRange(1, 2);
            if (option == 2) // The player cancels the switch
            {
                Printer.CancelSwitchMessage();
                return false; // Pokémon won't be changed
            }

            Console.Clear();

            // Show inventory to choose a Pokémon
            Printer.ShowInventory(pokemons);

            // Validate that the selected Pokémon is not fainted
            int selectedPokemon = Calculator.ValidateSelectionInGivenRange(1, pokemons.Count);
            player.SwitchPokemon(selectedPokemon - 1);

            // Confirm the switch
            Printer.SwitchConfirmation(player, 0);
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
            Console.Clear();

            Printer.ShowSelectedPokemon(player.SelectedPokemon, player.Name);

            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
            return true; // Pokémon switched successfully
        }

        private static void ForceSwitchPokemon(IPlayer player)
        {
            // Notify that the Pokémon was defeated and the player must switch it
            Printer.ForceSwitchMessage(player);

            // Move the defeated Pokémon to the cemetery before switching
            player.CarryToCementerio(); // Removes the Pokémon from the team and adds it to the cemetery

            List<IPokemon> pokemons = player.Pokemons;

            Printer.ShowInventory(player.Pokemons);

            // Ask for Pokémon input
            int selectedPokemon = Calculator.ValidateSelectionInGivenRange(1, pokemons.Count);
            // Switch to the selected Pokémon
            player.SwitchPokemon(selectedPokemon - 1);

            // Confirm the Pokémon switch
            Printer.SwitchConfirmation(player, 0);
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

    /// <summary>
    /// Aplica los efectos del estado actual de un Pokémon, como parálisis, sueño, quemaduras o envenenamiento.
    /// Actualiza la salud del Pokémon o determina si pierde el turno debido al estado.
    /// </summary>
    /// <param name="pokemon">El Pokémon al que se aplicarán los efectos del estado.</param>
    /// <returns>
    /// Un valor booleano que indica si el Pokémon pierde el turno debido a su estado actual.
    /// <c>true</c> si el turno es omitido (por parálisis o sueño), <c>false</c> en caso contrario.
    /// </returns>
    private static bool AplicarEfectos(IPokemon pokemon)
    {
    Random random = new Random();

    switch (pokemon.State)
    {
        case Estado.Paralizado:
            Printer.ImprimirCambioEstado(pokemon.Name, 3); // Print "Paralized" state change message
            if (random.NextDouble() < 0.5) // 50% chance to be unable to attack
            {
             
                return true; // Skip the turn if paralyzed
            }
           
            break;

        case Estado.Dormido:
            Printer.ImprimirCambioEstado(pokemon.Name, 4); // Print "Asleep" state change message
            if (pokemon.TurnosDormido > 0)
            {
                
                pokemon.DecreaseTurnosDormido(); // Reduce sleep turns
                return true; // Skip the turn if still asleep
            }
            else
            {
                // Pokémon may wake up with a 25% chance
                if (random.NextDouble() < 0.25)
                {
                    Console.WriteLine($"{pokemon.Name} woke up early.");
                    pokemon.CambiarEstado(Estado.Normal); // Set to normal state
                    
                }
                else
                {
                    Console.WriteLine($"{pokemon.Name} is still asleep.");
                    return true;
                }
            }
            break;

        case Estado.Quemado:
            Printer.ImprimirCambioEstado(pokemon.Name, 1); // Print "Burned" state change message
            pokemon.DecreaseHealth((int)(pokemon.InitialHealth * 0.10)); // Apply burn damage (10% of initial health)
            
            break;

        case Estado.Envenenado:
            Printer.ImprimirCambioEstado(pokemon.Name, 2); // Print "Poisoned" state change message
            pokemon.DecreaseHealth((int)(pokemon.InitialHealth * 0.05)); // Apply poison damage (5% of initial health)
            
            break;
    }

    return false; // No state effect to skip the turn
}
    }
}
