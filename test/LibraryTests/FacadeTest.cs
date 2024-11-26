using Library.Game.Pokemons;
using Library.Game.Players;
using Library.Game.Utilities;
using NUnit.Framework;
using Library.Game.Attacks;
using System.Collections.Generic;

namespace Library.Tests
{
    [TestFixture]
    public class FacadeTests
    {
        private IPokemon _bulbasaur;
        private IPokemon _charmander;

        [SetUp]
        public void Setup()
        {
            // Crear Pokémon de prueba
            _bulbasaur = new Pokemon("Bulbasaur", 15, "Plant", AttackGenerator.GenerateRandomAttack("Plant"));
            _charmander = new Pokemon("Charmander", 20, "Fire", AttackGenerator.GenerateRandomAttack("Fire"));

            // Crear el catálogo de Pokémon
            Catalogue.CreateCatalogue();
        }

        [Test]
        public void ManualInitialization_ShouldSetPlayersCorrectly()
        {
            // Inicialización manual
            Player.InitializePlayer1("Ash", new List<IPokemon>(), null);
            Player.InitializePlayer2("Misty", new List<IPokemon>(), null);

            // Verificaciones
            Assert.That(Player.Player1.Name, Is.EqualTo("Ash"), "Player1 name should be 'Ash'.");
            Assert.That(Player.Player2.Name, Is.EqualTo("Misty"), "Player2 name should be 'Misty'.");
        }

        [Test]
        public void CreateCatalogue_ShouldPopulateCatalogueWith20Pokemon()
        {
            // Act
            Catalogue.CreateCatalogue();
            var pokedex = Catalogue.GetPokedex();

            // Assert
            Assert.That(pokedex.Count, Is.EqualTo(20), "The catalogue should contain 20 Pokémon.");
        }

        [Test]
        public void Catalogue_ShouldContainVenusaurAfterCreation()
        {
            // Act
            Catalogue.CreateCatalogue();
            var pokedex = Catalogue.GetPokedex();

            // Assert
            Assert.That(pokedex[1].Name, Is.EqualTo("Venusaur"), "The first Pokémon in the catalogue should be Venusaur.");
        }
    }
}