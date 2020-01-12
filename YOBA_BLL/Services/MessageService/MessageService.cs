using System;
using System.Collections.Generic;
using System.Text;
using YOBA_LibraryData.BLL.Interfaces;
using YOBA_LibraryData.DAL.UOF.Interfaces;

namespace YOBA_BLL.Services.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork db;

        public MessageService(IUnitOfWork repository)
        {
            db = repository;
        }

        public void InfoMessage(object obj, string UserId, string message)
        {
            db.ClientLogRepository.AddClientChanges(obj, UserId, message);
        }
    }
}
