namespace Library.Interfaces;

public interface IPlayer
{
    public string Name { get; }
    public List<IPokemon> Pokemons { get; }
    public List<IPotions> Potions { get; }
}