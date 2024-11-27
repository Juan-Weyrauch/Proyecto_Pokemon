using System.Data;
using Library.Game.Attacks;
using Library.Game.Players;
using Library.Game.Pokemons;
using Library.Game.Attacks;
using Library.Game.Items;
using Library.Game.Players;
using Library.Game.Pokemons;
using Library.Game.Utilities;


namespace Library.Game.Utilities;

public class DefesaJesus
{

    public int Probabilidad { get; set; }
    public int Pokepoint { get; set; }
    public int Statuspoint { get; set; }
    public int Itempoint { get; set; }

    public static void PokemonPoint(IPlayer player)
    {
        ArgumentNullException.ThrowIfNull(player);
        ArgumentNullException.ThrowIfNull(player.Pokemons);
        if (player.Cementerio.Count > 0)
        {
            foreach (var pokemon in player.Cementerio)
            {
                int Pokepoint = -10;
            }
        }
    }

    public static void StatePoint(IPlayer player)
    {
        IPokemon pokemon = player.SelectedPokemon;
        switch (pokemon.State)
        {
            case SpecialEffect.None:
                return Statuspoint = 0;

            case SpecialEffect.Sleep:
                return Statuspoint = -10;

            case SpecialEffect.Paralyze:
                return Statuspoint = -10;
            case SpecialEffect.Poison:
                return Statuspoint = -10;
            case SpecialEffect.Burn:
                return Statuspoint = -10;
        }
    }

    private static void Itemspoint(IPlayer player, IPlayer rival)
    {

        int itemSelection = Calculator.ValidateSelectionInGivenRange(1, player.Items.Count);
        foreach (var item in player.Items)
        {
            return Itempoint = +6;
        }
    }

    private static void Probability()
    {
        int Probabilidad = int Pokepoint + int Statuspoint + Itempoint;
    }
}