using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RallyCat.Core.DataAccess
{
    public class Result <T>
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public T Object { get; set; }
    }
}
