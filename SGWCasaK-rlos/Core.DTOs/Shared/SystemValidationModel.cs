using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Shared
{
    public class SystemValidationModel
    {
        public int Id { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
    }
}
