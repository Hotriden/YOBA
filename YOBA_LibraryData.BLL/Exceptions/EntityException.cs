using System;
using System.Collections.Generic;
using System.Text;

namespace YOBA_LibraryData.DAL.Exceptions
{
    public class EntityException:Exception
    {
        public EntityException()
        {
        }

        public EntityException(string message)
            : base(message)
        {
        }

        public EntityException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
