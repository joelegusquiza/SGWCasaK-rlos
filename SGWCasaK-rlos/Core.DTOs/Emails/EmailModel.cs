using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Emails
{
    public class EmailModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string FromName { get; set; }
        public string ToName { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public string HtmlContent { get; set; }
    }
}
