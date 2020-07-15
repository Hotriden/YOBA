using System;
using System.Collections.Generic;
using System.Text;
using YOBA_BLL.Services.MessageService;
using YOBA_LibraryData.BLL.Entities.Sell;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_BLL.Catalogue.SellCatalogueFolder
{
    public class PaymentCatalogue : ICatalogue<Payment>
    {
        private readonly IUnitOfWork db;
        private readonly IMessageService messageService;

        public PaymentCatalogue(IUnitOfWork UOF, IMessageService _messageService)
        {
            db = UOF;
            messageService = _messageService;
        }
        public void Create(Payment item, string UserId)
        {
            if (item.IdentialPayNumber == null || item.Cusmoter == null || item.Value == 0)
            {
                messageService.InfoMessage(this, "Pay number or customer spelled wrong. Value couldn't be 0", UserId);
            }
            else
            {
                if (db.PaymentRepository.GetById(UserId, item.Id) == null)
                {
                    var _payment = item;
                    _payment.CreatedBy = UserId;
                    _payment.Created = DateTime.Now;
                    db.PaymentRepository.Add(UserId, _payment);
                    db.Save();

                    messageService.InfoMessage(this, $"{item} successful created", UserId);
                }
                else
                {
                    messageService.InfoMessage(this, $"{item.IdentialPayNumber} already exist", UserId);
                }
            }
        }

        public void Delete(Payment item, string UserId)
        {
            var result = db.PaymentRepository.GetById(UserId, item.Id);
            if (result != null)
            {
                var _payment = item;
                _payment.LastModifiedBy = UserId;
                _payment.LastModified = DateTime.Now;
                db.PaymentRepository.Delete(UserId, _payment);
                db.Save();

                messageService.InfoMessage(this, $"{item} successful deleted", UserId);
            }
            else
            {
                messageService.InfoMessage(this, $"{item.IdentialPayNumber} doesn't exist", UserId);
            }
        }

        public IEnumerable<Payment> GetAll(string UserId)
        {
            return db.PaymentRepository.GetAll(UserId);
        }

        public void Update(Payment item, string UserId)
        {
            var result = db.PaymentRepository.GetById(UserId, item.Id);
            if (result != null)
            {
                var _payment = item;
                _payment.LastModifiedBy = UserId;
                _payment.LastModified = DateTime.Now;
                db.PaymentRepository.Change(UserId, _payment);
                db.Save();

                messageService.InfoMessage(this, $"{item} successful changed", UserId);
            }
            else
            {
                messageService.InfoMessage(this, $"{item.IdentialPayNumber} doesn't exist", UserId);
            }
        }
    }
}
