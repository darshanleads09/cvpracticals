using System;

namespace WebApplicationTest.Models
{
    public class RiskTheme
    {
        public int RiskThemeId { get; set; }
        public string RiskThemeCode { get; set; }
        public string RiskThemeName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime SysStartTime { get; set; }
        public DateTime SysEndTime { get; set; }
    }
}
