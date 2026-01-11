using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionDAM.Modelos
{
    /// <summary>
    /// Clase para gestionar el año actual en las facturas.
    /// </summary>
    public class YearManager
    {
        public int MinYear { get; private set; }
        public int MaxYear { get; private set; }
        public int CurrentYear { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="currentYear">Año actual</param>
        /// <param name="minYear">Límite inferior de los años permitidos.</param>
        /// <param name="maxYear">Límite superior de los años permitidos.</param>
        /// <exception cref="ArgumentException">Si minYear es mayor que maxYear</exception>
        public YearManager(int currentYear, int minYear, int maxYear)
        {
            if (minYear > maxYear)
                throw new ArgumentException("MinYear no puede ser mayor que MaxYear.");

            MinYear = minYear;
            MaxYear = maxYear;

            // Ajustamos el año inicial si está fuera de rango
            if (currentYear < MinYear)
                CurrentYear = MinYear;
            else if (currentYear > MaxYear)
                CurrentYear = MaxYear;
            else
                CurrentYear = currentYear;
        }

        /// <summary>
        /// Incrementa el año actual en una unidad.
        /// </summary>
        public void IncrementYear()
        {
            if (CurrentYear < MaxYear)
                CurrentYear++;
        }

        /// <summary>
        /// Decrementa el año actual en una unidad.
        /// </summary>
        public void DecrementYear()
        {
            if (CurrentYear > MinYear)
                CurrentYear--;
        }

        /// <summary>
        /// Obtiene una lista de años permitidos, desde el mínimo al máximo, ambos incluidos.
        /// </summary>
        /// <returns>Un objeto de tipo List, con los años permitidos.</returns>
        public List<int> GetYearList()
        {
            var list = new List<int>();
            for (int y = MaxYear; y >= MinYear; y--)
                list.Add(y);
            return list;
        }

        /// <summary>
        /// Formatea la salida del año actual.
        /// </summary>
        /// <returns>Un string con el año actual formateado.</returns>
        public override string ToString()
        {
            return CurrentYear.ToString();
        }
    }
}
