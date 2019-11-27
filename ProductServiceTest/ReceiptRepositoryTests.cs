using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL;
using YOBA_LibraryData.BLL.UOF.Repository;
using FluentAssertions;
using YOBA_LibraryData.BLL.Entities.Staff;
using YOBA_LibraryData.BLL.Entities.Supply;

namespace ProductServiceTest
{
    class ReceiptRepositoryTests
    {
        [Test]
        public void ReceiptRepo_Add()
        {
            var mockDbSet = new Mock<DbSet<Receipt>>();
            var mockContext = new Mock<YOBAContext>();

            mockContext.Setup(c => c.Receipts).Returns(mockDbSet.Object);
            var res = new ReceiptRepository(mockContext.Object);

            res.Add(new Receipt() { ReceiptId=1, Shipped=false, ReceiptSum=100, Paid=true, OrderTime=new System.DateTime(2019, 6,12) });
            res.Add(new Receipt() { ReceiptId = 2, Shipped = true, ReceiptSum = 12100, Paid = true, OrderTime = new System.DateTime(2018, 2, 4) });

            mockContext.Verify(s => s.Add(It.IsAny<Receipt>()), Times.Exactly(2));
            mockContext.Verify(s => s.SaveChanges(), Times.Exactly(2));
        }

        [Test]
        public void ReceiptRepo_GetByID()
        {
            var data = new List<Receipt>() {
                new Receipt() { ReceiptId=1, Shipped=false, ReceiptSum=100, Paid=true, OrderTime=new System.DateTime(2019, 6, 12) },
                new Receipt() { ReceiptId=2, Shipped=true, ReceiptSum=12100, Paid=true, OrderTime=new System.DateTime(2018, 2, 4) }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Receipt>>();
            mockDbSet.As<IQueryable<Receipt>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Receipt>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Receipt>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Receipt>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Receipts).Returns(mockDbSet.Object);

            var repo = new ReceiptRepository(context.Object);
            var result = repo.GetById(2);

            Assert.IsTrue(result.ReceiptSum == 12100);
        }

        [Test]
        public void ReceiptRepo_GetAll()
        {
            var data = new List<Receipt>() {
                new Receipt() { ReceiptId=1, Shipped=false, ReceiptSum=100, Paid=true, OrderTime=new System.DateTime(2019, 6, 12) },
                new Receipt() { ReceiptId=2, Shipped=true, ReceiptSum=12100, Paid=true, OrderTime=new System.DateTime(2018, 2, 4) }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Receipt>>();
            mockDbSet.As<IQueryable<Receipt>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Receipt>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Receipt>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Receipt>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Receipts).Returns(mockDbSet.Object);

            var repo = new ReceiptRepository(context.Object);
            var result = repo.GetAll().ToList();

            result.Should().AllBeOfType(typeof(Receipt));
            result.Should().HaveCount(2);
            result.Should().Contain(x => x.OrderTime == new System.DateTime(2018,2,4));
        }

        [Test]
        public void ReceiptRepo_Delete()
        {
            var data = new List<Receipt>() {
                new Receipt() { ReceiptId=1, Shipped=false, ReceiptSum=100, Paid=true, OrderTime=new System.DateTime(2019, 6, 12) },
                new Receipt() { ReceiptId=2, Shipped=true, ReceiptSum=12100, Paid=true, OrderTime=new System.DateTime(2018, 2, 4) }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Receipt>>();
            mockDbSet.As<IQueryable<Receipt>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Receipt>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Receipt>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Receipt>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Receipts).Returns(mockDbSet.Object);

            var repo = new ReceiptRepository(context.Object);

            repo.Delete(new Receipt()
            {
                ReceiptId = 1,
                Shipped = false,
                ReceiptSum = 100,
                Paid = true,
                OrderTime = new System.DateTime(2019, 6, 12)
            });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }

        [Test]
        public void ReceiptRepo_Update()
        {
            var data = new List<Receipt>() {
                new Receipt() { ReceiptId=1, Shipped=false, ReceiptSum=100, Paid=true, OrderTime=new System.DateTime(2019, 6, 12) },
                new Receipt() { ReceiptId=2, Shipped=true, ReceiptSum=12100, Paid=true, OrderTime=new System.DateTime(2018, 2, 4) }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Receipt>>();
            mockDbSet.As<IQueryable<Receipt>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Receipt>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Receipt>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Receipt>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Receipts).Returns(mockDbSet.Object);

            var repo = new ReceiptRepository(context.Object);
            repo.Change(new Receipt() { ReceiptId = 2, Shipped = true, ReceiptSum = 12100, Paid = true, OrderTime = new System.DateTime(2018, 2, 4) });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }
    }
}
