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

namespace ProductServiceTest
{
    class EmployeeRepositoryTests
    {
        [Test]
        public void EmployeeRepo_Add()
        {
            var mockDbSet = new Mock<DbSet<Employee>>();
            var mockContext = new Mock<YOBAContext>();

            mockContext.Setup(c => c.Employees).Returns(mockDbSet.Object);
            var res = new EmployeeRepository(mockContext.Object);

            res.Add(new Employee() { EmployeeId= 1, Name="Nikola", LastName="Landao", Sallery=1200, TelephoneNumber="88000123"  });
            res.Add(new Employee() { EmployeeId= 2, Name="Khal", LastName="Drogo", Sallery=20 });

            mockContext.Verify(s => s.Add(It.IsAny<Employee>()), Times.Exactly(2));
            mockContext.Verify(s => s.SaveChanges(), Times.Exactly(2));
        }

        [Test]
        public void EmployeeRepo_GetByID()
        {
            var data = new List<Employee>() {
                new Employee() { EmployeeId=1, Name="Nikola", LastName="Landao", Sallery=1200, TelephoneNumber="88000123" },
                new Employee() { EmployeeId=2, Name="Khal", LastName="Drogo", Sallery=20}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Employee>>();
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Employees).Returns(mockDbSet.Object);

            var repo = new EmployeeRepository(context.Object);
            var result = repo.GetById(2);

            Assert.IsTrue(result.Name == "Khal");
        }

        [Test]
        public void EmployeeRepo_GetAll()
        {
            var data = new List<Employee>() {
                new Employee() {EmployeeId=1, Name="Nikola", LastName="Landao", Sallery=1200, TelephoneNumber="88000123" },
                new Employee() {EmployeeId=2, Name="Khal", LastName="Drogo", Sallery=20},
                new Employee() {EmployeeId=5, Name="John", LastName="Snow", Sallery=500},
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Employee>>();
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Employees).Returns(mockDbSet.Object);

            var repo = new EmployeeRepository(context.Object);
            var result = repo.GetAll().ToList();

            result.Should().AllBeOfType(typeof(Employee));
            result.Should().HaveCount(3);
            result.Should().Contain(x => x.Name == "Khal");
        }

        [Test]
        public void EmployeeRepo_Delete()
        {
            var data = new List<Employee>() {
                new Employee() {EmployeeId=1, Name="Nikola", LastName="Landao", Sallery=1200, TelephoneNumber="88000123" },
                new Employee() {EmployeeId=2, Name="Khal", LastName="Drogo", Sallery=20},
                new Employee() {EmployeeId=5, Name="John", LastName="Snow", Sallery=500}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Employee>>();
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Employees).Returns(mockDbSet.Object);

            var repo = new EmployeeRepository(context.Object);

            repo.Delete(new Employee()
            {
                EmployeeId = 5,
                Name = "John",
                LastName = "Snow",
                Sallery = 500
            });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }

        [Test]
        public void EmployeeRepo_Update()
        { 
            var data = new List<Employee>() {
                new Employee() {EmployeeId=1, Name="Nikola", LastName="Landao", Sallery=1200, TelephoneNumber="88000123" },
                new Employee() {EmployeeId=2, Name="Khal", LastName="Drogo", Sallery=20},
                new Employee() {EmployeeId=5, Name="John", LastName="Snow", Sallery=500}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Employee>>();
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Employee>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Employees).Returns(mockDbSet.Object);

            var repo = new EmployeeRepository(context.Object);
            repo.Change(new Employee() { EmployeeId = 5, Name = "John", LastName = "Snow", Sallery = 500 });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }
    }
}
