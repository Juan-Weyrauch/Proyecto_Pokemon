//-----------------------------------------------------------------------------
// <copyright file="TrainTests.cs" company="Universidad Católica del Uruguay">
// Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

using ClassLibrary;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Prueba de la clase <see cref="Train"/>.
    /// </summary>
    [TestFixture]
    public class TrainTests
    {
        /// <summary>
        /// El tren para probar.
        /// </summary>
        private Train train;

        /// <summary>
        /// Crea un tren para probar.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.train = new Train();
        }

        /// <summary>
        /// Prueba que el tren arranque.
        /// </summary>
        [Test]
        public void StartTrainTest()
        {
            Assert.That(this.train, Is.Not.Null);
            this.train.StartEngines();
            Assert.That(this.train.IsEngineStarted, Is.True);
        }

        /// <summary>
        /// Prueba que el tren se detenga.
        /// </summary>
        [Test]
        public void StopTrainTest()
        {
            Assert.That(this.train, Is.Not.Null);
            this.train.StartEngines();
            this.train.StopEngines();
            Assert.That(this.train.IsEngineStarted, Is.False);
        }
    }
}
