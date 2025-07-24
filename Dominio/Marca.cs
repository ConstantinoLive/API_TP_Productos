using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Marca
    {
        public int id { get; set; }
        public string marca { get; set; }

        public override string ToString()
        {
            return marca;
        }
    }
}
