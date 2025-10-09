using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejercicio.Models.Exportadores;

namespace Ejercicio.Models
{
    public class Multa :IComparable<Multa> ,IExportable
    {
        public string Patente { get; set; }
        DateTime Vencimiento { get; set; }
        double Importe { get; set; }
        Multa()
        {
        }

        public bool Importar (string data, IExportador exportador)
        {
            return exportador.Importar(data, this);
        }

        public string Exportar(IExportador exportador)
        {
            return exportador.Exportar(this);
        }

        public int CompareTo(Multa other)
        {
            return this.Patente.CompareTo(other.Patente);
        }

        public override string ToString()
        {
            return $"Patente:{this.Patente}, Importe:{this.Importe}$, Vencimiento:{this.Vencimiento}";
        }
    }
}
