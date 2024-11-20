/*
using NUnit.Framework;
using Library.Interfaces;  // Asegúrate de que este es el espacio de nombres correcto
using Library.StaticClasses;  // Si necesitas esta librería
using Library.Classes;    // Si IPokemon o IPlayer están definidos en esta librería
using System.Collections.Generic;

// Implementación falsa de IPlayer
// Implementación falsa de IPlayer
public class FakePlayer : IPlayer
{
    public string Name { get; set; } = "Player";
    public IPokemon SelectedPokemon { get; set; }  // Esta propiedad debe ser de tipo IPokemon
    public List<IPokemon> Pokemons { get; set; } = new List<IPokemon>();  // Lista de IPokemon
    public List<IPokemon> Cementerio { get; set; } = new List<IPokemon>();  // Lista de IPokemon
    public List<IPotions> Potions { get; set; } = new List<IPotions>();
    public int Turn { get; set; }

    // Métodos requeridos por IPlayer
    public void CarryToCementerio() { }
    public void SwitchPokemon(int index) { }
    public void UseItem(int index) { }
}
// Implementación falsa de IPokemon
public class FakePokemon 
{
    public int Health { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        var other = (FakePokemon)obj;
        return Health == other.Health;
    }

    public override int GetHashCode()
    {
        return Health.GetHashCode();
    }
}

[TestFixture]
public class BattleTests
{
    [Test]
    public void Test_StartBattle()
    {
        // Crear implementaciones manuales de las interfaces
        var player1 = new FakePlayer();
        var player2 = new FakePlayer();
        var pokemon1 = new FakePokemon();
        var pokemon2 = new FakePokemon();

        // Asignar los Pokémon a los jugadores
        player1.SelectedPokemon = pokemon1;
        player2.SelectedPokemon = pokemon2;

        // Establecer la salud de los Pokémon
        pokemon1.Health = 100;
        pokemon2.Health = 100;

        // Llamar al método de batalla
        Battle.StartBattle();

        // Verificar que los Pokémon asignados a los jugadores sean correctos
        Assert.Equals(pokemon1, player1.SelectedPokemon);
        Assert.Equals(pokemon2, player2.SelectedPokemon);

        // Verificar que la salud no haya cambiado
        Assert.Equals(100, pokemon1.Health);
        Assert.Equals(100, pokemon2.Health);
    }
}
*/