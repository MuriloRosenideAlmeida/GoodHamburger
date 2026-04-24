using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Application.Exceptions
{
    public class NaoEncontradoException : Exception
    {
        public NaoEncontradoException(string message) : base(message) { }
    }
}
