using System;
using System.Collections.Generic;
using System.Text;

namespace YOBA_BLL.Services.MessageService
{
    public interface IMessageService
    {
        void InfoMessage(object obj, string message, string UserId);
    }
}
