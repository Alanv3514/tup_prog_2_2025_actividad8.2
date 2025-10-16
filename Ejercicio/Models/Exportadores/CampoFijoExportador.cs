using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ejercicio.Models.Exportadores
{
    internal class CampoFijoExportador : IExportador
    {
        public bool Importar(string data, Multa m)
        {

                try
                {
                    string patente = data.Substring(0, 9);

                    string day = data.Substring(9, 2);
                    string month = data.Substring(12, 2);
                    string year = data.Substring(15,4);
                    string fechaStr = $"{day}/{month}/{year}";

                    string importeStr = data.Substring(19);
                    DateTime fecha = DateTime.ParseExact(fechaStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime vencimiento = new DateTime(fecha.Year, fecha.Month, fecha.Day);
                    double importe = Convert.ToDouble(importeStr.Replace(',', '.'), CultureInfo.InvariantCulture);

                    m.Patente = patente;
                    m.Vencimiento = vencimiento;
                    m.Importe = importe;

                    return true;
                }
                catch
                {
                    return false;
                }

        }
        public string Exportar(Multa m)
        {
            return $@"{m.Patente}   {m.Vencimiento}   {m.Importe:f2}";
        }
    }
}
