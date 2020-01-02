using System;
using System.Collections.Generic;
using System.Text;
using YOBA_BLL.Services.MessageService;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.Entities.User;
using YOBA_LibraryData.BLL.Interfaces;
using YOBA_LibraryData.BLL.UOF.Interfaces;

namespace YOBA_BLL.Catalogue
{
    public class SupplyCatalogue:ISupplyCatalogue
    {
        private readonly IUnitOfWork db;
        private readonly IMessageService messageService;

        public SupplyCatalogue(IUnitOfWork _repo, IMessageService service)
        {
            messageService = service;
            db = _repo;
        }

        public void CreateSupplier(Supplier supplier, Client client)
        {
            if (supplier.SupplierName == null & supplier.Address == null)
            {
                messageService.InfoMessage(client, "Wrong SupplierName or Address");
            }
            else 
            { 
                if (db.SupplierRepository.GetById(supplier.SupplierId) == null)
                {
                    db.SupplierRepository.Add(supplier);
                    db.Save();

                    messageService.InfoMessage(client, $"{supplier} successful created");
                }
                else
                {
                    messageService.InfoMessage(client, $"{supplier.SupplierId} already exist");
                }
            }
        }

        public void ChangeSupplier(Supplier supplier, Client client)
        {
            var result = db.SupplierRepository.GetById(supplier.SupplierId);
            if (result != null)
            {
                db.SupplierRepository.Change(supplier);
                db.Save();

                messageService.InfoMessage(client, $"{result.SupplierName} successful changed");
            }
            else
            {
                messageService.InfoMessage(client, $"{result.SupplierId} doesn't exist");
            }
        }

        public void DeleteSupplier(Supplier supplier, Client client)
        {
            var result = db.SupplierRepository.GetById(supplier.SupplierId);
            if (result != null)
            {
                db.SupplierRepository.Delete(supplier);
                db.Save();

                messageService.InfoMessage(client, $"{result.SupplierName} successful deleted");
            }
            else
            {
                messageService.InfoMessage(client, $"{result.SupplierId} doesn't exist");
            }
        }
    }
}
