using System;
using System.Collections.Generic;
using System.Text;
using YOBA_BLL.Catalogue.SupplyCatalogueFolder;
using YOBA_BLL.Services.MessageService;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_BLL.Catalogue.SupplyCatalogueFolder
{
    public class ReceiptCatalogue : ICatalogue<Receipt>
    {
        private readonly IUnitOfWork db;
        private readonly IMessageService messageService;

        public ReceiptCatalogue(IUnitOfWork _repo, IMessageService service)
        {
            messageService = service;
            db = _repo;
        }

        public void Create(Receipt receipt, string UserId)
        {
            if (receipt.ReceiptName == null & receipt.ReceiptValue == 0)
            {
                messageService.InfoMessage(this, "Receipt name or receipt value spelled wrong", UserId);
            }
            else
            {
                if (db.ReceiptRepository.GetById(receipt.ReceiptId) == null)
                {
                    var _receipt = receipt;
                    _receipt.CreatedBy = UserId;
                    _receipt.Created = DateTime.Now;
                    db.ReceiptRepository.Add(_receipt);
                    db.Save();

                    messageService.InfoMessage(this, $"{receipt} successful created", UserId);
                }
                else
                {
                    messageService.InfoMessage(this, $"{receipt.ReceiptId} already exist", UserId);
                }
            }
        }

        public void Delete(Receipt branch, string UserId)
        {
            throw new NotImplementedException();
        }


        public void Update(Receipt branch, string UserId)
        {
            throw new NotImplementedException();
        }
    }
}
