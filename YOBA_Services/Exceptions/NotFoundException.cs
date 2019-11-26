using System;
using System.Collections.Generic;
using System.Text;

namespace YOBA_Services.Exceptions
{
    public class NotFoundException:Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message)
            : base(String.Format($"{message} not found"))
        {
        }

        public NotFoundException(int id)
            : base(String.Format($"{id} not found"))
        {
        }
    }
}
