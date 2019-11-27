﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.Interfaces;
using YOBA_Services.Exceptions;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class SupplierRepository : IBaseRepository<Supplier>
    {
        private YOBAContext _context;
        public SupplierRepository(YOBAContext context)
        {
            _context = context;
        }
        public void Add(Supplier item)
        {
            if (_context.Suppliers.Find(item.SupplierName) == null)
            {
                _context.Add(item);
                _context.SaveChanges();
            }
            else
            {
                throw new AlreadyExistException(item.SupplierName);
            }
        }

        public void Delete(Supplier item)
        {
            if (_context.Suppliers.First(supplier => supplier.SupplierId == item.SupplierId) != null)
            {
                _context.Remove(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.SupplierId);
            }
        }

        public IEnumerable<Supplier> GetAll()
        {
            if (_context.Suppliers != null)
            {
                return _context.Suppliers;
            }
            else
            {
                throw new EmptyDataException(typeof(Supplier).ToString());
            }
        }

        public Supplier GetById(int id)
        {
            var result = _context.Suppliers.First(supplier => supplier.SupplierId == id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new EmptyDataException(typeof(Supplier).ToString());
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Change(Supplier item)
        {
            if (_context.Suppliers.First(x => x.SupplierId == item.SupplierId) != null)
            {
                _context.Suppliers.Update(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.SupplierId);
            }
        }
    }
}
