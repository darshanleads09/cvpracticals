using System;

namespace WebApplicationTest.Models
{
    public class RiskSubThemeLevel1
    {
        public int RiskSubThemeLevel1Id { get; set; }
        public string RiskSubThemeLevel1Code { get; set; }
        public string RiskSubThemeLevel1Name { get; set; }
        public bool IsActive { get; set; }
        public int RiskThemeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime SysStartTime { get; set; }
        public DateTime SysEndTime { get; set; }

        public RiskTheme RiskTheme { get; set; }
    }
}
