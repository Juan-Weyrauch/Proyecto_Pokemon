using Library.Game.Items;
using Library.Game.Pokemons;
using NUnit.Framework;
using Library.Tests;

namespace LibraryTests;


    [TestFixture]
    public class SPTestProbabilidadDeGanar
    {
        
        private IPokemon _mockPokemon1;
        private IPokemon _mockPokemon2;

        [SetUp]
        public void Setup()
        {
            _mockPokemon1 = new MockPokemon("Venasaur")
            {
                State = 4
            };
            _mockPokemon2 = new MockPokemon("Charizard");
            
        }
        
        
        
        [Test]
        public void SumaDePuntos()
        {
            var superPotion = new SuperPotion();
            var items = new List<Item> {superPotion};
            var mockPlayer = new MockPlayer()
            {
                Pokemons = { _mockPokemon1,_mockPokemon2 },
                Items = { items }
            };
            var mockRival = new MockPlayer()
            {
                Pokemons = { _mockPokemon2 }
            };

            ProbabilidadDeGanar(mockPlayer, mockRival);
            
            Assert.That(ProbabilidadDeGanar(mockPlayer,mockRival), Is.True);

        }
    }