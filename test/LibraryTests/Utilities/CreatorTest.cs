using NUnit.Framework;

using System.Collections.Generic;
using Library.Game.Pokemons;
using Library.Game.Utilities;

namespace Library.Tests
{
    [TestFixture]
    public class CreatorTest
    {
        // Inicializa el catálogo antes de cada prueba
        private Dictionary<int, IPokemon> _pokedex = Create.CreateCatalogue();

        [SetUp]
        public void Setup()
        {
            // Aquí se pueden agregar inicializaciones si fueran necesarias
        }

        [TearDown]
        public void TearDown()
        {
            // Limpieza: Reinicia los valores si es necesario
        }

        [Test]
        public void CreateCatalogue_ShouldInitializePokedex()
        {
            // Assert: Verifica que el catálogo no sea nulo y tenga Pokémon
            Assert.That(_pokedex, Is.Not.Null, "The Pokedex should not be null.");
            Assert.That(_pokedex.Count, Is.EqualTo(20), "The Pokedex should contain exactly 20 Pokémon.");
        }

        [Test]
        public void GetPokedex_ShouldReturnNonNullableDictionary()
        {
            // Assert
            Assert.That(_pokedex, Is.Not.Null, "The Pokedex returned by GetPokedex should not be null.");
            Assert.That(_pokedex.Count, Is.EqualTo(20), "The Pokedex returned by GetPokedex should contain exactly 20 Pokémon.");
        }

        [Test]
        public void GetPokemon_ShouldReturnCorrectPokemon()
        {
            // Act & Assert: Verifica que todos los Pokémon estén presentes con su nombre y tipo correcto

            // Lista de ID y sus valores esperados
            var expectedPokemons = new Dictionary<int, (string Name, string Type)>
            {
                { 1, ("Venusaur", "Plant") },
                { 2, ("Blastoise", "Water") },
                { 3, ("Butterfree", "Bug") },
                { 4, ("Charizard", "Fire") },
                { 5, ("Pikachu", "Electric") },
                { 6, ("Gengar", "Ghost") },
                { 7, ("Articuno", "Ice") },
                { 8, ("Machamp", "Fight") },
                { 9, ("Snorlax", "Normal") },
                { 10, ("Alakazam", "Psychic") },
                { 11, ("Onix", "Rock") },
                { 12, ("Golem", "Earth") },
                { 13, ("Nidoking", "Venom") },
                { 14, ("Pidgeot", "Flying") },
                { 15, ("Dragonite", "Dragon") },
                { 16, ("Vaporeon", "Water") },
                { 17, ("Jolteon", "Electric") },
                { 18, ("Flareon", "Fire") },
                { 19, ("Kabutops", "Rock") },
                { 20, ("Aerodactyl", "Flying") }
            };

            // Comprobamos si cada Pokémon en la lista esperada existe en el Pokedex y tiene los valores correctos
            foreach (var expected in expectedPokemons)
            {
                var pokemon = _pokedex[expected.Key];
                Assert.That(pokemon, Is.Not.Null, $"Pokémon with ID {expected.Key} should not be null.");
                Assert.That(pokemon?.Name, Is.EqualTo(expected.Value.Name), $"Pokémon with ID {expected.Key} should be {expected.Value.Name}.");
                Assert.That(pokemon?.Type, Is.EqualTo(expected.Value.Type), $"Pokémon with ID {expected.Key} should have type {expected.Value.Type}.");
            }
        }

        [Test]
        public void GetPokemon_ShouldThrowExceptionForInvalidId()
        {
            // Act & Assert: Verifica que una ID inválida lance una excepción
            Assert.Throws<KeyNotFoundException>(() => Catalogue.GetPokemon(999), "Accessing a non-existing Pokémon ID should throw KeyNotFoundException.");
        }
    }
}