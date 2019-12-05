using AnimalCrossing;
using AnimalCrossing.Controllers;
using AnimalCrossing.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AnimalCrossingTests
{
    public class UnitTest1
    {
        [Fact]
        public void TestAddMethodWithTwoPositiveNumbers()
        {
            DataTestService testService = new DataTestService();

            int result = testService.Add(2, 5);

            Assert.Equal(7, result);
        }

        [Fact]
        public void TestIndexMethodReturnsObjects()
        {
            // Arrange
            var mockRepo = new Mock<ISpeciesRepository>(); //create mock repo
            mockRepo.Setup(repo => repo.Get())
                .Returns(DataTestService.GetTestSpecies()); //Creates test species in mock repo and sends in the hardcoded test data to the tested class
            var controller = new SpeciesController(mockRepo.Object); //Use the species controller


            // Act
            var result = controller.Index(); //We are not doing async so no await. This will run the method in question

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Expect a view to be the output of the method
            var model = Assert.IsAssignableFrom<IEnumerable<Species>>( //asserts that we get a Species back
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count()); //Asserts that we get two items back from the list from the model

        }

        [Fact]
        public void Create_ReturnsBadRequestResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var mockRepo = new Mock<ISpeciesRepository>();
            /*mockRepo.Setup(repo => repo.Get())
                .Returns(TestService.GetTestSpecies());*/
            var controller = new SpeciesController(mockRepo.Object);

            controller.ModelState.AddModelError("Name", "Required"); //Set Name field to be required. We have to create a modelstate since normal model state testing is turned off when testing. This ModelState will always be invalid, no matter the species created below
            var species = new Species() //new species is creates to send in with the create function. This model is not used to check modelState, but is used cause it is required, and to assert that we get a model back.
            {
                SpeciesId = 1,
                Name = "", //Set name to be empty and therefore invalid, not used in test. Just for show????
                Description = "Dette er en test"
            };

            // Act
            var result = controller.Create(species); //Here we run the create method and send in the invalid species created above

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Species>(
                viewResult.ViewData.Model);
            Assert.IsType<Species>(model); //If we get a view back with a model of species, The bad result was met and the test passed. The created model does not come into play whether or not the ModelState is valid
        }

        [Fact]
        public void CreatePost_SaveThroughRepository_WhenModelStateIsValid()
        {
            // Arrange
            var mockRepo = new Mock<ISpeciesRepository>();
            mockRepo.Setup(repo => repo.Save(It.IsAny<Species>())) //Sets up repo to listen to whether save has been called with species as parameter
                .Verifiable(); //An xUnit method that makes it possible to call the verifier method in the end
            var controller = new SpeciesController(mockRepo.Object);
            Species species = new Species()
            {
                Name = "Test",
                Description = "Test species"
            };

            // Act
            var result = controller.Create(species);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName); //Asserts that the controller is the same as the one we're in. we are not going to a different controller
            Assert.Equal("Index", redirectToActionResult.ActionName); //Assert that we are running the Index method in the controller
            mockRepo.Verify(); //Verifies that the Save code ran
        }

        [Fact]
        public void AddNewCatToDatabase()
        {
            //Arrange We need to get a test version of the Animal Repository

            IAnimalRepository testRepo = DataTestService.GetInMemoryRepo(); //Get test repo

            var cat = new Cat() //Create test cat
            {
                Name = "TestKitty",
                Description = "A fine test candidate"
            };

            //Act We want to run the Save method of the repository

            testRepo.Save(cat);

            //Assert We want to make sure that the repository has saved the test object

            Assert.Single(testRepo.Get()); //We expect to have one single item in our repo
            Assert.Equal(cat.Name, testRepo.Get(1).Name); //We expect the item to have the name of the test item

        }

        [Fact]
        public void CatWithSameIDOverWritesCurrentCatInDB()
        {
            //Arrange
            IAnimalRepository testrepo = DataTestService.GetInMemoryRepo();

            var cat = new Cat()
            {
                Name = "TestKitty",
                Description = "A fine test candidate"
            };

            testrepo.Save(cat);

            var sameCat = testrepo.Get(1);

            sameCat.Name = "TestKitty2";

            //Act
            testrepo.Save(sameCat);

            //Assert

            Assert.Single(testrepo.Get()); //We want there to still only be a single item in the list, since it should be overwritten
            Assert.Equal(sameCat.Name, testrepo.Get(1).Name); //We want the name to have been updated
        }

        [Fact]
        public void DeleteCatFromDatabase()
        {
            //Arrange
            IAnimalRepository testrepo = DataTestService.GetInMemoryRepo();

            var cat = new Cat()
            {
                Name = "TestKitty",
                Description = "A fine test candidate"
            };

            testrepo.Save(cat);

            //Act
            testrepo.Delete(testrepo.Get(1).CatId); //Delete the cat we added to the db

            //Assert
            Assert.Empty(testrepo.Get()); //Assert that the database collection is empty
        }

 /*       [Fact]
        public void SearchAnimal_Repository_WhenReturningOneAnimal()
        {
            // Arrange
            var mockRepo = new Mock<IAnimalRepository>();
            mockRepo.Setup(repo => repo.Get())
               .Returns(TestService.GetTestCats());
            //Act
            List<Cat> result = mockRepo.Object.find("Test1");

            //Assert



        }
        [Fact]
        public void SearchAnimal_WhenReturningNoAnimal()
        {

        }
        [Fact]
        public void SearchANimal_WhenReturningAllAnimal()
        {

        }*/
    }
}

