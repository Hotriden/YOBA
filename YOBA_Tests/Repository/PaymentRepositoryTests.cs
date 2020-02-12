﻿using Microsoft.EntityFrameworkCore;
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
    class PaymentRepositoryTests
    {
        [Test]
        public void PaymentRepo_Add()
        {
            var mockDbSet = new Mock<DbSet<Payment>>();
            var mockContext = new Mock<YOBAContext>();

            mockContext.Setup(c => c.Payments).Returns(mockDbSet.Object);
            var res = new PaymentRepository(mockContext.Object);

            res.Add(new Payment() { Id= "1", Value=200, IdentialPayNumber="H0234200502020" });
            res.Add(new Payment() { Id = "2", Value = 12200, IdentialPayNumber = "K0134506502111" });

            mockContext.Verify(s => s.Add(It.IsAny<Payment>()), Times.AtLeast(2));
            mockContext.Verify(s => s.SaveChanges(), Times.AtLeast(2));
        }

        [Test]
        public void PaymentRepo_GetByID()
        {
            var data = new List<Payment>()
            {
                new Payment { Id="1", Value=200, IdentialPayNumber="H0234200502020" },
                new Payment { Id="2", Value = 12200, IdentialPayNumber = "K0134506502111" },
                new Payment { Id="4", Value = 1001, IdentialPayNumber = "L01545265011517"}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Payment>>();
            mockDbSet.As<IQueryable<Payment>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Payment>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Payment>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Payment>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Payments).Returns(mockDbSet.Object);

            var repo = new PaymentRepository(context.Object);
            var result = repo.GetById("4");

            Assert.IsTrue(result.IdentialPayNumber == "L01545265011517");
        }

        [Test]
        public void PaymentRepo_GetByNumber()
        {
            var data = new List<Payment>()
            {
                new Payment { Id="1", Value=200, IdentialPayNumber="H0234200502020" },
                new Payment { Id="2", Value = 12200, IdentialPayNumber = "K0134506502111" },
                new Payment { Id="4", Value = 1001, IdentialPayNumber = "L01545265011517"}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Payment>>();
            mockDbSet.As<IQueryable<Payment>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Payment>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Payment>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Payment>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Payments).Returns(mockDbSet.Object);

            var repo = new PaymentRepository(context.Object);
            var result = repo.GetByIdentity("K0134506502111");

            Assert.IsTrue(result.Id == "2");
        }

        [Test]
        public void PaymentRepo_GetAll()
        {
            var data = new List<Payment>()
            {
                new Payment { Id="1", Value=200, IdentialPayNumber="H0234200502020" },
                new Payment { Id="2", Value = 12200, IdentialPayNumber = "K0134506502111" },
                new Payment { Id="4", Value = 1001, IdentialPayNumber = "L01545265011517"}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Payment>>();
            mockDbSet.As<IQueryable<Payment>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Payment>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Payment>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Payment>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Payments).Returns(mockDbSet.Object);

            var repo = new PaymentRepository(context.Object);
            var result = repo.GetAll().ToList();

            result.Should().AllBeOfType(typeof(Payment));
            result.Should().HaveCount(3);
            result.Should().Contain(x => x.IdentialPayNumber == "K0134506502111");
        }

        [Test]
        public void PaymentRepo_Delete()
        {
            var data = new List<Payment>()
            {
                new Payment { Id="1", Value=200, IdentialPayNumber="H0234200502020" },
                new Payment { Id="2", Value = 12200, IdentialPayNumber = "K0134506502111" },
                new Payment { Id="4", Value = 1001, IdentialPayNumber = "L01545265011517"}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Payment>>();
            mockDbSet.As<IQueryable<Payment>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Payment>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Payment>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Payment>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Payments).Returns(mockDbSet.Object);

            var repo = new PaymentRepository(context.Object);
            repo.Delete(new Payment() { Id = "4", Value = 1001, IdentialPayNumber = "L01545265011517" });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }

        [Test]
        public void PaymentRepo_Update()
        {
            var data = new List<Payment>()
            {
                new Payment { Id="1", Value=200, IdentialPayNumber="H0234200502020" },
                new Payment { Id="2", Value = 12200, IdentialPayNumber = "K0134506502111" },
                new Payment { Id="4", Value = 1001, IdentialPayNumber = "L01545265011517"}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Payment>>();
            mockDbSet.As<IQueryable<Payment>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Payment>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Payment>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Payment>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Payments).Returns(mockDbSet.Object);

            var repo = new PaymentRepository(context.Object);
            repo.Change(new Payment() { Id = "2", Value = 13400, IdentialPayNumber = "K0134506502111" });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }
    }
}