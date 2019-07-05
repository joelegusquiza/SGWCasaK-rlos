using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Core.Constants;

namespace Core.Helpers
{
    public static class Helpers
    {

		public static string FormatStock(int stock, Dictionary<string, int> presentaciones, UnidadesMedida unidadMedidaDefault)
		{
			var format = "";
			if (stock == 0)
				return "0";
			else if (!presentaciones.Any(x => x.Value <= stock))
			{
				format += $"{stock} unidades";
			} else
			{
				var stockInit = stock;
				foreach (var presentacion in presentaciones)
				{

					if (presentaciones.Any(x => x.Value <= stockInit))
					{
						var cant = (int)(stockInit / presentacion.Value);
						var rest = stock % presentacion.Value;
						stockInit = rest;
						format += $"{cant} {presentacion.Key} ";
					}

				}
				if (stockInit != 0)
				{
					format += $"y {stockInit} unidades";
				}
			}
           
            
            return format;
        }
    }
}
