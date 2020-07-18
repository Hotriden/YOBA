using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL.UOF.Repository;
using FluentAssertions;
using YOBA_LibraryData.BLL.Entities.Sell;
using YOBA_LibraryData.DAL;
using System.Threading.Tasks;

namespace ProductServiceTest
{
    public class CustomerRepositoryTests
    {
        [Test]
        public async Task CustomerRepo_Add()
        {
            var mockDbSet = new Mock<DbSet<Customer>>();
            var mockContext = new Mock<YOBAContext>();

            mockContext.Setup(c => c.Customers).Returns(mockDbSet.Object);
            var res = new CustomerRepository(mockContext.Object);

            await res.Add(new Customer() { Address="Lincoln st. 9", CustomerEmail="no@gmail.com", Id=1, CustomerLastName="Peterson", CustomerName="Bob", TelephoneNumber="123123" });

            mockContext.Verify(s => s.Add(It.IsAny<Customer>()), Times.Once());
            mockContext.Verify(s => s.SaveChanges(), Times.Once());
        }

        [Test]
        public void CustomerRepo_GetByID()
        {
            var data = new List<Customer>() {
                new Customer() { Address="Lincoln st. 9", CustomerEmail="noway@gmail.com", Id=20, CustomerLastName="Dylan", CustomerName="Silvestry", TelephoneNumber="888555000"},
                new Customer() { Address="Lincoln st. 12", CustomerEmail="no@gmail.com", Id=25, CustomerLastName="Peterson", CustomerName="Bob", TelephoneNumber="380088713"}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Customer>>();
            mockDbSet.As<IQueryable<Customer>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Customer>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Customer>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Customer>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Customers).Returns(mockDbSet.Object);

            var repo = new CustomerRepository(context.Object);
            var result = repo.GetById(20);

            Assert.IsTrue(result.Address == "Lincoln st. 9");
        }

        [Test]
        public void CustomerRepo_GetAll()
        {
            var data = new List<Customer>() {
                new Customer() { Address="Lincoln st. 9", CustomerEmail="noway@gmail.com", Id=20, CustomerLastName="Silvestry", CustomerName="Dylan", TelephoneNumber="888555000"},
                new Customer() { Address="Lincoln st. 12", CustomerEmail="no@gmail.com", Id=20, CustomerLastName="Peterson", CustomerName="Bob", TelephoneNumber="380088713"}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Customer>>();
            mockDbSet.As<IQueryable<Customer>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Customer>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Customer>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Customer>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(c => c.Customers).Returns(mockDbSet.Object);

            var repo = new CustomerRepository(context.Object);
            var result = repo.GetAll("20").ToList();

            result.Should().AllBeOfType(typeof(Customer));
            result.Should().HaveCount(2);
            result.Should().Contain(x => x.CustomerName == "Dylan");
        }

        [Test]
        public async Task CustomerRepo_Delete()
        {
            var data = new List<Customer>() {
                new Customer() { Address="Lincoln st. 9", CustomerEmail="noway@gmail.com", Id=20, CustomerLastName="Silvestry", CustomerName="Dylan", TelephoneNumber="888555000"},
                new Customer() { Address="Lincoln st. 12", CustomerEmail="no@gmail.com", Id=25, CustomerLastName="Peterson", CustomerName="Bob", TelephoneNumber="380088713"}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Customer>>();
            mockDbSet.As<IQueryable<Customer>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Customer>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Customer>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Customer>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(c => c.Customers).Returns(mockDbSet.Object);
            var repo = new CustomerRepository(context.Object);
            await repo.Delete(new Customer() { Address = "Lincoln st. 9", CustomerEmail = "noway@gmail.com", Id = 20, CustomerLastName = "Silvestry", CustomerName = "Dylan", TelephoneNumber = "888555000" });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }

        [Test]
        public async Task CustomerRepo_Update()
        {
            var data = new List<Customer>() {
                new Customer() { Address="Lincoln st. 9", CustomerEmail="noway@gmail.com", Id=20, CustomerLastName="Silvestry", CustomerName="Dylan", TelephoneNumber="888555000"},
                new Customer() { Address="Lincoln st. 12", CustomerEmail="no@gmail.com", Id=25, CustomerLastName="Peterson", CustomerName="Bob", TelephoneNumber="380088713"}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Customer>>();
            mockDbSet.As<IQueryable<Customer>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Customer>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Customer>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Customer>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(c => c.Customers).Returns(mockDbSet.Object);
            var repo = new CustomerRepository(context.Object);
            await repo.Change(new Customer() { Address = "Lincoln st. 12", CustomerEmail = "no@gmail.com", Id = 25, CustomerLastName = "Peterson", CustomerName = "Scot", TelephoneNumber = "380088713" });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }
    }
}
