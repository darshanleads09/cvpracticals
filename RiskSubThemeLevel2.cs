using System;

namespace WebApplicationTest.Models
{
    public class RiskSubThemeLevel2
    {
        public int RiskSubThemeLevel2Id { get; set; }
        public string RiskSubThemeLevel2Code { get; set; }
        public string RiskSubThemeLevel2Name { get; set; }
        public bool IsActive { get; set; }
        public int RiskThemeId { get; set; }
        public int RiskSubThemeLevel1Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime SysStartTime { get; set; }
        public DateTime SysEndTime { get; set; }

        public RiskTheme RiskTheme { get; set; }
        public RiskSubThemeLevel1 RiskSubThemeLevel1 { get; set; }
    }
}
