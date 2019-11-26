using System;
using System.Collections.Generic;
using System.Text;

namespace YOBA_Services.Exceptions
{
    public class EmptyDataException: Exception
    {
        public EmptyDataException()
        {
        }

        public EmptyDataException(string message)
            : base(String.Format($"{message} has no data"))
        {
        }
    }
}
