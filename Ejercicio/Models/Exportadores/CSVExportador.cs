using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ejercicio.Models.Exportadores
{
    public class CSVExportador : IExportador
    {
        public bool Importar(string data, Multa m)
        {
            string[] datos = data.Split(';');
            if (datos.Length == 3)
            {
                string patente = datos[0];
                if (!DateTime.TryParseExact(datos[1], "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fecha))
                {
                    return false;
                }
                string importe = datos[2];

                m.Patente = patente.Trim();
                m.Vencimiento = fecha;
                if (!double.TryParse(importe.Trim(), out double importeParsed))
                {
                    return false;
                }
                m.Importe = importeParsed;
                return true;
            }
            return false;
        }
        public string Exportar(Multa m)
        {
            return $"{m.Patente};{m.Vencimiento:dd/MM/yyyy};{m.Importe:f2}";
        }
    }
}
