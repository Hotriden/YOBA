using System;
using System.Collections.Generic;
using System.Text;

namespace YOBA_Services.Exceptions
{
    public class AlreadyExistException:Exception
    {
        public AlreadyExistException()
        {
        }

        public AlreadyExistException(string message)
            : base(String.Format($"{message} already exist"))
        {
        }
    }
}
