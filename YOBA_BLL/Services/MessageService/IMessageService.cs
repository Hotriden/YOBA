using System;
using System.Collections.Generic;
using System.Text;
using YOBA_LibraryData.BLL.Entities.User;

namespace YOBA_BLL.Services.MessageService
{
    public interface IMessageService
    {
        void InfoMessage(object obj, Client client, string message);
    }
}
