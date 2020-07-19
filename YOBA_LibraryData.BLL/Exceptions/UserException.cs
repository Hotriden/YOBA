using System;
using System.Collections.Generic;
using System.Text;

namespace YOBA_LibraryData.DAL.Exceptions
{
    class UserException:Exception
    {
        public UserException()
        {
        }

        public UserException(string message)
            : base(message)
        {
        }

        public UserException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
