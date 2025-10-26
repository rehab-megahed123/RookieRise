using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RookieRise.Data.Contracts
{
    public record Error(string Code, string Message)
    {
        public static readonly Error none = new Error(string.Empty, string.Empty);

        public static Error NotFound(string code, string message) => new(code, message);
        public static Error Validation(string code, string message) => new(code, message);
        public static Error Failure(string code, string message) => new(code, message);
    }
}

