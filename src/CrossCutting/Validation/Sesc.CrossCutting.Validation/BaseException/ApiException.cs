using System;
using System.Collections.Generic;
using System.Text;

namespace Sesc.CrossCutting.Validation.BaseException
{
    public class ApiException : Exception
    {
        public ContentSingleton Content { get; set; }

        public ApiException(ContentSingleton content) : base()
        {
            Content = content;
        }
    }
}
