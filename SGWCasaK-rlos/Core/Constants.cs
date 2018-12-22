using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class Constants
    {
        public enum EstadoVenta
        {
            Pagado,
            Anulado,
            Pendiente
        }

        public enum PorcIva
        {
            Excento = 1,
            Cinco = 2,
            Diez = 3
        }

        public enum CondicionVenta
        {
            Contado,
            Credito
        }

        public static class CustomClaimTypes
        {
            public static string UserId = "UserId";
        }
    }
}
