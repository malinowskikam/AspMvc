using NUnit.Framework;
using AspMvc.Models;
using System;
using System.Collections.Generic;
using System.Text;

using static AspTests.Utils;

namespace AspTests.ModelTest
{
    class ManufacturerTest
    {
        Manufacturer manufacturer;

        [SetUp]
        public void Setup()
        {
            manufacturer = new Manufacturer();

            manufacturer.Id = 1;
            manufacturer.Name = "Producent";
            manufacturer.Rating = 4.59;
            manufacturer.CreationDate = new DateTime(2017, 01, 21);
        }

        [Test]
        public void InvalidNameLength()
        {
            manufacturer.Name = "a";

            var validationResult = ValidateModel(manufacturer);

            var expectedErrorMessage = "Pole Nazwa musi być ciągiem o długości minimalnej 2 i maksymalnej 50.";

            Assert.AreEqual(1, validationResult.Count);
            Assert.AreEqual(expectedErrorMessage, validationResult[0].ErrorMessage);
        }

        [Test]
        public void NullName()
        {
            manufacturer.Name = null;

            var validationResult = ValidateModel(manufacturer);

            var expectedErrorMessage = "Pole Nazwa jest wymagane.";

            Assert.AreEqual(1, validationResult.Count);
            Assert.AreEqual(expectedErrorMessage, validationResult[0].ErrorMessage);
        }

        [Test]
        public void ValidManufacturer()
        {
            Assert.AreEqual(0, ValidateModel(manufacturer).Count);
        }

        [Test]
        public void RatingTooBig()
        {
            manufacturer.Rating = 14.6;

            var validationResult = ValidateModel(manufacturer);

            var expectedErrorMessage = "Wartość musi być pomiędzy 1 a 5";

            Assert.AreEqual(1, validationResult.Count);
            Assert.AreEqual(expectedErrorMessage, validationResult[0].ErrorMessage);
        }

        [Test]
        public void RatingTooLow()
        {
            manufacturer.Rating = 0.7;

            var validationResult = ValidateModel(manufacturer);

            var expectedErrorMessage = "Wartość musi być pomiędzy 1 a 5";

            Assert.AreEqual(1, validationResult.Count);
            Assert.AreEqual(expectedErrorMessage, validationResult[0].ErrorMessage);
        }
    }
}
