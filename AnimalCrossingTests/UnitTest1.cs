using System;
using System.Collections.Generic;
using System.Linq;
using AnimalCrossing;
using AnimalCrossing.Controllers;
using AnimalCrossing.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AnimalCrossingTests
{
    public class UnitTest1
    {
        [Fact]
        public void TestAddMethodWithTwoPositiveNumbers()
        {
            //Arrange - instan. classes etc.
            var testService = new TestService();

            //Act - Call the method to test
            int result = testService.Add(2, 5);

            //Assert - Check if you get the right result back
            Assert.Equal(7, result);
        }

        [Fact]
        public void TestIndexMethodReturnsObjects()
        {
            // Arrange
            var mockRepo = new Mock<ISpeciesRepository>();
            mockRepo.Setup(repo => repo.Get())
                .Returns(DataTestService.GetTestSpecies());
            var controller = new SpeciesController(mockRepo.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Species>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void CreatePost_ReturnsViewWithSpecies_WhenModelStateIsInvalid()
        {
            // Arrange
            var mockRepo = new Mock<ISpeciesRepository>();
            var controller = new SpeciesController(mockRepo.Object);

            controller.ModelState.AddModelError("Name", "Required");
            var species = new Species() { Name = "", Description = "Karl Karlson something nice" };

            // Act
            var result = controller.Create(species);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Species>(viewResult.ViewData.Model);
            Assert.IsType<Species>(model);
        }

        [Fact]
        public void CreatePost_SaveThroughRepository_WhenModelStateIsValid()
        {
            // Arrange
            var mockRepo = new Mock<ISpeciesRepository>();
            mockRepo.Setup(repo => repo.Save(It.IsAny<Species>()))
                //.Returns(Task.CompletedTask)
                .Verifiable();
            var controller = new SpeciesController(mockRepo.Object);
            Species s = new Species()
            {
                Name = "Human",
                Description = "Don't listen to scientists"
            };

            // Act
            var result = controller.Create(s);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepo.Verify();
        }
    }
}
