﻿using System;
using System.Collections.Generic;
using YOBA_BLL.Catalogue;
using YOBA_BLL.Catalogue.SupplyCatalogueFolder;
using YOBA_BLL.Services.MessageService;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_BLL.SupplyCatalogueFolder
{
    public class SupplierCatalogue:ICatalogue<Supplier>
    {
        private readonly IUnitOfWork db;
        private readonly IMessageService messageService;

        public SupplierCatalogue(IUnitOfWork _repo, IMessageService service)
        {
            messageService = service;
            db = _repo;
        }

        public void Create(Supplier supplier, string UserId)
         {
            if (supplier.SupplierName == null || supplier.Address == null)
            {
                messageService.InfoMessage(this, "Supplier name or supplier address spelled wrong", UserId);
            }
            else 
            { 
                if (db.SupplierRepository.Get(UserId, supplier) == null)
                {
                    var _supplier = supplier;
                    _supplier.CreatedBy = UserId;
                    _supplier.Created = DateTime.Now;
                    db.SupplierRepository.Add(UserId, _supplier);
                    db.Save();

                    messageService.InfoMessage(this, $"{supplier} successful created", UserId);
                }
                else
                {
                    messageService.InfoMessage(this, $"{supplier.Id} already exist", UserId);
                }
            }
        }

        public void Update(Supplier supplier, string UserId)
        {
            var result = db.SupplierRepository.Get(UserId, supplier);
            if (result != null)
            {
                var _supplier = supplier;
                _supplier.LastModifiedBy = UserId;
                _supplier.LastModified = DateTime.Now;
                db.SupplierRepository.Change(UserId, _supplier);
                db.Save();

                messageService.InfoMessage(this, $"{result.SupplierName} successful changed", UserId);
            }
            else
            {
                messageService.InfoMessage(this, $"{result.Id} doesn't exist", UserId);
            }
        }

        public void Delete(Supplier supplier, string UserId)
        {
            var result = db.SupplierRepository.Get(UserId, supplier);
            if (result != null)
            {
                var _supplier = supplier;
                _supplier.LastModifiedBy = UserId;
                _supplier.LastModified = DateTime.Now;
                db.SupplierRepository.Delete(UserId, _supplier);
                db.Save();

                messageService.InfoMessage(this, $"{result.SupplierName} successful deleted", UserId);
            }
            else
            {
                messageService.InfoMessage(this, $"{result.Id} doesn't exist", UserId);
            }
        }

        public IEnumerable<Supplier> GetAll(string UserId)
        {
            return db.SupplierRepository.GetAll(UserId);
        }
    }
}
