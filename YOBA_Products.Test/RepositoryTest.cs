using Microsoft.VisualStudio.TestTools.UnitTesting;
using YOBA_LibraryData.BLL.UOF.Repository;
using Moq;
using YOBA_LibraryData.BLL;
using FluentAssertions;

namespace YOBA_Products.Test
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void AddMethodTest()
        {
            Mock<YOBAContext> context = new Mock<YOBAContext>();
            var repos = new ProductRepository(context.Object);
            repos.Should().NotBeNull();
        }
    }
}
