using Library.Facade;
using Library.Game.Attacks;
using Library.Game.Players;
using Library.Game.Pokemons;
using Library.Game.Utilities;
using NUnit.Framework;

namespace Library.Tests
{
    /// <summary>
    /// Tests for the Attack class.
    /// </summary>
    [TestFixture]
  static  public class CalcularChances
    {
        [Test]
        static public void TestCalcularQuemadoEfectos()
        {
            
            Catalogue.CreateCatalogue();
           Dictionary<int,IPokemon> pokedex = Catalogue.GetPokedex();
            List < IPokemon > Poke1 = new List<IPokemon>();
            Poke1.Add(pokedex[1]);
            Poke1.Add(pokedex[2]);
            Poke1.Add(pokedex[4]);
            Poke1.Add(pokedex[3]);
            Poke1.Add(pokedex[15]);
            Poke1.Add(pokedex[9]);
            List < IPokemon > Poke2 = new List<IPokemon>();
            Poke2.Add(pokedex[20]);
            Poke2.Add(pokedex[21]);
            Poke2.Add(pokedex[22]);
            Poke2.Add(pokedex[23]);
            Poke2.Add(pokedex[24]);
            Poke2.Add(pokedex[25]);
            
            Player.InitializePlayer1("example", Poke1, Poke1[1]);
            Player.InitializePlayer2("example", Poke2, Poke2[4]);
            
            IPlayer p1 = Player.Player1;
            IPlayer p2 = Player.Player2;
           p2.SelectedPokemon.State = SpecialEffect.Burn; // Doble Pokemons con un estado alterado para demostrar que se queda a 90.
           p2.Pokemons[4].State = SpecialEffect.Burn;
           Assert.That(Calculator.FuncionCalcularChances(p1,p2).Equals("P1 gano con 100 y P2 tiene 90"));
        }

        [Test]
        static public void TestFaltaItemsYPokemonMuerto()
        {

            Catalogue.CreateCatalogue();
            Dictionary<int, IPokemon> pokedex = Catalogue.GetPokedex();
            List<IPokemon> Poke1 = new List<IPokemon>();
            Poke1.Add(pokedex[1]);
            Poke1.Add(pokedex[2]);
            Poke1.Add(pokedex[4]);
            Poke1.Add(pokedex[3]);
            Poke1.Add(pokedex[15]);
            Poke1.Add(pokedex[9]);
            List<IPokemon> Poke2 = new List<IPokemon>();
            Poke2.Add(pokedex[20]);
            Poke2.Add(pokedex[21]);
            Poke2.Add(pokedex[22]);
            Poke2.Add(pokedex[23]);
            Poke2.Add(pokedex[24]);
            Poke2.Add(pokedex[25]);

            Player.InitializePlayer1("example", Poke1, Poke1[1]);
            Player.InitializePlayer2("example", Poke2, Poke2[5]);

            IPlayer p1 = Player.Player1;
            IPlayer p2 = Player.Player2;
            p1.Items.RemoveAt(0); // Elimina la primera lista de items del jugador 1.
            p2.Pokemons[0].Health = 0;
            p2.Pokemons[2].Health = 0;
            
          Assert.That(Calculator.FuncionCalcularChances(p1, p2).Equals("P1 gano con 95 y P2 tiene 80"));
        }
       
    }
    
    
    }