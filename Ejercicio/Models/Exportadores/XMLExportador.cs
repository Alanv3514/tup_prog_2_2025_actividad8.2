using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio.Models.Exportadores
{
    internal class XMLExportador : IExportador
    {
        public string Exportar(Multa m)
        {
            return $"<Multa>";
        }

        public bool Importar(string data, Multa m)
        {

            return false;
        }
    }
}
