using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejercicio.Models.Exportadores;

namespace Ejercicio.Models
{
    public interface IExportable
    {
        bool Importar(string data, IExportador exportador);
        string Exportar(IExportador exportador);
    }
}
