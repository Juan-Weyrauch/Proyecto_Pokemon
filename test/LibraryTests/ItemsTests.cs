using NUnit.Framework;
using Library.Game.Items;

namespace Library.Tests
{
    /// <summary>
    /// This class tests the behavior and properties of item objects like TotalCure, SuperPotion, and RevivePotion.
    /// </summary>
    [TestFixture]
    public class ItemsTests
    {
        /// <summary>
        /// Instance of TotalCure used in tests.
        /// </summary>
        private TotalCure totalCure;

        /// <summary>
        /// Instance of SuperPotion used in tests.
        /// </summary>
        private SuperPotion superPotion;

        /// <summary>
        /// Instance of RevivePotion used in tests.
        /// </summary>
        private RevivePotion revivePotion;

        /// <summary>
        /// This method sets up the test environment by initializing instances of TotalCure, SuperPotion, and RevivePotion.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            totalCure = new TotalCure();
            superPotion = new SuperPotion();
            revivePotion = new RevivePotion();
        }

        /// <summary>
        /// Tests if TotalCure has the correct Name and RegenValue properties.
        /// </summary>
        [Test]
        public void TotalCureShouldHaveCorrectProperties()
        {
            // Assert: Verificamos las propiedades de TotalCure
            Assert.That(totalCure.Name, Is.EqualTo("Total Cure"));
            Assert.That(totalCure.RegenValue, Is.EqualTo(0));
        }

        /// <summary>
        /// Tests if SuperPotion has the correct Name and RegenValue properties.
        /// </summary>
        [Test]
        public void SuperPotionShouldHaveCorrectProperties()
        {
            // Assert: Verificamos las propiedades de SuperPotion
            Assert.That(superPotion.Name, Is.EqualTo("Super Potion"));
            Assert.That(superPotion.RegenValue, Is.EqualTo(70));
        }

        /// <summary>
        /// Tests if RevivePotion has the correct Name and RegenValue properties.
        /// </summary>
        [Test]
        public void RevivePotionShouldHaveCorrectProperties()
        {
            // Assert: Verificamos las propiedades de RevivePotion
            Assert.That(revivePotion.Name, Is.EqualTo("Revive Potion"));
            Assert.That(revivePotion.RegenValue, Is.EqualTo(50));
        }
    }
}
