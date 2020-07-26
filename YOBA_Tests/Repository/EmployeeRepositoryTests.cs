using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL;
using YOBA_LibraryData.BLL.UOF.Repository;
using FluentAssertions;
using YOBA_LibraryData.BLL.Entities.Staff;
using YOBA_LibraryData.DAL;
using System.Threading.Tasks;

namespace ProductServiceTest
{
    class EmployeeRepositoryTests
    {
        [Test]
        public async Task EmployeeRepo_Add()
        {
            var mockDbSet = new Mock<DbSet<Employee>>();
            var mockContext = new Mock<YOBAContext>();

            mockContext.Setup(c => c.Employees).Returns(mockDbSet.Object);
            var res = new EmployeeRepository(mockContext.Object);

            await res.Add("Vasyan11", new Employee() { Id= 1, Name="Nikola", LastName="Landao", Salary=1200, TelephoneNumber="88000123", UserId = "asdasd123" });
            await res.Add("Vasyan11", new Employee() { Id= 2, Name="Khal", LastName="Drogo", Salary=20, UserId="asdasd123" });

            mockContext.Verify(s => s.Add(It.IsAny<Employee>()), Times.Exactly(2));
            mockContext.Verify(s => s.SaveChanges(), Times.Exactly(2));
        }

        [Test]
        public void EmployeeRepo_GetByID()
        {
            var data = new List<Employee>() {
                new Employee() { Id=1, Name="Nikola", LastName="Landao", Salary=1200, TelephoneNumber="88000123", UserId="sdasd123"},
                new Employee() { Id=2, Name="Khal", LastName="Drogo", Salary=20, UserId="sdasd123"}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Employee>>();
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Employees).Returns(mockDbSet.Object);

            var repo = new EmployeeRepository(context.Object);
            var result = repo.Get("Vasyan11", new Employee() { Name="Nikola" });

            Assert.IsTrue(result.Name == "Khal");
        }

        [Test]
        public void EmployeeRepo_GetAll()
        {
            var data = new List<Employee>() {
                new Employee() {Id=1, Name="Nikola", LastName="Landao", Salary=1200, TelephoneNumber="88000123", UserId="sdasd123" },
                new Employee() {Id=2, Name="Khal", LastName="Drogo", Salary=20, UserId="sdasd123"},
                new Employee() {Id=5, Name="John", LastName="Snow", Salary=500, UserId="sdasd123"},
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Employee>>();
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Employees).Returns(mockDbSet.Object);

            var repo = new EmployeeRepository(context.Object);
            var result = repo.GetAll("sdasd123").ToList();

            result.Should().AllBeOfType(typeof(Employee));
            result.Should().HaveCount(3);
            result.Should().Contain(x => x.Name == "Khal");
        }

        [Test]
        public async Task EmployeeRepo_Delete()
        {
            var data = new List<Employee>() {
                new Employee() {Id=1, Name="Nikola", LastName="Landao", Salary=1200, TelephoneNumber="88000123" },
                new Employee() {Id=2, Name="Khal", LastName="Drogo", Salary=20},
                new Employee() {Id=5, Name="John", LastName="Snow", Salary=500}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Employee>>();
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Employees).Returns(mockDbSet.Object);

            var repo = new EmployeeRepository(context.Object);

            await repo.Delete("Vasyan11", new Employee()
            {
                Id = 5,
                Name = "John",
                LastName = "Snow",
                Salary = 500
            });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }

        [Test]
        public async Task EmployeeRepo_Update()
        { 
            var data = new List<Employee>() {
                new Employee() {Id=1, Name="Nikola", LastName="Landao", Salary=1200, TelephoneNumber="88000123" },
                new Employee() {Id=2, Name="Khal", LastName="Drogo", Salary=20},
                new Employee() {Id=5, Name="John", LastName="Snow", Salary=500}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Employee>>();
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Employees).Returns(mockDbSet.Object);

            var repo = new EmployeeRepository(context.Object);
            await repo.Change("Vasyan11", new Employee() { Id = 5, Name = "John", LastName = "Snow", Salary = 500 });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }
    }
}
