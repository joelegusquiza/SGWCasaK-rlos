using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Helpers
{
    public static class Helpers
    {
        
        public static string FormatStock(int stock, Dictionary<string, int> presentaciones)
        {
            if (stock == 0)
                return "0";
            var format = "";
            var stockInit = stock;
            foreach (var presentacion in presentaciones)
            {
                
                if (presentaciones.Any(x=> x.Value <= stockInit))
                {
                    var cant = (int)(stockInit / presentacion.Value);
                    var rest = stock % presentacion.Value;
                    stockInit = rest;
                    format += $"{cant} {presentacion.Key} ";
                }
                else if (stockInit != 0)
                {
                    format += $"{stockInit} unidades";
                }
            }
            return format;
        }
    }
}
