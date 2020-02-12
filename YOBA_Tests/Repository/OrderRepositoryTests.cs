using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL;
using YOBA_LibraryData.BLL.UOF.Repository;
using FluentAssertions;
using YOBA_LibraryData.BLL.Entities.Sell;
using YOBA_LibraryData.DAL;

namespace ProductServiceTest
{
    class OrderRepositoryTests
    {
        [Test]
        public void OrderRepo_Add()
        {
            var mockDbSet = new Mock<DbSet<Order>>();
            var mockContext = new Mock<YOBAContext>();

            mockContext.Setup(c => c.Orders).Returns(mockDbSet.Object);
            var res = new OrderRepository(mockContext.Object);

            res.Add(new Order() { Id= "1", Paid=true, Shipped=true, OrderSum=100});
            res.Add(new Order() { Id= "3", Paid = false, Shipped = false, OrderSum = 300 });

            mockContext.Verify(s => s.Add(It.IsAny<Order>()), Times.AtLeast(2));
            mockContext.Verify(s => s.SaveChanges(), Times.AtLeast(2));
        }

        [Test]
        public void PaymentRepo_GetByID()
        {
            var data = new List<Order>()
            {
                new Order { Id="1", Paid=true, Shipped=true, OrderSum=100 },
                new Order { Id="3", Paid=false, Shipped=true, OrderSum=600 },
                new Order { Id="5", Paid=true, Shipped=false, OrderSum=1100}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Order>>();
            mockDbSet.As<IQueryable<Order>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Order>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Order>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Order>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Orders).Returns(mockDbSet.Object);

            var repo = new OrderRepository(context.Object);
            var result = repo.GetById("5");

            Assert.IsTrue(result.Paid == true);
        }

        [Test]
        public void PaymentRepo_GetByIdentity()
        {
            var data = new List<Order>()
            {
                new Order { Id="1", Paid=true, Shipped=true, OrderSum=100, OrderIdentity="T5-19-00001" },
                new Order { Id="3", Paid=false, Shipped=true, OrderSum=600, OrderIdentity="T8-18-04234"},
                new Order { Id="5", Paid=true, Shipped=false, OrderSum=1100, OrderIdentity="T12-19-00021"}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Order>>();
            mockDbSet.As<IQueryable<Order>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Order>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Order>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Order>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Orders).Returns(mockDbSet.Object);

            var repo = new OrderRepository(context.Object);
            var result = repo.GetByIdentity("T12-19-00021");

            Assert.IsTrue(result.Paid == true);
        }

        [Test]
        public void PaymentRepo_GetAll()
        {
            var data = new List<Order>()
            {
                new Order { Id="1", Paid=true, Shipped=true, OrderSum=100 },
                new Order { Id="3", Paid=false, Shipped=true, OrderSum=600 },
                new Order { Id="5", Paid=true, Shipped=false, OrderSum=1100}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Order>>();
            mockDbSet.As<IQueryable<Order>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Order>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Order>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Order>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Orders).Returns(mockDbSet.Object);

            var repo = new OrderRepository(context.Object);
            var result = repo.GetAll().ToList();

            result.Should().AllBeOfType(typeof(Order));
            result.Should().HaveCount(3);
            result.Should().Contain(x => x.OrderSum == 1100);
        }

        [Test]
        public void PaymentRepo_Delete()
        {
            var data = new List<Order>()
            {
                new Order { Id="1", Paid=true, Shipped=true, OrderSum=100 },
                new Order { Id="3", Paid=false, Shipped=true, OrderSum=600 },
                new Order { Id="5", Paid=true, Shipped=false, OrderSum=1100}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Order>>();
            mockDbSet.As<IQueryable<Order>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Order>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Order>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Order>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Orders).Returns(mockDbSet.Object);

            var repo = new OrderRepository(context.Object);
            repo.Delete(new Order() { Id = "5", Paid = true, Shipped = false, OrderSum = 1100 });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }

        [Test]
        public void PaymentRepo_Update()
        {
            var data = new List<Order>()
            {
                new Order { Id="1", Paid=true, Shipped=true, OrderSum=100 },
                new Order { Id="3", Paid=false, Shipped=true, OrderSum=600 },
                new Order { Id="5", Paid=true, Shipped=false, OrderSum=1100}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Order>>();
            mockDbSet.As<IQueryable<Order>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Order>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Order>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Order>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Orders).Returns(mockDbSet.Object);

            var repo = new OrderRepository(context.Object);
            repo.Change(new Order() { Id = "5", Paid = true, Shipped = false, OrderSum = 1100 });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }
    }
}
