using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWebTech.Contacts.Services.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
            : base("The specified resource cannot be found")
        { }
    }
}
