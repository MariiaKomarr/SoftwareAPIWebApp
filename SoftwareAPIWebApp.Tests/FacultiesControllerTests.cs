using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftwareAPIWebApp.Controllers;
using SoftwareAPIWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareAPIWebApp.Tests
{
    [TestClass]
    public class FacultiesControllerTests
    {
        private SoftwareAPIContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<SoftwareAPIContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            var context = new SoftwareAPIContext(options);

            // Попереднє очищення (якщо потрібно)
            context.Faculties.RemoveRange(context.Faculties);
            context.SaveChanges();

            // Додай тестові дані
            context.Faculties.Add(new Faculty { FacultyId = 1, Name = "ФІТ", Dean = "Іванов" });
            context.Faculties.Add(new Faculty { FacultyId = 2, Name = "ФЕ", Dean = "Петрова" });
            context.SaveChanges();

            return context;
        }

        [TestMethod]
        public async Task GetFaculties_ReturnsAllFaculties()
        {
            // Arrange
            var context = GetInMemoryContext();
            var controller = new FacultiesController(context);

            // Act
            var result = await controller.GetFaculties();

            // Assert
            var actionResult = result as ActionResult<IEnumerable<Faculty>>;
            var returnValue = actionResult.Value.ToList();

            Assert.AreEqual(2, returnValue.Count);
            Assert.AreEqual("ФІТ", returnValue[0].Name);
        }
    }
}