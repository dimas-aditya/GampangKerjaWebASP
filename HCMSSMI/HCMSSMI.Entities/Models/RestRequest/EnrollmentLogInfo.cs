using System;
using System.Collections.Generic;

namespace Entities.Models.RestRequest
{
    public class EnrollmentLogInfo
    {
        public string CodeProgress { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public string LogFileName { get; set; }
        public string LogFilePath { get; set; }
        public int ? CountCompleted { get; set; }
        public int ? CountFailed { get; set; }
        public List<string> LogData { get; set; }
        public DateTime ? ResponseAt { get; set; } 


    }
}
