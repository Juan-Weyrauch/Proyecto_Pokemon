using NUnit.Framework;
using System.Collections.Generic;
using Library.Game.Utilities; // Asegúrate de que este es el espacio de nombres correcto
using Library.Game.Pokemons;

namespace Library.Tests
{
    [TestFixture]
    public class CreateTests
    {
        private Dictionary<int, IPokemon> _pokedex;

        [SetUp]
        public void Setup()
        {
            // Configuración previa: Llama al método que deseas probar
            _pokedex = Create.CreateCatalogue();
        }

        [TearDown]
        public void TearDown()
        {
            // Limpieza: Si necesitas reiniciar valores estáticos o estados
            _pokedex = null!;
        }

        [Test]
        public void CreateCatalogue_ShouldReturnNonNullablePokedex()
        {
            // Assert: Verifica que el resultado no sea nulo
            Assert.That(_pokedex, Is.Not.Null, "The Pokedex should not be null.");
        }

        [Test]
        public void CreateCatalogue_ShouldContain20Pokemon()
        {
            // Assert: Verifica la cantidad de Pokémon en el Pokedex
            Assert.That(_pokedex.Count, Is.EqualTo(20), "The Pokedex should contain exactly 20 Pokémon.");
        }

        [Test]
        public void CreateCatalogue_ShouldContainVenusaur()
        {
            // Assert: Verifica que Venusaur está correctamente definido
            Assert.That(_pokedex.ContainsKey(1), Is.True, "The Pokedex should contain Pokémon with ID 1.");
            Assert.That(_pokedex[1].Name, Is.EqualTo("Venusaur"), "Pokémon ID 1 should be Venusaur.");
            Assert.That(_pokedex[1].Type, Is.EqualTo("Plant"), "Pokémon ID 1 should have type Plant.");
        }

        [Test]
        public void CreateCatalogue_ShouldContainCharizard()
        {
            // Assert: Verifica que Charizard está correctamente definido
            Assert.That(_pokedex.ContainsKey(4), Is.True, "The Pokedex should contain Pokémon with ID 4.");
            Assert.That(_pokedex[4].Name, Is.EqualTo("Charizard"), "Pokémon ID 4 should be Charizard.");
            Assert.That(_pokedex[4].Type, Is.EqualTo("Fire"), "Pokémon ID 4 should have type Fire.");
        }

        [Test]
        public void CreateCatalogue_ShouldContainPikachu()
        {
            // Assert: Verifica que Pikachu está correctamente definido
            Assert.That(_pokedex.ContainsKey(5), Is.True, "The Pokedex should contain Pokémon with ID 5.");
            Assert.That(_pokedex[5].Name, Is.EqualTo("Pikachu"), "Pokémon ID 5 should be Pikachu.");
            Assert.That(_pokedex[5].Type, Is.EqualTo("Electric"), "Pokémon ID 5 should have type Electric.");
        }
    }
}