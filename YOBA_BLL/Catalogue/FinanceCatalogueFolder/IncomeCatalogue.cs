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
                if (db.TaxRepository.GetById(item.Id) == null)
                {
                    var _income = item;
                    _income.CreatedBy = UserId;
                    _income.Created = DateTime.Now;
                    db.IncomeRepository.Add(_income);
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
            var result = db.IncomeRepository.GetById(item.Id);
            if (result != null)
            {
                var _income = item;
                _income.LastModifiedBy = UserId;
                _income.LastModified = DateTime.Now;
                db.IncomeRepository.Delete(_income);
                db.Save();

                messageService.InfoMessage(this, $"{item} successful deleted", UserId);
            }
            else
            {
                messageService.InfoMessage(this, $"{item.Name} doesn't exist", UserId);
            }
        }

        public IEnumerable<Income> GetAll()
        {
            return db.IncomeRepository.GetAll();
        }

        public void Update(Income item, string UserId)
        {
            var result = db.IncomeRepository.GetById(item.Id);
            if (result != null)
            {
                var _income = item;
                _income.LastModifiedBy = UserId;
                _income.LastModified = DateTime.Now;
                db.IncomeRepository.Change(_income);
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
