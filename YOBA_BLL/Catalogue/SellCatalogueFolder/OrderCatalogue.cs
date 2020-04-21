using System;
using System.Collections.Generic;
using System.Text;
using YOBA_BLL.Services.MessageService;
using YOBA_LibraryData.BLL.Entities.Sell;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_BLL.Catalogue.SellCatalogueFolder
{
    public class OrderCatalogue : ICatalogue<Order>
    {
        private readonly IUnitOfWork db;
        private readonly IMessageService messageService;

        public OrderCatalogue(IUnitOfWork UOF, IMessageService _messageService)
        {
            db = UOF;
            messageService = _messageService;
        }

        public void Create(Order item, string UserId)
        {
            if (item.OrderIdentity == null || item.Customer == null || item.OrderSum == 0)
            {
                messageService.InfoMessage(this, "Order number or customer spelled wrong. Value couldn't be 0", UserId);
            }
            else
            {
                if (db.OrderRepository.GetById(item.Id) == null)
                {
                    var _order = item;
                    _order.CreatedBy = UserId;
                    _order.Created = DateTime.Now;
                    db.OrderRepository.Add(_order);
                    db.Save();

                    messageService.InfoMessage(this, $"{item} successful created", UserId);
                }
                else
                {
                    messageService.InfoMessage(this, $"{item.OrderIdentity} already exist", UserId);
                }
            }
        }

        public void Delete(Order item, string UserId)
        {
            var result = db.OrderRepository.GetById(item.Id);
            if (result != null)
            {
                var _order = item;
                _order.LastModifiedBy = UserId;
                _order.LastModified = DateTime.Now;
                db.OrderRepository.Delete(_order);
                db.Save();

                messageService.InfoMessage(this, $"{item} successful deleted", UserId);
            }
            else
            {
                messageService.InfoMessage(this, $"{item.OrderIdentity} doesn't exist", UserId);
            }
        }

        public IEnumerable<Order> GetAll()
        {
            return db.OrderRepository.GetAll();
        }

        public void Update(Order item, string UserId)
        {
            var result = db.OrderRepository.GetById(item.Id);
            if (result != null)
            {
                var _order = item;
                _order.LastModifiedBy = UserId;
                _order.LastModified = DateTime.Now;
                db.OrderRepository.Change(_order);
                db.Save();

                messageService.InfoMessage(this, $"{item} successful changed", UserId);
            }
            else
            {
                messageService.InfoMessage(this, $"{item.OrderIdentity} doesn't exist", UserId);
            }
        }
    }
}
