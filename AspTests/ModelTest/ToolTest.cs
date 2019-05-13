using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using AspMvc.Models;

using static AspTests.Utils;

namespace AspTests.ModelTest
{
    class ToolTest
    {
        Tool tool;
        Manufacturer manufacturer;

        [SetUp]
        public void Setup()
        {
            manufacturer = new Manufacturer();

            manufacturer.Id = 1;
            manufacturer.Name = "Producent";
            manufacturer.Rating = 4.59;
            manufacturer.CreationDate = new DateTime(2017,01,21);

            tool = new Tool();

            tool.Id = 1;
            tool.Name = "Narzędzie";
            tool.ProductionDate = new DateTime(2018, 05, 06);
            tool.Price = 50.99;
            tool.Rating = 4.24;
            tool.ManufacturerId = 1;
            tool.Manufacturer = manufacturer;
        }

        [Test]
        public void ValidTool()
        {
            Assert.AreEqual(0, ValidateModel(tool).Count);
        }

        [Test]
        public void InvalidNameLength()
        {
            tool.Name = "a";

            var validationResult = ValidateModel(tool);

            var expectedErrorMessage = "Pole Nazwa musi być ciągiem o długości minimalnej 2 i maksymalnej 50.";

            Assert.AreEqual(1, validationResult.Count);
            Assert.AreEqual(expectedErrorMessage, validationResult[0].ErrorMessage);
        }

        [Test]
        public void NullName()
        {
            tool.Name = null; 

            var validationResult = ValidateModel(tool);

            var expectedErrorMessage = "Pole Nazwa jest wymagane.";

            Assert.AreEqual(1, validationResult.Count);
            Assert.AreEqual(expectedErrorMessage, validationResult[0].ErrorMessage);
        }

        [Test]
        public void ZeroPrice()
        {
            tool.Price = 0.0;

            var validationResult = ValidateModel(tool);

            var expectedErrorMessage = "Wartość musi być dodatnia";

            Assert.AreEqual(1, validationResult.Count);
            Assert.AreEqual(expectedErrorMessage, validationResult[0].ErrorMessage);
        }

        [Test]
        public void NegativePrice()
        {
            tool.Price = -5.12;

            var validationResult = ValidateModel(tool);

            var expectedErrorMessage = "Wartość musi być dodatnia";

            Assert.AreEqual(1, validationResult.Count);
            Assert.AreEqual(expectedErrorMessage, validationResult[0].ErrorMessage);
        }

        [Test]
        public void RatingTooBig()
        {
            tool.Rating = 14.6;

            var validationResult = ValidateModel(tool);

            var expectedErrorMessage = "Wartość musi być pomiędzy 1 a 5";

            Assert.AreEqual(1, validationResult.Count);
            Assert.AreEqual(expectedErrorMessage, validationResult[0].ErrorMessage);
        }

        [Test]
        public void RatingTooLow()
        {
            tool.Rating = 0.7;

            var validationResult = ValidateModel(tool);

            var expectedErrorMessage = "Wartość musi być pomiędzy 1 a 5";

            Assert.AreEqual(1, validationResult.Count);
            Assert.AreEqual(expectedErrorMessage, validationResult[0].ErrorMessage);
        }

    }
}