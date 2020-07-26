using System;

namespace OpenWebTech.Contacts.Services.Exceptions
{
    public class DuplicateException : Exception
    {
        public DuplicateException()
            : base("Such a resource already exists")
        { }

    }
}
