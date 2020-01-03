using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YOBA_LibraryData.BLL;
using YOBA_LibraryData.BLL.Entities.User;
using YOBA_LibraryData.DAL.Entities.User;
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

        public void AddClientChanges(object obj, Client client, string message)
        {
            ClientLog log = new ClientLog();
            log.Client = client;
            log.Message = message;
            log.StackTraceType = obj.GetType().ToString();
            log.Created = DateTime.Now;
            _context.ClientLogs.Add(log);
            _context.SaveChanges();
        }
    }
}
