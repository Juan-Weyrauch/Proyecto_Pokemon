using Library.Game.Attacks;
using Library.Game.Items;
using Library.Game.Players;
using Library.Game.Pokemons;
using Library.Game.Utilities;

namespace Library.Facade
{
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

            while (true)
            {
                // Check if the current player has no active Pokémon left
                if (!Calculator.HasActivePokemon(currentPlayer))
                {
                    Printer.DisplayWinner(opposingPlayer.Name);
                    break;
                }

                // Check if the opposing player has no active Pokémon left
                if (!Calculator.HasActivePokemon(opposingPlayer))
                {
                    Printer.DisplayWinner(currentPlayer.Name);
                    break;
                }

                // Ensure the current player's Pokémon is alive before the turn
                if (currentPlayer.SelectedPokemon.Health <= 0)
                {
                    currentPlayer.CarryToCementerio();
                    if (!Calculator.HasActivePokemon(currentPlayer))
                    {
                        Printer.DisplayWinner(opposingPlayer.Name);
                        break;
                    }
                    ForceSwitchPokemon(currentPlayer);
                }

                // Execute player action
                PlayerAction(currentPlayer, opposingPlayer);

                // Switch turns
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
                ForceSwitchPokemon(player);
                return;
            }

            Printer.YourTurn(player.Name);
            Printer.ShowTurnInfo(player, player.SelectedPokemon);

            bool repeatTurn = true;
            while (repeatTurn)
            {
                int choice = Calculator.ValidateSelectionInGivenRange(1, 3);

                switch (choice)
                {
                    case 1:
                        Attack(player, rival);
                        repeatTurn = false;
                        break;
                    case 2:
                        UseItemAux(player, rival);
                        repeatTurn = false;
                        break;
                    case 3:
                        repeatTurn = !VoluntarySwitchPokemon(player);
                        break;
                }
            }
        }

        /// <summary>
        /// Executes an attack from the current player to the opposing player.
        /// </summary>
        private static void Attack(IPlayer player, IPlayer rival)
        {
            IPokemon attacker = player.SelectedPokemon;
            IPokemon receiver = rival.SelectedPokemon;

            Printer.ShowAttacks(attacker, receiver);
            int attackInput = Calculator.ValidateSelectionInGivenRange(1, 4);
            IAttack attack = attacker.GetAttack(attackInput - 1);

            Random random = new Random();
            if (random.Next(1, 101) > attack.Accuracy)
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
            Printer.ShowSelectedPokemon(attacker, player.Name);
            Printer.ShowSelectedPokemon(receiver, rival.Name);
        }

        /// <summary>
        /// Allows the player to voluntarily switch their Pokémon.
        /// </summary>
        private static bool VoluntarySwitchPokemon(IPlayer player)
        {
            Printer.SwitchQuestion(player);

            if (Calculator.ValidateSelectionInGivenRange(1, 2) == 2)
            {
                Printer.CancelSwitchMessage();
                return false;
            }

            Printer.ShowInventory(player.Pokemons);
            int selectedPokemon = Calculator.ValidateSelectionInGivenRange(1, player.Pokemons.Count);
            player.SwitchPokemon(selectedPokemon - 1);

            Printer.SwitchConfirmation(player, 0);
            return true;
        }

        /// <summary>
        /// Forces the player to switch their Pokémon if the current one is defeated.
        /// </summary>
        private static void ForceSwitchPokemon(IPlayer player)
        {
            player.CarryToCementerio();
            Printer.ShowInventory(player.Pokemons);
            int selectedPokemon = Calculator.ValidateSelectionInGivenRange(1, player.Pokemons.Count);
            player.SwitchPokemon(selectedPokemon - 1);
            Printer.SwitchConfirmation(player, 0);
        }

        /// <summary>
        /// Allows the player to use an item during their turn.
        /// </summary>
        private static void UseItemAux(IPlayer player, IPlayer rival)
        {
            Printer.PrintearItems(player.Items);
            int itemSelection = Calculator.ValidateSelectionInGivenRange(1, player.Items.Count);
            Item item = player.GetItem(itemSelection);

            if (item is SuperPotion or TotalCure)
            {
                Printer.ShowInventory(player.Pokemons);
                int pokemonChoice = Calculator.ValidateSelectionInGivenRange(1, player.Pokemons.Count);
                item.Use(player.Pokemons[pokemonChoice - 1]);
                player.RemoveItem(itemSelection);
            }
            else if (item is RevivePotion && player.Cementerio.Count > 0)
            {
                Printer.ShowInventory(player.Cementerio);
                int pokemonChoice = Calculator.ValidateSelectionInGivenRange(1, player.Cementerio.Count);
                item.Use(player.Cementerio[pokemonChoice - 1]);
                player.RemoveItem(itemSelection);
            }
            else
            {
                Console.WriteLine("No Pokémon to revive.");
            }
        }
    }
  
}