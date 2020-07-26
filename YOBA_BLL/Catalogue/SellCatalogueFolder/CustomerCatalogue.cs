using System;
using System.Collections.Generic;
using System.Text;
using YOBA_BLL.Services.MessageService;
using YOBA_LibraryData.BLL.Entities.Sell;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_BLL.Catalogue.SellCatalogueFolder
{
    public class CustomerCatalogue : ICatalogue<Customer>
    {
        private readonly IUnitOfWork db;
        private readonly IMessageService messageService;

        public CustomerCatalogue(IUnitOfWork UOF, IMessageService _messageService)
        {
            db = UOF;
            messageService = _messageService;
        }
        public void Create(Customer item, string UserId)
        {
            if (item.CustomerName == null || item.TelephoneNumber == null && item.CustomerEmail == null)
            {
                messageService.InfoMessage(this, "Customer name or telephone number spelled wrong. Customer Email should not be empty", UserId);
            }
            else
            {
                if (db.CustomerRepository.Get(UserId, item) == null)
                {
                    var _customer = item;
                    _customer.CreatedBy = UserId;
                    _customer.Created = DateTime.Now;
                    db.CustomerRepository.Add(UserId, _customer);
                    db.Save();

                    messageService.InfoMessage(this, $"{item} successful created", UserId);
                }
                else
                {
                    messageService.InfoMessage(this, $"{item.Id} already exist", UserId);
                }
            }
        }

        public void Delete(Customer item, string UserId)
        {
            var result = db.CustomerRepository.Get(UserId, item);
            if (result != null)
            {
                var _customer = item;
                _customer.LastModifiedBy = UserId;
                _customer.LastModified = DateTime.Now;
                db.CustomerRepository.Delete(UserId, _customer);
                db.Save();

                messageService.InfoMessage(this, $"{item} successful deleted", UserId);
            }
            else
            {
                messageService.InfoMessage(this, $"{item.Id} doesn't exist", UserId);
            }
        }

        public IEnumerable<Customer> GetAll(string UserId)
        {
            return db.CustomerRepository.GetAll(UserId);
        }

        public void Update(Customer item, string UserId)
        {
            var result = db.CustomerRepository.Get(UserId, item);
            if (result != null)
            {
                var _customer = item;
                _customer.LastModifiedBy = UserId;
                _customer.LastModified = DateTime.Now;
                db.CustomerRepository.Change(UserId, _customer);
                db.Save();

                messageService.InfoMessage(this, $"{item} successful changed", UserId);
            }
            else
            {
                messageService.InfoMessage(this, $"{item.Id} doesn't exist", UserId);
            }
        }
    }
}
