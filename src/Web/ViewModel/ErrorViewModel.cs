using System;

namespace Web.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string source { get; set; }

        public string message { get; set; }
        public string stackTrace { get; set; }
    }
}
