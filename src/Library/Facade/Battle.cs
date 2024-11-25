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

        while (Calculator.HasActivePokemon(currentPlayer) && Calculator.HasActivePokemon(opposingPlayer))
        {
            // Apply turn-based effects on both players' Pokémon.
            currentPlayer.SelectedPokemon.ProcessTurnEffects();
            opposingPlayer.SelectedPokemon.ProcessTurnEffects();

            if (currentPlayer.SelectedPokemon.Health <= 0)
            {
                Printer.ForceSwitchMessage(currentPlayer);
               
                ForceSwitchPokemon(currentPlayer);
                
            }

            if (!Calculator.HasActivePokemon(opposingPlayer))
            {
                Printer.DisplayWinner(currentPlayer.Name);
                break;
            }

            PlayerAction(currentPlayer, opposingPlayer);
            (currentPlayer, opposingPlayer) = (opposingPlayer, currentPlayer);
        }
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
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();

        // Validate the state before allowing any actions.
        if (!StateValidation(player))
        {
            IPokemon pokemon = player.SelectedPokemon;
            Printer.CantAttackBecauseOfStatus(pokemon);
            Console.Clear();
            Console.WriteLine("Skipping turn due to status effect.");
            Console.ReadLine(); // Pause for player to read.
            return; // End the turn without further actions.
        }

        if (player.SelectedPokemon.Health <= 0)
        {
            player.CarryToCementerio(); // Move the defeated Pokémon to the cemetery before switching
            Printer.ForceSwitchMessage(player); // Warn the player that it must change its Pokémon
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
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
                case 1: // Attack
                    Attack(player, rival);
                    repeatTurn = false; // Ends the turn
                    break;

                case 2: // Use item
                    UseItemAux(player, rival);
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
    /// 
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    private static bool StateValidation(IPlayer player)
    {
        IPokemon pokemon = player.SelectedPokemon;

        switch (pokemon.State)
        {
            case SpecialEffect.None:
                return true; // The Pokémon can act normally.

            case SpecialEffect.Sleep:
                    pokemon.ProcessTurnEffects(); // Decrease sleep turns.
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadLine();
                    return false;

            case SpecialEffect.Paralyze:
                bool canAttack = Random.Next(0, 2) == 0; // 50% chance to act.
                if (!canAttack)
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadLine();
                }
                return canAttack;
    
            // these two cases are handled on the Pokémon class.
            case SpecialEffect.Poison:
            case SpecialEffect.Burn:
                // These effects reduce health but do not prevent actions.
                pokemon.ProcessTurnEffects();
                return true;
        }

        return true; // Default case (should not be reached).
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
            Console.WriteLine($"{attacker.Name} missed the attack {attack.Name}.");
            Console.ReadLine();
            return;
        }

        bool isCritical = attack.IsCritical();
        int damage = isCritical ? (int)(attack.Damage * 1.2) : attack.Damage;

        if (isCritical)
        {
            Console.WriteLine($"Critical hit! {attacker.Name} dealt extra damage.");
            Console.ReadLine();
        }

        Calculator.InfringeDamage(attack, receiver, attacker);

        // Check if the attack has a special effect and apply it
        if (attack.Special != SpecialEffect.None)
        {
            Console.WriteLine($"{receiver.Name} is affected by {attack.Name}'s special effect!");
            receiver.ApplyStatusEffect(attack.Special);
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
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        Console.Clear();

        Printer.ShowSelectedPokemon(player.SelectedPokemon, player.Name);

        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        return true; // Pokémon switched successfully
    }

    /// <summary>
    /// Method that allows the player to change their Pokémon, forced by the defeat of the current Pokémon.
    /// </summary>
    /// <param name="player">The player who needs to switch their defeated Pokémon.</param>
    private static void ForceSwitchPokemon(IPlayer player)
    {
        // Notify that the Pokémon was defeated and the player must switch it
        Printer.ForceSwitchMessage(player);


        while (true)
        {
            Printer.ShowInventory(player.Pokemons);
            int selectedIndex = Calculator.ValidateSelectionInGivenRange(1, player.Pokemons.Count) - 1;
            IPokemon selectedPokemon = player.Pokemons[selectedIndex];

            if (selectedPokemon.Health > 0)
            {
                player.SwitchPokemon(selectedIndex);
                Console.WriteLine($"{player.Name} switched to {selectedPokemon.Name}.");
                Console.ReadLine();
                break;
            }
            else
            {
                Console.WriteLine("You cannot select a fainted Pokémon. Please choose another.");
            }
        }
        Printer.ShowInventory(player.Pokemons);
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();

    }

    /// <summary>
    /// Method that allows the player to use an item during their turn. 
    /// Depending on the item, the player may use it on a Pokémon from their team or the cemetery.
    /// </summary>
    /// <param name="player">The player using the item.</param>
    /// <param name="rival">The opposing player in the battle.</param>
    private static void UseItemAux(IPlayer player, IPlayer rival)
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
}