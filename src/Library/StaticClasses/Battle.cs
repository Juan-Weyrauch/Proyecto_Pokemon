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
    public static void StartBattle()
    {
        // Determina el jugador inicial aleatoriamente
        Player currentPlayer = Calculator.FirstTurnSelection() == 1 ? Player.Player1 : Player.Player2;
        Player opposingPlayer = currentPlayer == Player.Player1 ? Player.Player2 : Player.Player1;

        while (Calculator.HasActivePokemon(currentPlayer) && Calculator.HasActivePokemon(opposingPlayer))
        {
            // Chequeo inicial del estado del jugador
            if (!HandlePlayerStatus(currentPlayer))
            {
                Printer.DisplayWinner(opposingPlayer.Name);
                break;
            }

            // Ejecutar la acción del jugador actual
            PlayerAction(currentPlayer, opposingPlayer);

            // Cambiar turnos
            (currentPlayer, opposingPlayer) = (opposingPlayer, currentPlayer);
        }
    }

    private static bool HandlePlayerStatus(Player player)
    {
        if (!Calculator.HasActivePokemon(player))
        {
            if (!player.Potions.OfType<RevivePotion>().Any())
            {
                Printer.DisplayNoPokemonsMessage(player.Name);
                return false;
            }

            // Forzar uso de Revive Potion si tiene disponible
            UseRevivePotion(player);
        }

        return true;
    }

    private static void UseRevivePotion(Player player)
    {
        var revivePotion = player.Potions.OfType<RevivePotion>().FirstOrDefault();
        if (revivePotion != null)
        {
            Printer.DisplayUsingRevivePotion(player.Name);
            player.Potions.Remove(revivePotion);

            // Revive el Pokémon con la salud más baja
            var faintedPokemon = player.Pokemons.FirstOrDefault(p => p.Health <= 0);
            if (faintedPokemon != null)
            {
                faintedPokemon.Health = faintedPokemon.InitialHealth / 2; // Revivir con 50% de salud
            }
        }
    }

    private static void PlayerAction(Player player, Player opponent)
    {
        Printer.YourTurn(player.Name);
        Printer.ShowTurnInfo(player, player.SelectedPokemon);

        int choice = Calculator.ValidateSelectionInGivenRange(1, 3);
        switch (choice)
        {
            case 1:
                Attack(player, opponent);
                break;
            case 2:
                UseItem(player);
                break;
            case 3:
                VoluntarySwitchPokemon(player);
                break;
        }
    }

    private static void UseItem(Player player)
    {
        if (!player.Potions.Any())
        {
            Printer.DisplayNoItemsMessage(player.Name);
            return;
        }

        Printer.ShowItems(player.Potions);
        int choice = Calculator.ValidateSelectionInGivenRange(1, player.Potions.Count);

        var selectedItem = player.Potions[choice - 1];
        selectedItem.ApplyEffect(player.SelectedPokemon);
        player.Potions.RemoveAt(choice - 1);
    }

    private static void Attack(Player player, Player opponent)
    {
        IPokemon attacker = player.SelectedPokemon;
        IPokemon receiver = opponent.SelectedPokemon;

        Printer.ShowAttacks(attacker, receiver);
        int attackInput = Calculator.ValidateSelectionInGivenRange(1, 4);
        IAttack attack = attacker.GetAttack(attackInput - 1);

        if (attack.IsSpecial && player.Turn < 2)
        {
            Printer.SpecialAttackNotReadyMessage();
            return;
        }

        if (attack.IsSpecial) player.Turn = 0;
        else player.Turn++;

        Calculator.InfringeDamage(attack, receiver);
        Printer.ShowSelectedPokemon(attacker, player.Name);
        Printer.ShowSelectedPokemon(receiver, opponent.Name);
    }
}
