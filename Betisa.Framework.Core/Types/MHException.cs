using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betisa.Framework.Core.Types
{
    public class MHException : Exception
    {
        public string Code { get; set; }

        public MHException(string code)
        {
            Code = code;
        }

        public MHException(string message, params object[] args)
            : this(string.Empty, message, args)
        {
        }

        public MHException(string code, string message, params object[] args)
            : this(null, code, message, args)
        {
        }

        public MHException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public MHException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}
