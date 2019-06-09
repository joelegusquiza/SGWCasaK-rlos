using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Shared
{
    public class DropDownViewModel<T>
    {
        public string Text { get; set; }
        public T Value { get; set; }
    }

    public class AdditionalData : DropDownViewModel<int>
    {
		public string AdditionalString { get; set; }
        public bool AdditionalBool { get; set; }
    }
}
