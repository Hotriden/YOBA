using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL;
using YOBA_LibraryData.BLL.UOF.Repository;
using FluentAssertions;
using YOBA_LibraryData.BLL.Entities.Finance;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.DAL;

namespace ProductServiceTest
{
    [TestFixture]
    public class SupplierRepositoryTests
    {
        [Test]
        public void SupplierRepo_Add()
        {
            var mockDbSet = new Mock<DbSet<Supplier>>();
            var mockContext = new Mock<YOBAContext>();

            mockContext.Setup(c => c.Suppliers).Returns(mockDbSet.Object);
            var res = new SupplierRepository(mockContext.Object);

            res.Add(new Supplier() { Address="Washington st 12", SupplierId= "1", SupplierName="Bington LTD" });

            mockContext.Verify(s => s.Add(It.IsAny<Supplier>()), Times.Once());
            mockContext.Verify(s => s.SaveChanges(), Times.Once());
        }

        [Test]
        public void SupplierRepo_GetByID()
        {
            var data = new List<Supplier>() {
                new Supplier() { Address="Washington st 12", SupplierId="1", SupplierName="Bington LTD"},
                new Supplier() { Address="Clinton ave 12", SupplierId="2", SupplierName="WashStore LTD"},
                new Supplier() { Address="Gabboni st 144", SupplierId="11", SupplierName="DCH LTD"},
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Supplier>>();
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Suppliers).Returns(mockDbSet.Object);

            var repo = new SupplierRepository(context.Object);
            var result = repo.GetById("11");

            Assert.IsTrue(result.SupplierName == "DCH LTD");
        }

        [Test]
        public void SupplierRepo_GetAll()
        {
            var data = new List<Supplier>() {
                new Supplier() { Address="Washington st 12", SupplierId="1", SupplierName="Bington LTD"},
                new Supplier() { Address="Clinton ave 12", SupplierId="2", SupplierName="WashStore LTD"},
                new Supplier() { Address="Gabboni st 144", SupplierId="11", SupplierName="DCH LTD"},
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Supplier>>();
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Suppliers).Returns(mockDbSet.Object);

            var repo = new SupplierRepository(context.Object);
            var result = repo.GetAll().ToList();

            result.Should().AllBeOfType(typeof(Supplier));
            result.Should().HaveCount(3);
            result.Should().Contain(x => x.SupplierName == "WashStore LTD");
        }

        [Test]
        public void SupplierRepo_Delete()
        {
            var data = new List<Supplier>() {
                new Supplier() { Address="Washington st 12", SupplierId="1", SupplierName="Bington LTD"},
                new Supplier() { Address="Clinton ave 12", SupplierId="2", SupplierName="WashStore LTD"},
                new Supplier() { Address="Gabboni st 144", SupplierId="11", SupplierName="DCH LTD"},
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Supplier>>();
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Suppliers).Returns(mockDbSet.Object);

            var repo = new SupplierRepository(context.Object);
            repo.Delete(new Supplier()
            {
                SupplierId= "11",
                Address = "Gabboni st 144",
                SupplierName = "DCH LTD"
            });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }

        [Test]
        public void SupplierRepo_Update()
        {
            var data = new List<Supplier>() {
                new Supplier() { Address="Washington st 12", SupplierId="1", SupplierName="Bington LTD"},
                new Supplier() { Address="Clinton ave 12", SupplierId="2", SupplierName="WashStore LTD"},
                new Supplier() { Address="Gabboni st 144", SupplierId="11", SupplierName="DCH LTD"},
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Supplier>>();
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Supplier>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            var context = new Mock<YOBAContext>();
            context.Setup(s => s.Suppliers).Returns(mockDbSet.Object);

            var repo = new SupplierRepository(context.Object);
            repo.Change(new Supplier() { Address = "Gabboni st 144", SupplierId = "11", SupplierName = "DCH LTD" });

            repo.Should().NotBeSameAs(data);
            context.Verify(s => s.SaveChanges(), Times.Once());
        }
    }
}
