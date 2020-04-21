using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YOBA_LibraryData.BLL;
using YOBA_LibraryData.DAL.Entities;
using YOBA_LibraryData.DAL.UOF.Interfaces;

namespace YOBA_LibraryData.DAL.UOF.Repository
{
    public class ClientLogRepository : IClientLogRepository
    {
        private readonly YOBAContext _context;
        public ClientLogRepository(YOBAContext context)
        {
            _context = context;
        }

        public void AddClientChanges(object obj, string userId, string message)
        {
            UserLog log = new UserLog() { UserId = userId, Message = message, ObjectMessage = obj.GetType().ToString(), Time = DateTime.Now };
            _context.Add(log);
            _context.SaveChanges();
        }
    }
}
