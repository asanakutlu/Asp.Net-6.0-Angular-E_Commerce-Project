using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Exceptions
{
    public class PasswordChangeFailedExcepiton:Exception
    {
        public PasswordChangeFailedExcepiton() : base("hatalı")
        {
        }

        public PasswordChangeFailedExcepiton(string? message) : base(message)
        {
        }

        public PasswordChangeFailedExcepiton(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
