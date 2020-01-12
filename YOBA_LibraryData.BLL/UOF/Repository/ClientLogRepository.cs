using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YOBA_LibraryData.BLL;
using YOBA_LibraryData.DAL.Entities;
using YOBA_LibraryData.DAL.UOF.Interfaces;
using YOBA_Services.Exceptions;

namespace YOBA_LibraryData.DAL.UOF.Repository
{
    public class ClientLogRepository : IClientLogRepository
    {
        private readonly YOBAContext _context;
        public ClientLogRepository(YOBAContext context)
        {
            _context = context;
        }

        public void AddClientChanges(object obj, string UserId, string message)
        {
            UserLog log = new UserLog() { Id = UserId, Message = message, ObjectMessage = obj.GetType().ToString(), Time = DateTime.Now };
            _context.Add(log);
            _context.SaveChanges();
        }
    }
}
