using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using YOBA_BLL.Supply;
using YOBA_LibraryData.BLL.Entities.Supply;
using FluentAssertions;
using Moq;
using Microsoft.EntityFrameworkCore;
using YOBA_LibraryData.BLL;
using YOBA_LibraryData.BLL.UOF.Repository;
using YOBA_LibraryData.BLL.Interfaces;
using YOBA_LibraryData.BLL.UOF;

namespace YOBA_Tests.BLL.Production
{
    public class WareHouseBLLTest
    {
        [Test]
        public void CheckRawStuff_Test()
        {
            var receipt = new List<Receipt>() {
                new Receipt() { DocumentNumber="101010SD", Shipped=true, ReceiptId=1, ReceiptValue=15 },
                new Receipt() { DocumentNumber="101010DD", Shipped=false, ReceiptId=2, ReceiptValue=5 }
            };
            var receiptForWareHouse = new List<Receipt>() {
                new Receipt() { DocumentNumber="101010SD", Shipped=true, ReceiptId=1, ReceiptValue=25 },
                new Receipt() { DocumentNumber="101010DD", Shipped=false, ReceiptId=2, ReceiptValue=35 },
                new Receipt() { DocumentNumber="101010TD", Shipped=false, ReceiptId=3, ReceiptValue=48 },
                new Receipt() { DocumentNumber="101010CD", Shipped=true, ReceiptId=4, ReceiptValue=55 }
            };
            
            var product = new Receipt() { Cost = 10, Price = 20, ReceiptName = "Detail B", ReceiptId = 1 };
            var wareHouse = new WareHouse() { Id = 1, ProductOportunity = true, WareHouseName = "First wareHouse", Receipts = receiptForWareHouse };

            IWareHouseBLL WareHouse = new WareHouseBLL();
            var result = WareHouse.CheckRawStuff(product, wareHouse);

            result.Should().NotBeNullOrEmpty();
            result.Should().Be($"{product.ReceiptName} successful created");
        }
    }
}
