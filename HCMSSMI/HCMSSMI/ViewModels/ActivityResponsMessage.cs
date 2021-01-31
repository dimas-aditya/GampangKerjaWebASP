using System;

namespace HCMSSMI.ViewModels
{
    public class ActivityResponsMessage
    {
        public string Code { get; set; }
        public DateTime? ResponseTime { get; set; }
        public DateTime? RequestTime { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
    }
}