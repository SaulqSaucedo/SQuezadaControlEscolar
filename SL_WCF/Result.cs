using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SL_WCF
{
    public class Result
    {
        public bool Correct { get; set; } // return 1 / 2
        public Exception Ex { get; set; } // return exception
        public string ErrorMesagge { get; set; } // return guardar el error
        public object Object { get; set; } // return objeto
        public List<object> Objects { get; set; } // return lista de objetos
    }
}