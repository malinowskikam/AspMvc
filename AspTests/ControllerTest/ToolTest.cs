using AspTests.FakeRepositories;
using Moq;
using NUnit.Framework;
using AspMvc.Models;
using AspMvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AspTests.ControllerTest
{
    class ToolTest
    {
        ToolsController controller;
        Mock<IRepository> repositoryMock;

        Mock<Tool> toolMock;
        
        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IRepository>();
            toolMock = new Mock<Tool>();

            controller = new ToolsController(repositoryMock.Object);
        }

        [Test]
        public void IndexList()
        {
            repositoryMock.Setup(foo => foo.Tools).Returns( (new Dictionary<long,Tool>().Values).AsQueryable());
           
            ViewResult view = controller.Index() as ViewResult;

            var model = view.Model;

            repositoryMock.VerifyAll();

            Assert.NotNull(view);
            Assert.NotNull(model);
            Console.WriteLine(model);
            Assert.True(model is IList<Tool>);
        }


        [Test]
        public void ToolDetails()
        {
            repositoryMock.Setup(foo => foo.FindToolById(1)).Returns(toolMock.Object);

            ViewResult view = controller.Details(1) as ViewResult;

            var model = view.Model;

            repositoryMock.VerifyAll();

            Assert.NotNull(view);
            Assert.NotNull(model);
            Assert.True(model is Tool);
        }
        
        [Test]
        public void DetailsOfNonExistentTool()
        {
            repositoryMock.Setup(foo => foo.FindToolById(3)).Returns((Tool)null);

            HttpStatusCodeResult code = controller.Details(3) as HttpStatusCodeResult;

            repositoryMock.VerifyAll();

            Assert.AreEqual(code.StatusCode, 404);
        }

        [Test]
        public void DetailOfNullId()
        {
            HttpStatusCodeResult code = controller.Details(null) as HttpStatusCodeResult;

            Assert.AreEqual(code.StatusCode, 400);
        }

        [Test]
        public void AddValidTool()
        {
            repositoryMock.Setup(foo => foo.Add(toolMock.Object)).Returns(toolMock.Object);

            RedirectToRouteResult redirect = controller.Create(toolMock.Object) as RedirectToRouteResult;

            repositoryMock.VerifyAll();

            Assert.NotNull(redirect);
        }

        [Test]
        public void AddInvalidTool()
        {
            controller.ModelState.AddModelError("field", "error");

            var view = controller.Create(toolMock.Object) as ViewResult;

            var model = view.Model;

            Assert.NotNull(view);
            Assert.NotNull(model);
            Assert.True(model is Tool);
        }

        [Test]
        public void DeleteExistingTool()
        {
            repositoryMock.Setup(foo => foo.FindToolById(1)).Returns(toolMock.Object);

            ViewResult view = controller.Delete(1) as ViewResult;

            repositoryMock.VerifyAll();

            Assert.NotNull(view);
        }

        [Test]
        public void DeleteNonExistentTool()
        {
            repositoryMock.Setup(foo => foo.FindToolById(3)).Returns((Tool)null);

            HttpStatusCodeResult code = controller.Delete(3) as HttpStatusCodeResult;

            repositoryMock.VerifyAll();

            Assert.AreEqual(code.StatusCode, 404);
        }

        [Test]
        public void DeleteNullId()
        {
            HttpStatusCodeResult code = controller.Delete(null) as HttpStatusCodeResult;

            Assert.AreEqual(code.StatusCode, 400);
        }

        [Test]
        public void DeleteConfirmed()
        {
            repositoryMock.Setup(foo => foo.FindToolById(3)).Returns(toolMock.Object);
            repositoryMock.Setup(foo => foo.Delete(toolMock.Object)).Returns(toolMock.Object);

            RedirectToRouteResult redirect = controller.DeleteConfirmed(3) as RedirectToRouteResult;

            repositoryMock.VerifyAll();

            Assert.NotNull(redirect);
        }
    }
}
