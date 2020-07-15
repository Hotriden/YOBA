using System;
using System.Collections.Generic;
using System.Text;
using YOBA_BLL.Services.MessageService;
using YOBA_LibraryData.BLL.Entities.Staff;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.Interfaces;
using YOBA_LibraryData.BLL.UOF.Interfaces;

namespace YOBA_BLL.Catalogue.SupplyCatalogueFolder
{
    public class WareHouseCatalogue : ICatalogue<WareHouse>
    {
        private readonly IUnitOfWork db;
        private readonly IMessageService messageService;

        public WareHouseCatalogue(IUnitOfWork _repo, IMessageService service)
        {
            messageService = service;
            db = _repo;
        }

        public void Create(WareHouse wareHouse, string UserId)
        {
            if (wareHouse.WareHouseName == null || wareHouse.Address == null)
            {
                messageService.InfoMessage(this, "Warehouse name or warehouse address spelled wrong", UserId);
            }
            else
            {
                if (db.WareHouseRepository.GetById(UserId, wareHouse.Id) == null)
                {
                    var _wareHouse = wareHouse;
                    _wareHouse.CreatedBy = UserId;
                    _wareHouse.Created = DateTime.Now;
                    db.WareHouseRepository.Add(_wareHouse);
                    db.Save();

                    messageService.InfoMessage(this, $"{wareHouse} successful created", UserId);
                }
                else
                {
                    messageService.InfoMessage(this, $"{wareHouse.Id} already exist", UserId);
                }
            }
        }

        public void Delete(WareHouse wareHouse, string UserId)
        {
            var result = db.WareHouseRepository.GetById(UserId, wareHouse.Id);
            if (result != null)
            {
                var _wareHouse = wareHouse;
                _wareHouse.LastModifiedBy = UserId;
                _wareHouse.LastModified = DateTime.Now;
                db.WareHouseRepository.Delete(UserId, _wareHouse);
                db.Save();

                messageService.InfoMessage(this, $"{result.WareHouseName} successful deleted", UserId);
            }
            else
            {
                messageService.InfoMessage(this, $"{result.WareHouseName} doesn't exist", UserId);
            }
        }

        public IEnumerable<WareHouse> GetAll(string UserId)
        {
            return db.WareHouseRepository.GetAll(UserId);
        }

        public void Update(WareHouse wareHouse, string UserId)
        {
            var result = db.WareHouseRepository.GetById(UserId, wareHouse.Id);
            if (result != null)
            {
                var _wareHouse = wareHouse;
                _wareHouse.LastModifiedBy = UserId;
                _wareHouse.LastModified = DateTime.Now;
                db.WareHouseRepository.Change(UserId, _wareHouse);
                db.Save();

                messageService.InfoMessage(this, $"{wareHouse.WareHouseName} successful changed", UserId);
            }
            else
            {
                messageService.InfoMessage(this, $"{wareHouse.WareHouseName} doesn't exist", UserId);
            }
        }
    }
}
