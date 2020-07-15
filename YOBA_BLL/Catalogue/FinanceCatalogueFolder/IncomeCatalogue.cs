using System;
using System.Collections.Generic;
using System.Text;
using YOBA_BLL.Services.MessageService;
using YOBA_LibraryData.BLL.Entities.Finance;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_BLL.Catalogue.FinanceCatalogueFolder
{
    public class IncomeCatalogue : ICatalogue<Income>
    {
        private readonly IUnitOfWork db;
        private readonly IMessageService messageService;

        public IncomeCatalogue(IUnitOfWork UOF, IMessageService _messageService)
        {
            db = UOF;
            messageService = _messageService;
        }
        public void Create(Income item, string UserId)
        {
            if (item.Name == null || item.Value == 0)
            {
                messageService.InfoMessage(this, "Income name spelled wrong. Value couldn't be 0", UserId);
            }
            else
            {
                if (db.TaxRepository.GetById(UserId, item.Id) == null)
                {
                    var _income = item;
                    _income.CreatedBy = UserId;
                    _income.Created = DateTime.Now;
                    db.IncomeRepository.Add(UserId, _income);
                    db.Save();

                    messageService.InfoMessage(this, $"{item} successful created", UserId);
                }
                else
                {
                    messageService.InfoMessage(this, $"{item.Name} already exist", UserId);
                }
            }
        }

        public void Delete(Income item, string UserId)
        {
            var result = db.IncomeRepository.GetById(UserId, item.Id);
            if (result != null)
            {
                var _income = item;
                _income.LastModifiedBy = UserId;
                _income.LastModified = DateTime.Now;
                db.IncomeRepository.Delete(UserId, _income);
                db.Save();

                messageService.InfoMessage(this, $"{item} successful deleted", UserId);
            }
            else
            {
                messageService.InfoMessage(this, $"{item.Name} doesn't exist", UserId);
            }
        }

        public IEnumerable<Income> GetAll(string UserId)
        {
            return db.IncomeRepository.GetAll(UserId);
        }

        public void Update(Income item, string UserId)
        {
            var result = db.IncomeRepository.GetById(UserId, item.Id);
            if (result != null)
            {
                var _income = item;
                _income.LastModifiedBy = UserId;
                _income.LastModified = DateTime.Now;
                db.IncomeRepository.Change(UserId, _income);
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
