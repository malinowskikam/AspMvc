using AspTests.FakeRepositories;
using NUnit.Framework;
using AspMvc.Models;
using AspMvc.Controllers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace AspTests.ControllerTest
{
    class ManufacturerTest
    {
        ManufacturersController controller;
        IRepository repository;

        Manufacturer manufacturer;

        [SetUp]
        public void Setup()
        {
            repository = (IRepository)new DictionarySetRepository();
            Utils.PopulateWithTestData(repository);
            controller = new ManufacturersController(repository);

            manufacturer = new Manufacturer();

            manufacturer.Id = 1;
            manufacturer.Name = "Producent";
            manufacturer.Rating = 4.59;
            manufacturer.CreationDate = new DateTime(2017, 01, 21);
        }

        [Test]
        public void IndexList()
        {
            ViewResult view = controller.Index() as ViewResult;

            var model = view.Model;

            Assert.NotNull(view);
            Assert.NotNull(model);
            Assert.True(model is IList<Manufacturer>);
        }

        [Test]
        public void StatsList()
        {
            ViewResult view = controller.Stats() as ViewResult;

            var model = view.Model;

            Assert.NotNull(view);
            Assert.NotNull(model);
            Assert.True(model is IList<ManufacturerVM>);
        }

        [Test]
        public void ManufacturerDetails()
        {
            ViewResult view = controller.Details(1) as ViewResult;

            var model = view.Model;

            Assert.NotNull(view);
            Assert.NotNull(model);
            Assert.NotNull(view.ViewData["ToolCount"]);
            Assert.NotNull(view.ViewData["ToolRating"]);
            Assert.True(model is Manufacturer);
        }

        [Test]
        public void DetailsOfNonExistentManufacturer()
        {
            HttpStatusCodeResult code = controller.Details(3) as HttpStatusCodeResult;

            Assert.AreEqual(code.StatusCode, 404);
        }

        [Test]
        public void DetailOfNullId()
        {
            HttpStatusCodeResult code = controller.Details(null) as HttpStatusCodeResult;

            Assert.AreEqual(code.StatusCode, 400);
        }

        [Test]
        public void CreateManufacturerForm()
        {
            ViewResult view = controller.Create() as ViewResult;
            
            Assert.NotNull(view);
        }

        [Test]
        public void AddValidManufacturer()
        {
            manufacturer = new Manufacturer();
            manufacturer.Name = "Nowy producent narzędzi";
            manufacturer.Rating = 5.0;
            manufacturer.CreationDate = new DateTime(2018, 01, 01);

            RedirectToRouteResult redirect = controller.Create(manufacturer) as RedirectToRouteResult;
            
            Assert.NotNull(redirect);
        }

        [Test]
        public void AddInvalidManufacturer()
        {
            manufacturer = new Manufacturer();
            manufacturer.Name = "a";
            manufacturer.Rating = 50.0;
            manufacturer.CreationDate = new DateTime(2018,01,01);

            controller.ModelState.AddModelError("field", "error");

            var view = controller.Create(manufacturer) as ViewResult;

            var model = view.Model;

            Assert.NotNull(view);
            Assert.NotNull(model);
            Assert.True(model is Manufacturer);
        }

        [Test]
        public void DeleteExistingManufacturer()
        {
            ViewResult view = controller.Delete(1) as ViewResult;

            Assert.NotNull(view);
        }

        [Test]
        public void DeleteNonExistentManufacturer()
        {
            HttpStatusCodeResult code = controller.Delete(3) as HttpStatusCodeResult;

            Assert.AreEqual(code.StatusCode, 404);
        }

        [Test]
        public void DeleteNullId()
        {
            HttpStatusCodeResult code = controller.Delete(null) as HttpStatusCodeResult;

            Assert.AreEqual(code.StatusCode, 400);
        }
    }
}
