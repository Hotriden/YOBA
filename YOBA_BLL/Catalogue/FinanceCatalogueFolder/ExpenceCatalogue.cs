using System;
using System.Collections.Generic;
using System.Text;
using YOBA_BLL.Services.MessageService;
using YOBA_LibraryData.BLL.Entities.Finance;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_BLL.Catalogue.FinanceCatalogueFolder
{
    public class ExpenceCatalogue : ICatalogue<Expence>
    {
        private readonly IUnitOfWork db;
        private readonly IMessageService messageService;

        public ExpenceCatalogue(IUnitOfWork UOF, IMessageService _messageService)
        {
            db = UOF;
            messageService = _messageService;
        }
        public void Create(Expence item, string UserId)
        {
            if (item.Name == null || item.Value == 0)
            {
                messageService.InfoMessage(this, "Expence name spelled wrong. Value couldn't be 0", UserId);
            }
            else
            {
                if (db.ExpenceRepository.Get(UserId, item) == null)
                {
                    var _expence = item;
                    _expence.CreatedBy = UserId;
                    _expence.Created = DateTime.Now;
                    db.ExpenceRepository.Add(UserId, _expence);
                    db.Save();

                    messageService.InfoMessage(this, $"{item} successful created", UserId);
                }
                else
                {
                    messageService.InfoMessage(this, $"{item.Name} already exist", UserId);
                }
            }
        }

        public void Delete(Expence item, string UserId)
        {
            var result = db.ExpenceRepository.Get(UserId, item);
            if (result != null)
            {
                var _expence = item;
                _expence.LastModifiedBy = UserId;
                _expence.LastModified = DateTime.Now;
                db.ExpenceRepository.Delete(UserId, _expence);
                db.Save();

                messageService.InfoMessage(this, $"{item} successful deleted", UserId);
            }
            else
            {
                messageService.InfoMessage(this, $"{item.Name} doesn't exist", UserId);
            }
        }

        public IEnumerable<Expence> GetAll(string UserId)
        {
            return db.ExpenceRepository.GetAll(UserId);
        }

        public void Update(Expence item, string UserId)
        {
            var result = db.ExpenceRepository.Get(UserId, item);
            if (result != null)
            {
                var _expence = item;
                _expence.LastModifiedBy = UserId;
                _expence.LastModified = DateTime.Now;
                db.ExpenceRepository.Change(UserId, _expence);
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
