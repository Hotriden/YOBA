using System;
using System.Collections.Generic;
using System.Text;
using YOBA_BLL.Services.MessageService;
using YOBA_LibraryData.BLL.Entities.Staff;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_BLL.Catalogue.StaffCatalogueFolder
{
    public class BranchCatalogue : ICatalogue<Branch>
    {
        private readonly IUnitOfWork db;
        private readonly IMessageService messageService;

        public BranchCatalogue(IUnitOfWork UOF, IMessageService _messageService)
        {
            db = UOF;
            messageService = _messageService;
        }

        public void Create(Branch item, string UserId)
        {
            if (item.BranchName == null)
            {
                messageService.InfoMessage(this, "Branch name spelled wrong", UserId);
            }
            else
            {
                if (db.BranchRepository.GetById(UserId, item.BranchId) == null)
                {
                    var _branch = item;
                    _branch.CreatedBy = UserId;
                    _branch.Created = DateTime.Now;
                    db.BranchRepository.Add(UserId, _branch);
                    db.Save();

                    messageService.InfoMessage(this, $"{item} successful created", UserId);
                }
                else
                {
                    messageService.InfoMessage(this, $"{item.BranchId} already exist", UserId);
                }
            }
        }

        public void Delete(Branch item, string UserId)
        {
            var result = db.BranchRepository.GetById(UserId, item.BranchId);
            if (result != null)
            {
                var _branch = item;
                _branch.LastModifiedBy = UserId;
                _branch.LastModified = DateTime.Now;
                db.BranchRepository.Delete(UserId, _branch);
                db.Save();

                messageService.InfoMessage(this, $"{_branch.BranchName} successful deleted", UserId);
            }
            else
            {
                messageService.InfoMessage(this, $"{item.BranchName} doesn't exist", UserId);
            }
        }


        public void Update(Branch item, string UserId)
        {
            var result = db.BranchRepository.GetById(UserId, item.BranchId);
            if (result != null)
            {
                var _branch = item;
                _branch.LastModifiedBy = UserId;
                _branch.LastModified = DateTime.Now;
                db.BranchRepository.Change(UserId, _branch);
                db.Save();

                messageService.InfoMessage(this, $"{item.BranchName} successful changed", UserId);
            }
            else
            {
                messageService.InfoMessage(this, $"{item.BranchName} doesn't exist", UserId);
            }
        }

        public IEnumerable<Branch> GetAll(string UserId)
        {
            return db.BranchRepository.GetAll(UserId);
        }
    }
}
