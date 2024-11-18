using Library.Classes;
using Library.Interfaces;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class PokemonTests
    {
        [Test]
        public void PokemonClone_CreatesIndependentInstance()
        {
            var original = new Pokemon("Pikachu", 50, "Eléctrico", new List<IAttack>());

            var clone = original.Clone();

            // Asegurar que sean diferentes instancias
            Assert.That(original, Is.Not.SameAs(clone));

            // Modificar la salud del clon no afecta al original
            clone.Health = 50;
            Assert.That(original.Health, Is.Not.EqualTo(clone.Health));
        }
    }
}