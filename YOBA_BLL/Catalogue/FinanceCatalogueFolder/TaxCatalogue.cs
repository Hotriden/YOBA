using System;
using System.Collections.Generic;
using System.Text;
using YOBA_BLL.Services.MessageService;
using YOBA_LibraryData.BLL.Entities.Finance;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_BLL.Catalogue.FinanceCatalogueFolder
{
    public class TaxCatalogue : ICatalogue<Tax>
    {
        private readonly IUnitOfWork db;
        private readonly IMessageService messageService;

        public TaxCatalogue(IUnitOfWork UOF, IMessageService _messageService)
        {
            db = UOF;
            messageService = _messageService;
        }

        public void Create(Tax item, string UserId)
        {
            if (item.Name == null || item.Percent == 0)
            {
                messageService.InfoMessage(this, "Tax name spelled wrong. Value couldn't be 0", UserId);
            }
            else
            {
                if (db.TaxRepository.Get(UserId, item) == null)
                {
                    var _tax = item;
                    _tax.CreatedBy = UserId;
                    _tax.Created = DateTime.Now;
                    db.TaxRepository.Add(UserId, _tax);
                    db.Save();

                    messageService.InfoMessage(this, $"{item} successful created", UserId);
                }
                else
                {
                    messageService.InfoMessage(this, $"{item.Name} already exist", UserId);
                }
            }
        }

        public void Delete(Tax item, string UserId)
        {
            var result = db.TaxRepository.Get(UserId, item);
            if (result != null)
            {
                var _tax = item;
                _tax.LastModifiedBy = UserId;
                _tax.LastModified = DateTime.Now;
                db.TaxRepository.Delete(UserId, _tax);
                db.Save();

                messageService.InfoMessage(this, $"{item} successful deleted", UserId);
            }
            else
            {
                messageService.InfoMessage(this, $"{item.Name} doesn't exist", UserId);
            }
        }

        public IEnumerable<Tax> GetAll(string UserId)
        {
            return db.TaxRepository.GetAll(UserId);
        }

        public void Update(Tax item, string UserId)
        {
            var result = db.TaxRepository.Get(UserId, item);
            if (result != null)
            {
                var _tax = item;
                _tax.LastModifiedBy = UserId;
                _tax.LastModified = DateTime.Now;
                db.TaxRepository.Change(UserId, _tax);
                db.Save();

                messageService.InfoMessage(this, $"{item} successful changed", UserId);
            }
            else
            {
                messageService.InfoMessage(this, $"{item.Name} doesn't exist", UserId);
            }
        }
    }
}
