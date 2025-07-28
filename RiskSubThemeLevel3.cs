using System;

namespace WebApplicationTest.Models
{
    public class RiskSubThemeLevel3
    {
        public int RiskSubThemeLevel3Id { get; set; }
        public string RiskSubThemeLevel3Code { get; set; }
        public string RiskSubThemeLevel3Name { get; set; }
        public bool IsActive { get; set; }
        public int RiskThemeId { get; set; }
        public int RiskSubThemeLevel2Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime SysStartTime { get; set; }
        public DateTime SysEndTime { get; set; }

        public RiskTheme RiskTheme { get; set; }
        public RiskSubThemeLevel2 RiskSubThemeLevel2 { get; set; }
    }
}
