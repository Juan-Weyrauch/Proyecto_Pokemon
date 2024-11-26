using Library.Game.Attacks;
using Library.Game.Items;
using Library.Game.Players;
using Library.Game.Pokemons;
using Library.Game.Utilities;

namespace Library.Facade;

/// <summary>
/// Static class responsible for managing the battle between two players.
/// It includes turn selection, displaying the current player's options, 
/// and delegating actions to the appropriate methods.
/// 
/// You’re not re-creating the values; you’re simply accessing them as part of Battle.StartBattle. 
/// When you pass player1 and player2 to Battle, you’re passing the references to these Player objects. 
/// This means that Battle is using the same player instances created in Facade—it’s not making new copies of them.
/// </summary>

// The methods in this class are: (in order)
// 1) StartBattle
// 2) ProcessTurnEffectsForPlayers
// 3) PlayerAction
// 4) StateValidation
// 5) Attack
// 6) UseItem
// 7) VoluntarySwitchPokemon
// 8) ForceSwitchPokemon

public static class Battle
{
    private static readonly Random Random = new Random();

    /// <summary>
    /// Starts the battle by selecting the turn order and guiding each player's actions.
    /// </summary>
    public static void StartBattle()
    {
        Player currentPlayer = Player.Player1;
        Player opposingPlayer = Player.Player2;

        while (true)
        {
            // Apply turn-based effects on both players' Pokémon.
            ProcessTurnEffectsForPlayers(currentPlayer);
            
            // chequeamos que el actual tenga vida:
            if (currentPlayer.SelectedPokemon.Health == 0)
            {
                // si el pokemon seleccionado no tiene vida:
                currentPlayer.CarryToCementerio(); // se elimina el pokemon del equipo del player.
            }
            
            // check if the player still has active Pokémon
            if (!Calculator.HasActivePokemon(currentPlayer))
            {
                Printer.DisplayWinner(opposingPlayer.Name); // si no tiene, se muestra al ganador y termina el programa
                break;
            }
            
            // si entra aca es poruqe 1) el pokemon seleccionado no tiene vida y 2) todavía tiene pokemons
            if (currentPlayer.SelectedPokemon.Health == 0)
            {
                Printer.ForceSwitchMessage(currentPlayer);
                ForceSwitchPokemon(currentPlayer);
            }

            // si aún tiene pokemons activos:


            // si aún tiene vida, el jugador elije que hacer:
            PlayerAction(currentPlayer, opposingPlayer);
            
            // cuando vuelva se cambian los turnos
            (currentPlayer, opposingPlayer) = (opposingPlayer, currentPlayer);
        }
    }
    
    /// <summary>
    /// Process turn-based effects for current players' Pokémon
    /// </summary>
    private static void ProcessTurnEffectsForPlayers(Player currentPlayer)
    {
        currentPlayer.SelectedPokemon.ProcessTurnEffects();
    }

    /// <summary>
    /// Handles the selected action for the current player's turn.
    /// </summary>
    /// <param name="player">The current player whose turn is being handled.</param>
    /// <param name="rival">The opposing player in the battle.</param>
    private static void PlayerAction(IPlayer player, IPlayer rival)
    {
        Console.Clear();
        Printer.ShowSelectedPokemon(player.SelectedPokemon, player.Name);
        Printer.ShowSelectedPokemon(rival.SelectedPokemon, rival.Name);

        // Validate the state before allowing any actions.
        if (!Calculator.StateValidation(player))
        {
            IPokemon pokemon = player.SelectedPokemon;
            Console.Clear();
            Printer.CantAttackBecauseOfStatus(pokemon);
            Printer.SkippingDueToStatus();
            Printer.PressToContinue();
            return; // End the turn without further actions.
        }
        
        // if everything checks, we procede with the players turn
        Printer.YourTurn(player.Name);
        Printer.ShowTurnInfo(player, player.SelectedPokemon);

        bool repeatTurn = true;

        while (repeatTurn) // Repeat while the turn is not over (the player can cancel certain actions)
        {
            int choice = Calculator.ValidateSelectionInGivenRange(1, 3);

            switch (choice)
            {
                case 1: // Attack
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

    

    /// <summary>
    /// This method is responsible for:
    /// 1) Showing the available attacks to the player.
    /// 2) Calling the damage Calculator and storing that int.
    /// 3) Inflicting damage to the opponent's selected Pokémon.
    /// </summary>
    /// <param name="player">The player performing the attack.</param>
    /// <param name="rival">The opposing player who is receiving the attack.</param>
    private static void Attack(IPlayer player, IPlayer rival)
    {
        IPokemon attacker = player.SelectedPokemon;
        IPokemon receiver = rival.SelectedPokemon;

        Printer.ShowAttacks(attacker, receiver);

        int attackInput = Calculator.ValidateSelectionInGivenRange(1, 4);
        IAttack attack = attacker.GetAttack(attackInput - 1);

        if (Random.Next(1, 101) > attack.Accuracy)
        {
            Printer.MissedAttack(attack, player.Name);
            Printer.PressToContinue();
            return;
        }

        Calculator.InfringeDamage(attack, receiver, attacker);

        // Check if the attack has a special effect and apply it
        if (attack.Special != SpecialEffect.None)
        {
            receiver.ApplyStatusEffect(attack.Special);
            Printer.WasAfected(receiver, attack);
        }
        Printer.PressToContinue();
    }
    
     /// <summary>
    /// Method that allows the player to use an item during their turn. 
    /// Depending on the item, the player may use it on a Pokémon from their team or the cemetery.
    /// </summary>
    /// <param name="player">The player using the item.</param>
    /// <param name="rival">The opposing player in the battle.</param>
    private static void UseItem(IPlayer player, IPlayer rival)
    {
        // Show the player's available items
        Printer.PrintItems(player.Items);

        // Ask the player to select an item
        int itemSelection = Calculator.ValidateSelectionInGivenRange(1, player.Items.Count); // Assuming 3 types of items
        Item item = player.GetItem(itemSelection); // Call the player's UseItem method

        if (item != null)
        {
            // If the item is a healing item (SuperPotion or TotalCure), ask the player which Pokémon to use it on
            if (item is SuperPotion or TotalCure)
            {
                Printer.ShowInventory(player.Pokemons);
                Console.WriteLine($"Which Pokémon do you want to use {item.Name} on?");
                int pokemonChoice = Calculator.ValidateSelectionInGivenRange(1, player.Pokemons.Count) -1;
                item.Use(player,pokemonChoice); // Use the item on the selected Pokémon
                player.RemoveItem(itemSelection);
            }
            // If it's a revive potion, use it on a Pokémon in the cemetery
            else if (item is RevivePotion)
            {
                if (player.Cementerio.Count > 0)
                {
                    Printer.ShowInventory(player.Cementerio);
                    Console.WriteLine($"Which Pokémon do you want to revive with {item.Name}?");
                    int pokemonChoice = Calculator.ValidateSelectionInGivenRange(1, player.Cementerio.Count) -1;
                    item.Use(player, pokemonChoice); // Revive the selected Pokémon
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
    
    /// <summary>
    /// Method that allows the player to voluntarily change their selected Pokémon during their turn.
    /// </summary>
    /// <param name="player">The player attempting to switch their Pokémon.</param>
    /// <returns>Returns a boolean indicating whether the Pokémon was successfully switched.</returns>
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
        Printer.PressToContinue();
        Console.Clear();

        Printer.ShowSelectedPokemon(player.SelectedPokemon, player.Name);

        Printer.PressToContinue();
        return true; // Pokémon switched successfully
    }

    /// <summary>
    /// Method that allows the player to change their Pokémon, forced by the defeat of the current Pokémon.
    /// </summary>
    /// <param name="player">The player who needs to switch their defeated Pokémon.</param>
    private static void ForceSwitchPokemon(Player player)
    {
        // Notify that the Pokémon was defeated and the player must switch it
        Console.Clear();
        Printer.ForceSwitchMessage(player);
        Printer.ShowInventory(player.Pokemons);
        int selectedIndex = Calculator.ValidateSelectionInGivenRange(1, player.Pokemons.Count) - 1;
        IPokemon selectedPokemon = player.Pokemons[selectedIndex];
        
        // se efectúa el cambio
        player.SwitchPokemon(selectedIndex);
        Printer.SwitchConfirmation(player, 0);
        Console.WriteLine($"{player.Name} switched to {selectedPokemon.Name}.");
        Console.ReadLine();

    }
}