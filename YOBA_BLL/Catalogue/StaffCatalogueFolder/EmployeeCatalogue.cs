﻿using System;
using System.Collections.Generic;
using System.Text;
using YOBA_BLL.Services.MessageService;
using YOBA_LibraryData.BLL.Entities.Staff;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_BLL.Catalogue.StaffCatalogueFolder
{
    public class EmployeeCatalogue : ICatalogue<Employee>
    {
        private readonly IUnitOfWork db;
        private readonly IMessageService messageService;

        public EmployeeCatalogue(IUnitOfWork UOF, IMessageService _messageService)
        {
            db = UOF;
            messageService = _messageService;
        }

        public void Create(Employee item, string UserId)
        {
            if (item.Name == null || item.Position == null)
            {
                messageService.InfoMessage(this, "Employee name or position spelled wrong", UserId);
            }
            else
            {
                if (db.EmployeeRepository.GetById(item.EmployeeId) == null)
                {
                    var _employee = item;
                    _employee.CreatedBy = UserId;
                    _employee.Created = DateTime.Now;
                    db.EmployeeRepository.Add(_employee);
                    db.Save();

                    messageService.InfoMessage(this, $"{item} successful created", UserId);
                }
                else
                {
                    messageService.InfoMessage(this, $"{item.EmployeeId} already exist", UserId);
                }
            }
        }

        public void Delete(Employee item, string UserId)
        {
            var result = db.EmployeeRepository.GetById(item.EmployeeId);
            if (result != null)
            {
                var _employee = item;
                _employee.LastModifiedBy = UserId;
                _employee.LastModified = DateTime.Now;
                db.EmployeeRepository.Delete(_employee);
                db.Save();

                messageService.InfoMessage(this, $"{_employee.Name} successful deleted", UserId);
            }
            else
            {
                messageService.InfoMessage(this, $"{item.Name} doesn't exist", UserId);
            }
        }

        public void Update(Employee item, string UserId)
        {
            var result = db.EmployeeRepository.GetById(item.EmployeeId);
            if (result != null)
            {
                var _employee = item;
                _employee.LastModifiedBy = UserId;
                _employee.LastModified = DateTime.Now;
                db.EmployeeRepository.Change(_employee);
                db.Save();

                messageService.InfoMessage(this, $"{item.Name} successful changed", UserId);
            }
            else
            {
                messageService.InfoMessage(this, $"{item.Name} doesn't exist", UserId);
            }
        }
        public IEnumerable<Employee> GetAll()
        {
            return db.EmployeeRepository.GetAll();
        }
    }
}
