using System;
namespace MicroTest1.Messages
{
	public class AhExeption : Exception
        {
            public string Code { get; set; }

            public AhExeption(string code)
            {
                Code = code;
            }

            public AhExeption(string message, params object[] args)
                : this(string.Empty, message, args)
            {
            }

            public AhExeption(string code, string message, params object[] args)
                : this(null, code, message, args)
            {
            }

            public AhExeption(Exception innerException, string message, params object[] args)
                : this(innerException, string.Empty, message, args)
            {
            }

            public AhExeption(Exception innerException, string code, string message, params object[] args)
                : base(string.Format(message, args), innerException)
            {
                Code = code;
            }
        }
}


